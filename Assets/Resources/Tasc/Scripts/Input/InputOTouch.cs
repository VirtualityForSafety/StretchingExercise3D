using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class InputOTouch : MonoBehaviour
    {
        private Quaternion prevControllerRotation;
        private Vector3 prevControllerPosition;
        //public Vector3 veloDirection;
        private float velocityMagnitute;

        public bool isGrabing
        {
            get; set;
        }

        public Valve.VR.InteractionSystem.GrabTypes grabType
        {
            get; set;
        }

        public float GetSwingMagnitude()
        {
            return (isGrabing) ? velocityMagnitute : 0;
        }

        private void Start()
        {
            isGrabing = false;
        }

        private void Update()
        {
            float rotDiff = Quaternion.Angle(this.transform.localRotation, prevControllerRotation);
            Vector3 posDiff = this.transform.position - prevControllerPosition;
            prevControllerRotation = this.transform.localRotation;
            prevControllerPosition = this.transform.position;
            velocityMagnitute = rotDiff * rotDiff * 0.05f;
        }
    }
}
