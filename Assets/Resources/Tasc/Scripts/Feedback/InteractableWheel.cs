using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    [RequireComponent(typeof(Tasc.Wheel))]
    public class InteractableWheel : FeedbackSteamVR
    {
        CircularDrive circularDrive;
        public Transform pivotPoint { get; set; }

        public override void Awake()
        {
            base.Awake();
            terminus = GetComponent<Wheel>();
            circularDrive = GetComponent<CircularDrive>();
        }

        public override void Proceed(Hand hand)
        {
            UpdateInControl(hand);
            if (isInControl)
            {
                terminus.Control(this.transform, hand.transform.position, hand.transform.rotation);
                (terminus as Wheel).SetAngle(circularDrive.outAngle);
            }
        }

        public override void UpdateInstructions(List<Interface> interfaces)
        {
            for (int i = 0; i < interfaces.Count; i++)
            {
                terminus.UpdateInformation();
                interfaces[i].Transfer(terminus.information.GetContent(interfaces[i].context));
            }
        }
    }
}