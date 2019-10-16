using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class OTouchDownState : InputState
    {
        public OTouchDownState(Terminus _sub, int _key)
        {
            name = "OTouchDownState";
            description = "Oculus Touch button down of a subject";
            subject = _sub;
            value = new Parameter<int>(_key);
        }

        public override void Update()
        {
            
        }
    }
}
