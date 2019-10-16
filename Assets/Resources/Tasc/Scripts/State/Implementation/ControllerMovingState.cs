using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class ControllerMovingState : InputState
    {
        public ControllerMovingState(Terminus _sub, int _key)
        {
            name = "ControllerMovingState";
            description = "Moving state of controller(s).";
            subject = _sub;
            value = new Parameter<int>(_key);
        }

        public override void Update()
        {
            
        }
    }
}