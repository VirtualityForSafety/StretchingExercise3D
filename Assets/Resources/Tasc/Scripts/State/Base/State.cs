using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public abstract class State: IComparable
    {
        public int internalStateCode = -1;

        public string name { set; get; }
        public string description { set; get; }
        public bool shouldLog;

        public delegate void OnStateChangeDelegate(State newState);
        public event OnStateChangeDelegate OnStateChange;

        public virtual int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual void Update() { }

        public override string ToString()
        {
            return name;
        }
    }
}
