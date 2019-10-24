using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    class ConditionContainer : Expression
    {
        public Condition condition1;
        public Condition condition2;
        public LogicalOperator relationship;
        public TimeState holdingTimer;
        protected bool isSatisfied;

        public ConditionContainer(Condition c1, LogicalOperator ope, Condition c2, TimeState timeState = null)
        {
            condition1 = c1;
            relationship = ope;
            condition2 = c2;
            holdingTimer = timeState;
            isSatisfied = false;
        }

        public override void Activate()
        {
            if (condition1 != null)
                condition1.Activate();
            if (condition2 != null)
                condition2.Activate();
        }

        public override void Deactivate()
        {
            if (condition1 != null)
                condition1.Deactivate();
            if (condition2 != null)
                condition2.Deactivate();
        }

        public override bool IsActivated()
        {
            return condition1.IsActivated() && condition2.IsActivated();
        }

        public override void ActivateAndStartMonitoring()
        {
            Debug.Log(condition1);
            Debug.Log(condition2);
            if (condition1 != null && condition2 != null)
            {
                condition1.Activate();
                condition2.Activate();
                StartMonitoring();
            }
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
            if (IsActivated() && !IsSatisfied())
                CheckActive(state);
        }

        protected override bool Check(State state1, Operator ope, State state2, TimeState timeState = null)
        {
            throw new NotImplementedException();
        }

        public override bool CheckPassive()
        {
            // for the case where two conditions are both passive types...
            if (condition1.ShouldCheckPassively())
                condition1.CheckPassive();
            if (condition2.ShouldCheckPassively())
                condition2.CheckPassive();
            if(condition1.ShouldCheckPassively() && condition2.ShouldCheckPassively())
            {
                isSatisfied = false;
                if (relationship == LogicalOperator.And)
                    isSatisfied = condition1.IsSatisfied() && condition2.IsSatisfied();
                else if (relationship == LogicalOperator.Or)
                    isSatisfied = condition1.IsSatisfied() || condition2.IsSatisfied();
                else
                    throw new Exception("No logical operator set.");
            }
            return isSatisfied;
        }

        public override bool CheckActive(State state, TimeState timeState = null)
        {
            isSatisfied = false;
            if (relationship == LogicalOperator.And)
                isSatisfied = condition1.CheckActive(state, timeState) && condition2.CheckActive(state, timeState);
            else if (relationship == LogicalOperator.Or)
                isSatisfied = condition1.CheckActive(state, timeState) || condition2.CheckActive(state, timeState);
            else
                throw new Exception("No logical operator set.");
            return isSatisfied;
        }

        public override bool IsSatisfied()
        {
            return isSatisfied;
            /*
            bool isSatisfied = false;
            if (relationship == LogicalOperator.And)
                isSatisfied = condition1.IsSatisfied() && condition2.IsSatisfied();
            else if (relationship == LogicalOperator.Or)
                isSatisfied = condition1.IsSatisfied() || condition2.IsSatisfied();
            else
                throw new Exception("No logical operator set.");
            return isSatisfied;
            */
        }
    }
}
