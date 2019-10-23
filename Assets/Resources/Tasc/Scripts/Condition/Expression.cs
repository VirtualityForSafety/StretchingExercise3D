using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public abstract class Expression
    {
        public abstract bool Check(State state1, Operator ope, State state2, TimeState timeState = null);
        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void ActivateAndStartMonitoring();
    }
}
