using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    [RequireComponent(typeof(Tasc.Joystick))]
    public class InteractableJoystick : FeedbackSteamVR
    {
        public Transform pivotPoint { get; set; }
        
        public override void Awake()
        {
            base.Awake();
            terminus = GetComponent<Joystick>();
            pivotPoint = transform.parent.transform.Find("PivotPoint").transform;
            (terminus as Joystick).SetLength(Vector3.Distance(this.transform.position, pivotPoint.position));
            (terminus as Joystick).SetPivot(pivotPoint);
        }

        public override void Proceed(Hand hand)
        {
            UpdateInControl(hand);            
            if (isInControl)
            {
                Debug.Log(hand);
                Transform newTrans = terminus.Control(this.transform, hand.transform.position, hand.transform.rotation);
                this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
        }

        public override void UpdateInstructions(List<Interface> interfaces)
        {
            for(int i=0; i< interfaces.Count; i++)
            {
                terminus.UpdateInformation();
                interfaces[i].Transfer(terminus.information.GetContent(interfaces[i].context));
            }
        }
    }
}

