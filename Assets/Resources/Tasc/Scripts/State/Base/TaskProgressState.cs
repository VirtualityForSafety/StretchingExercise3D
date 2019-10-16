using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public sealed class TaskProgressState: State
    {
        public static TaskProgressState Idle = new TaskProgressState(1000, "Idle", "Task is not initiated.");
        public static TaskProgressState Started = new TaskProgressState(1001, "Started", "Task is in progress.");
        public static TaskProgressState Ended = new TaskProgressState(1002, "Ended", "Task is terminated.");
        
        public TaskProgressState(int _id, string _name, string _description)
        {
            internalStateCode = _id;
            name = _name;
            description = _description;
        }

        public override bool Equals(object obj)
        {
            if ((obj as TaskProgressState) != null)
            {
                TaskProgressState other = obj as TaskProgressState;
                return internalStateCode.Equals(other.internalStateCode);
            }
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override int CompareTo(object obj)
        {
            if ((obj as TaskProgressState) != null)
            {
                return internalStateCode.CompareTo((obj as TaskProgressState).internalStateCode);
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
