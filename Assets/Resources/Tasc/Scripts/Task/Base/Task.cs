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
        public Condition entrance;
        public Terminus target;
        public Task action;
        public Condition exit;
        public Instruction instruction;
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

        public bool Proceed(List<Interface> interfaces)
        {
            if (entrance == null || exit == null)
                throw new MissingComponentException();

            if (!isActivated)
                return false;
            if (state == TaskProgressState.Idle)
            {
                if(entrance.Check()){
                    state = TaskProgressState.Started;
                    startingTime = new TimeState(TimeState.GetGlobalTimer());
                    cantSkipInterval = GlobalConstraint.TASK_CANT_SKIP_INTERVAL;
                    entrance.Deactivate();
                    exit.ActivateAndStartMonitoring();
                }
            }
            else if (state == TaskProgressState.Started)
            {
                if (interfaces.Count > 0)
                {
                    instruction.Proceed(interfaces);
                    if (!instruction.isAudioInstructionEnded())
                        cantSkipInterval--;
                }                    
                if (exit.Check())// && cantSkipInterval < 0)
                {
                    state = TaskProgressState.Ended;
                    MoveNext(Evaluate());
                }
                
            }
            return exit.isSatisfied;
        }
    }
}