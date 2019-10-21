using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    public class InteractableLever : TerminusSteamVR
    {
        public int gear;
        public float length;
        public float angle;
        public Transform pivotPoint { get; set; }

        const string variableName = "gearValue";

        /*/
        public Lever(string _name) : base(_name)
        {
            Initialize();
        }
        //*/

        public void SetPivot(Transform pivot)
        {
            pivotPoint = pivot;
        }

        public override void Initialize()
        {
            base.Initialize();
            angle = 0;
            gear = getGear(angle);
        }

        public void SetLength(float givenLength)
        {
            length = givenLength;
        }

        public override Transform Control(Transform terminus, Vector3 controlVector, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            if (givenFromDesktop)
            {
                angle += controlVector.y;
                if (angle > -87 && angle < 87)
                {
                    terminus.transform.position = pivotPoint.transform.position + (Quaternion.AngleAxis(angle - 90, Vector3.right)) * Vector3.forward * length;
                    terminus.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right) * Quaternion.AngleAxis(90, Vector3.forward);
                }
                gear = getGear(angle);
                Send();

                return terminus;
            }
            else
            {
                Vector3 projected = (controlVector - pivotPoint.transform.position);
                projected.x = 0;
                angle = -Vector3.Angle(projected, Vector3.forward) + 90;
                if (angle > -87 && angle < 87)
                {
                    terminus.transform.position = pivotPoint.transform.position + projected.normalized * length;
                    terminus.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right) * Quaternion.AngleAxis(90, Vector3.forward);
                }

                gear = getGear(angle);
                Send();

                return terminus;
            }

        }

        public void Log()
        {
            if (GlobalLogger.isLogging == true)
            {
                GlobalLogger.addLogDataOnce(new GlobalLogger.LogDataFormat(name + "_gear", GlobalLogger.DataType.INT, gear));
                GlobalLogger.addLogDataOnce(new GlobalLogger.LogDataFormat(name + "_angle", GlobalLogger.DataType.FLOAT, angle));
            }
        }

        public override string ToString()
        {
            return base.ToString() + ": Value (" + gear + ")";
        }

        int getGear(float value)
        {
            //value is from -90 ~ 90
            int gearIdx = 1;
            if (value > -90 && value < -30)
            {
                gearIdx = 1;
            }
            else if (value >= -30 && value < 30)
            {
                gearIdx = 2;
            }
            else if (value >= 30 && value < 90)
            {
                gearIdx = 3;
            }

            return gearIdx;
        }

        public override void Send()
        {
            ConditionPublisher.Instance.Send(new IntVariableState(this, variableName, gear));
        }

        private void Start()
        {
            Initialize();
        }

        public override void Awake()
        {
            base.Awake();
            pivotPoint = transform.parent.transform.Find("PivotPoint").transform;
            SetLength(Vector3.Distance(this.transform.position, pivotPoint.position));
            SetPivot(pivotPoint);
        }

        public override void Proceed(Hand hand)
        {
            UpdateInControl(hand);
            if (isInControl)
            {
                Transform newTrans = Control(this.transform, hand.transform.position, hand.transform.rotation);
                this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
        }

    }
}