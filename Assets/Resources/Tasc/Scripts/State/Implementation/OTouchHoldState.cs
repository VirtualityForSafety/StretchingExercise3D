using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class OTouchHoldState : InputState
    {
        public OTouchHoldState(Terminus _sub, int _key)
        {
            name = "OTouchHoldState";
            description = "Oculus Touch button hold of a subject";
            subject = _sub;
            value = new Parameter<int>(_key);
        }

        public override void Update()
        {
            
        }
    }
}
