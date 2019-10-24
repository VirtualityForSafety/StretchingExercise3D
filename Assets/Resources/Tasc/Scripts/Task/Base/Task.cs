using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Task: PrimitiveTask
    {
        public string name;
        public string description;
        public int priority;
        public bool isActivated;
        public Terminus actor;
        public Expression entrance;
        public Terminus target;
        public Task action;
        public Expression exit;
        public List<Instruction> instructions;
        public Dictionary<TaskEndState, Task> next;
        public TimeState startingTime;
        
        int cantSkipInterval;
        
        public Task()
        {
            if(state==null)
                state = TaskProgressState.Idle;
            if(taskResult==null)
                taskResult = TaskEndState.None;
            state.OnStateChange += StateChangeHandler;
            isActivated = false;
            next = new Dictionary<TaskEndState, Task>();
            entrance = Condition.DummyCondition;
            instructions = new List<Instruction>();
        }

        public Task(bool _isActivated): this()
        {
            isActivated = _isActivated;
        }

        public bool HasFinished()
        {
            return !isActivated && state == TaskProgressState.Ended;
        }

        private void StateChangeHandler(State newState){
            Debug.Log(TimeState.GetGlobalTimer() + "\tTask: "+name+"\tTaskProgressState: " + newState.ToString());
            ConditionPublisher.Instance.Send(new TaskState(this, newState as TaskProgressState));
        }

        public override string ToString()
        {
            return name + ": " + description;
        }

        public Task(string _name, string _description) : this()
        {
            name = _name;
            description = _description;
        }

        public TaskEndState Evaluate(){
            return TaskEndState.Correct;
        }

        public void SetNext(TaskEndState taskEndState, Task task)
        {
            if(next != null && task != null)
            {
                next.Add(taskEndState, task);
            }
        }

        public void MoveNext(TaskEndState taskEndState)
        {
            Deactivate();
            if (taskEndState != TaskEndState.None && next.ContainsKey(taskEndState) && next[taskEndState]!= null)
                next[taskEndState].Activate();
        }

        public void AddInstruction(Instruction instruction)
        {
            instructions.Add(instruction);
        }

        public void Activate()
        {
            if (!isActivated)
            {
                isActivated = true;
                OnStateChange += StateChangeHandler;
                entrance.ActivateAndStartMonitoring();
            }
        }

        public void Deactivate()
        {
            if (isActivated)
            {
                isActivated = false;
                OnStateChange -= StateChangeHandler;
                entrance.Deactivate();
                exit.Deactivate();
            }
        }

        public bool Proceed()
        {
            if (entrance == null || exit == null)
                throw new MissingComponentException();
            if (!isActivated)
                return false;

            bool resultFromExit = false;
            if (state == TaskProgressState.Idle)
            {
                if(entrance.CheckPassive()){
                    state = TaskProgressState.Started;
                    startingTime = new TimeState(TimeState.GetGlobalTimer());
                    cantSkipInterval = GlobalConstraint.TASK_CANT_SKIP_INTERVAL;
                    entrance.Deactivate();
                    exit.ActivateAndStartMonitoring();
                }
            }
            else if (state == TaskProgressState.Started)
            {
                for (int i = 0; i < instructions.Count; i++)
                {
                    instructions[i].Proceed();
                    if (!instructions[i].isAudioInstructionEnded())
                        cantSkipInterval--;
                }
                exit.CheckPassive();
                resultFromExit = exit.IsSatisfied();
                if (resultFromExit && cantSkipInterval < 0)
                {
                    TaskEndState evaluateResult = Evaluate();
                    if(evaluateResult == TaskEndState.Correct)
                    {
                        state = TaskProgressState.Ended;
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            instructions[i].WrapUp();
                        }
                        MoveNext(evaluateResult);
                    }
                }
            }
            return resultFromExit;
        }
    }
}