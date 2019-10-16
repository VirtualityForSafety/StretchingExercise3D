using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class DesktopActor : Actor
    {
        public override void Update()
        {
            base.Update();
            HandleKeyInput();
        }

        // Update is called once per frame
        void HandleKeyInput()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Z))
            {
                //Condition.Instance.Evaluate(new InputUpState(this, (int)UnityEngine.KeyCode.A));
                ConditionPublisher.Instance.Send(new InputDownState(this, (int)UnityEngine.KeyCode.Z));
            }
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.X))
            {
                ConditionPublisher.Instance.Send(new InputDownState(this, (int)UnityEngine.KeyCode.X));
            }
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.C))
            {
                ConditionPublisher.Instance.Send(new InputDownState(this, (int)UnityEngine.KeyCode.C));
            }
            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.Z))
            {
                //Condition.Instance.Evaluate(new InputUpState(this, (int)UnityEngine.KeyCode.A));
                ConditionPublisher.Instance.Send(new InputUpState(this, (int)UnityEngine.KeyCode.Z));
            }
            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.X))
            {
                ConditionPublisher.Instance.Send(new InputUpState(this, (int)UnityEngine.KeyCode.X));
            }
            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.C))
            {
                ConditionPublisher.Instance.Send(new InputUpState(this, (int)UnityEngine.KeyCode.C));
            }
        }
    }
}