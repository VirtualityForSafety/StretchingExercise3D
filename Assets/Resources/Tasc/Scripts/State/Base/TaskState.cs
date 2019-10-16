using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class TaskState : State
    {
        public Task task;
        public TaskProgressState progressState;

        public TaskState(Task _task, TaskProgressState _progressState)
        {
            task = _task;
            progressState = _progressState;
        }

        // initialize with existing task instance and its progressState
        public TaskState(Task _task)
        {
            task = _task;
            progressState = _task.state;
        }

        public override string ToString()
        {
            return "TaskState: " + task.name + "\t" + progressState;
        }

        public override bool Equals(object obj)
        {
            if((obj as TaskState) != null)
            {
                TaskState other = obj as TaskState;
                return task.name.Equals(other.task.name) && progressState.Equals(other.progressState);
            }
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override int CompareTo(object obj)
        {
            if ((obj as TaskState) != null && task.name.Equals((obj as TaskState).task.name))
            {
                TaskState other = obj as TaskState;
                return progressState.CompareTo(other.progressState);
            }
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
