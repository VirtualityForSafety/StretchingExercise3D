using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    public class InteractableButton : TerminusSteamVR
    {
        public bool isPushed = false;
        const string variableName = "isPushed";
        /*/
        public Button(string _name) : base(_name)
        {
            Initialize();
        }
        */

        public override void Initialize()
        {
            base.Initialize();
        }

        public override string ToString()
        {
            return base.ToString() + " : Value (" + (isPushed) + ")";
        }

        public void Log()
        {
            if (GlobalLogger.isLogging == true)
            {
                GlobalLogger.addLogDataOnce(new GlobalLogger.LogDataFormat(name, GlobalLogger.DataType.BOOL, isPushed));
            }
        }

        public override Transform Control(Transform terminus, Vector3 controlVector, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            isPushed = true;
            Send();
            return terminus;
        }

        public override void Send()
        {
            ConditionPublisher.Instance.Send(new BoolVariableState(this, variableName, isPushed));
        }

        private void Start()
        {
            Initialize();
        }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Proceed(Hand hand)
        {
            UpdateInControl(hand);
            if (isInControl)
            {
                Transform newTrans = Control(this.transform, hand.transform.position,hand.transform.rotation);
                this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
        }
    }
}
