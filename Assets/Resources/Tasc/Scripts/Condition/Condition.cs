using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Condition : Expression
    {
        public static Condition DummyCondition = new Condition(true);
        public static Condition NeverSatisfied = new Condition(false);
        public static Condition StartFromBeginning = DummyCondition;

        ////////////////////////////////////////////////
        // should implement as multiple operator and operand
        ////////////////////////////////////////////////
        ///

        public RelationalOperator comparison;
        public State endConditionState = null;
        public TimeState holdingTimer;
        public bool isSatisfied;
        public int holdingCount;
        public bool isActivated;

        // constructor for dummy condition
        public Condition(bool _isSatisfied)
        {
            isSatisfied = _isSatisfied;
            isActivated = true;
            comparison = RelationalOperator.NotEqual;
            holdingTimer = null;
            holdingCount = 0;
        }

        public Condition(State _endCondition, RelationalOperator _comparison)
        {
            endConditionState = _endCondition;
            comparison = _comparison;
            isSatisfied = false;
            holdingTimer = null;
            holdingCount = 0;
        }

        public Condition(State _endCondition, RelationalOperator _comparison, TimeState _elapsedState): this(_endCondition, _comparison)
        {
            holdingTimer = _elapsedState;
        }

        ~Condition()
        {
            Deactivate();
        }

        public override void ActivateAndStartMonitoring()
        {
            if (!isActivated)
            {
                isActivated = true;
                StartMonitoring();                 
            }
        }

        public override void Activate()
        {
            isActivated = true;
        }

        public override void Deactivate()
        {
            if (isActivated)
            {
                isActivated = false;
                StopMonitoring();
            }
        }

        public override string ToString()
        {
            return endConditionState + " : " + comparison + (holdingTimer == null ? "" : " (during " + holdingTimer.ToString() + ")");
        }

        public void StartMonitoring()
        {
            ConditionPublisher.Instance.OnCheck += Send;
        }

        public void StopMonitoring()
        {
            ConditionPublisher.Instance.OnCheck -= Send;
        }

        public void Send(State state)
        {
            if(isActivated && !isSatisfied)
                Check(state);
        }

        public override bool Check(State state1, Operator ope, State state2, TimeState timeState = null)
        {
            if (isSatisfied)
                return true;
            bool result = false;

            // unwrapping autovariable state: we convert it to the specific varible state inside of the AutoVariableState.
            if (state1.GetType() == typeof(AutoVariableState))
                state1 = (state1 as AutoVariableState).GetVariableState();
            if (state2.GetType() == typeof(AutoVariableState))
                state2 = (state2 as AutoVariableState).GetVariableState();

            if (state1.GetType() == state2.GetType())
            {
                //Debug.Log("Check: " + state1.ToString() + "\t" + state2.ToString());
                //Debug.Log(state1.CompareTo(state2));
                if (ope == RelationalOperator.Larger)
                    result = state1.CompareTo(state2) > 0;
                else if (ope == RelationalOperator.LargerOrEqual)
                    result = state1.CompareTo(state2) > 0 || state1.Equals(state2);
                else if (ope == RelationalOperator.Equal)
                    result = state1.Equals(state2);
                else if (ope == RelationalOperator.SmallerOrEqual)
                    result = state1.CompareTo(state2) < 0 || state1.Equals(state2);
                else if (ope == RelationalOperator.Smaller)
                    result = state1.CompareTo(state2) < 0;
                else if (ope == RelationalOperator.NotEqual)
                    result = state1 != state2;
            }
            if (holdingTimer != null)
            {
                if (result)
                {
                    if(holdingCount<30)
                        holdingCount += 1;
                    if (!holdingTimer.IsTimerOn())
                        holdingTimer.StartTimer();
                }
                else
                {
                    if (holdingCount > 0)
                        holdingCount -= 1;
                    else
                    {
                        holdingTimer.StopTimer();
                    }
                }
                if (holdingTimer.IsOver())
                    isSatisfied = true;
            }
            else
                isSatisfied = result;
            return isSatisfied;
        }

        public bool Check(State state, TimeState timeState = null)
        {
            if (endConditionState.GetType() == typeof(TimeState))
                return HandleTimeState(this);
            else if (endConditionState.GetType() == typeof(TaskState))
                return HandleTaskState(this);
            else if (endConditionState.GetType() == typeof(VariableDistanceState) && state.GetType().IsSubclassOf(typeof(VariableState)))
                return HandleVariableDistanceState(this, state, timeState);
            else if (endConditionState.GetType() == typeof(DistanceState))
                return HandleDistanceState(this, state, timeState);
            else
                return Check(state, comparison, endConditionState, timeState);
        }

        // passive check
        public virtual bool Check()
        {
            if (!isActivated)
                return false;
            if (isSatisfied)
                return true;

            if (endConditionState == null)
                throw new MissingComponentException();
            if (ShouldCheckTimeState())
                return HandleTimeState(this);
            else if (ShouldCheckTaskState())
                return HandleTaskState(this);
            return false;
        }

        public bool ShouldCheckPassively()
        {
            return ShouldCheckTaskState() || ShouldCheckTimeState();
        }

        public bool ShouldCheckTimeState()
        {
            return endConditionState.GetType() == typeof(TimeState);
        }

        public bool ShouldCheckTaskState()
        {
            return endConditionState.GetType() == typeof(TaskState);
        }

        private bool HandleDistanceState(Condition cond, State state, TimeState timeState)
        {
            DistanceState var1 = cond.endConditionState as DistanceState;
            MoveState var2 = state as MoveState;
            if ((state as MoveState) != null)
            {
                if (var1.hasMoveStateFromSameTerminus(var2))
                    return Check(var1.GetUpdated(var2), cond.comparison, cond.endConditionState, timeState);
            }
            return false;
        }

        private bool HandleVariableDistanceState(Condition cond, State state, TimeState timeState)
        {
            VariableDistanceState var1 = cond.endConditionState as VariableDistanceState;
            if ((state as VariableState) != null)
            {
                VariableState var2 = state as VariableState;
                if (VariableState.IsSameVariable(var1.stateVar1, var2))
                {
                    return Check(new VariableDistanceState(var1, var2), cond.comparison, cond.endConditionState, timeState);
                }
                    
            }
            return false;
        }

        private bool HandleTimeState(Condition cond)
        {
            if (ShouldCheckTimeState())
            {
                return Check(TimeState.GlobalTimer, cond.comparison, cond.endConditionState);
            }
            else
                return false;
        }

        private bool HandleTaskState(Condition cond)
        {
            if (ShouldCheckTaskState())
            {
                TaskState taskState = cond.endConditionState as TaskState;
                //Debug.Log("HandleTaskState : " + Check(new TaskState(taskState.task), cond.endConditionState, cond.comparison));
                return Check(new TaskState(taskState.task), cond.comparison, cond.endConditionState);
            }
            else
                return false;
        }
    }
}