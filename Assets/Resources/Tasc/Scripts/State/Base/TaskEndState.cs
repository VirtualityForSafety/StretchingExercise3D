using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public sealed class TaskEndState: State
    {
        public static TaskEndState None = new TaskEndState(2000, "None", "Not evaluated.");
        public static TaskEndState Correct = new TaskEndState(2001, "Correct", "Task state does not initiated.");
        public static TaskEndState Incorrect = new TaskEndState(2002, "Incorrect", "Task state initiated.");
        public static TaskEndState Interrupted = new TaskEndState(2003, "Interrupted", "Task state terminated.");

        public float progressRate;

        public TaskEndState(int _id, string _name, string _description)
        {
            internalStateCode = _id;
            name = _name;
            description = _description;
            progressRate = 0.0f;
        }

        
    }
}
