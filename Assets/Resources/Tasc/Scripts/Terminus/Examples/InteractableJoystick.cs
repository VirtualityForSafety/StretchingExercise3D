using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    public class InteractableJoystick : TerminusSteamVR
    {
        Vector3 value;
        const string variableName = "leverCoord";
        public float leverLength { get; set; }
        public Transform pivotPoint { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            value = Vector3.zero;
        }

        public void SetPivot(Transform pivot)
        {
            pivotPoint = pivot;
        }

        public void SetLength(float length)
        {
            leverLength = length;
        }

        public void Log()
        {
            //Logging system
            if (GlobalLogger.isLogging == true)
            {
                GlobalLogger.addLogDataOnce(new GlobalLogger.LogDataFormat(name, GlobalLogger.DataType.VEC3, value));
            }
        }

        public override Transform Control(Transform terminus, Vector3 controlVector, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            if (givenFromDesktop)
            {
                value += new Vector3(controlVector.x, controlVector.y, 0);

                if (value.x > -87 && value.x < 87)
                {
                    terminus.transform.position = pivotPoint.transform.position + ((Quaternion.AngleAxis(-value.x + 90, Vector3.forward) * Vector3.right + Quaternion.AngleAxis(value.y - 90, Vector3.right) * Vector3.forward) - Vector3.up) * leverLength; //(Quaternion.AngleAxis(value.y - 90, Vector3.right))) * Vector3.right  * leverLength ;
                    terminus.transform.rotation = Quaternion.AngleAxis(value.y, Vector3.right) * Quaternion.AngleAxis(-value.x, Vector3.forward);
                    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
                }
                Send();

                return terminus;
            }
            else
            {
                Vector3 projected = (controlVector - pivotPoint.transform.position);
                //Debug.Log("projected = [" + projected.x + ", " + projected.y + ", " + projected.z + "]");
                float angleX = -Vector3.Angle(projected, Vector3.forward) + 90;
                float angleY = Vector3.Angle(projected, Vector3.right) - 90;
                if (angleX > -87 && angleX < 87)
                {
                    terminus.transform.position = pivotPoint.transform.position + projected.normalized * leverLength;
                    terminus.transform.rotation = Quaternion.AngleAxis(angleX, Vector3.right) * Quaternion.AngleAxis(angleY, Vector3.forward);
                    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
                }
                value = new Vector3(-angleY, angleX);
                Send();

                return terminus;
            }
        }

        public override void Send()
        {
            ConditionPublisher.Instance.Send(new VectorVariableState(this, variableName, value));
        }

        public override string ToString()
        {
            return base.ToString() + ": Value (" + (int)(value.x) + ", " + (int)(value.y) + ", " + (int)(value.z) + ")";
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
                Debug.Log(hand);
                Transform newTrans = Control(this.transform, hand.transform.position, hand.transform.rotation);
                this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
        }
    }
}

