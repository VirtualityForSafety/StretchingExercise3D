using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public abstract class Expression
    {
        protected abstract bool Check(State state1, Operator ope, State state2, TimeState timeState = null);
        public abstract bool CheckPassive();
        public abstract bool CheckActive(State state, TimeState timeState = null);
        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void ActivateAndStartMonitoring();
        public abstract bool IsSatisfied();
        public abstract bool IsActivated();
    }
}
