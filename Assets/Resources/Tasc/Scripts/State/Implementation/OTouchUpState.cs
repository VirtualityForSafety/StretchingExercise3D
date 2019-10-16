using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class OTouchUpState : InputState
    {
        public OTouchUpState(Terminus _sub, int _key)
        {
            name = "OTouchUpState";
            description = "Oculus Touch button up of a subject";
            subject = _sub;
            value = new Parameter<int>(_key);
        }

        public override void Update()
        {
            //value.SetValue(Vector3.Distance(subject1.gameObject.transform.position, subject2.gameObject.transform.position));
        }
    }
}
