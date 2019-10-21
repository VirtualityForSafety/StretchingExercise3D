using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    public class InteractableWheel : TerminusSteamVR
    {
        public static float WHEEL_MIN_LOOP_ANGLE = 15;
        public static float WHEEL_LOOP_ANGLE = 200;

        public float prevAngle;
        public float angle;
        const string variableName = "wheelAngle";
        public bool didCW;
        public bool didCCW;


        public override void Initialize()
        {
            base.Initialize();
            didCCW = false;
            didCW = false;
            angle = 0;
            prevAngle = 0;
        }

        public void SetAngle(float givenAngle)
        {
            angle = givenAngle - prevAngle;
            CalculateCWCCW();
        }

        private void CalculateCWCCW()
        {
            if (Mathf.Abs(angle) > WHEEL_LOOP_ANGLE)
            {
                if (angle > 0)
                {
                    didCW = true;
                    didCCW = false;
                }
                else
                {
                    didCW = false;
                    didCCW = true;
                }
                Send();
                prevAngle += angle;
            }

        }

        public override void Send()
        {
            if (didCW)
                ConditionPublisher.Instance.Send(new BoolVariableState(this, "didCW", didCW));
            else if (didCCW)
                ConditionPublisher.Instance.Send(new BoolVariableState(this, "didCCW", didCCW));

        }

        private void AlertCWCCW()
        {

        }

        public override string ToString()
        {
            return base.ToString() + ": Value (" + angle + ")";
        }

        public override Transform Control(Transform terminus, Vector3 controlVector, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            if (givenFromDesktop)
            {
                SetAngle(angle + controlVector.x);

                terminus.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);

                Log();
            }

            return terminus;
        }

        public void Log()
        {
            if (GlobalLogger.isLogging == true)
            {
                GlobalLogger.addLogDataOnce(new GlobalLogger.LogDataFormat(name, GlobalLogger.DataType.FLOAT, angle));
            }
        }

        private void Start()
        {
            Initialize();
        }

        CircularDrive circularDrive;
        public Transform pivotPoint { get; set; }

        public override void Awake()
        {
            base.Awake();
            circularDrive = GetComponent<CircularDrive>();
        }

        public override void Proceed(Hand hand)
        {
            UpdateInControl(hand);
            if (isInControl)
            {
                Control(this.transform, hand.transform.position, hand.transform.rotation);
                SetAngle(circularDrive.outAngle);
            }
        }
    }
}