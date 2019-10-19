using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    public class FeedbackSteamVR : FeedbackDT
    {
        public List<Interface> interfaces;

        protected bool isInControl;

        public virtual void Awake()
        {
            if (transform != null)
            {
                name = transform.name;
            }
            isInControl = false;
        }

        public override void OnMouseDrag()
        {
            if (PlatformSelector.GetCurrentPlatform() == Platform.Desktop)
            {
                base.OnMouseDrag();
                //Debug.Log(dragDirection);
                Transform newTrans = terminus.Control(this.transform, currentPosition,Quaternion.identity, true);
                if(newTrans)
                    this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
        }

        private void Update()
        {
            UpdateInstructions(interfaces);
        }

        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            //Debug.Log("Hovering hand: " + hand.name);
        }


        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {

        }

        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {
            //Debug.Log("Hovering hand: " + hand.name);
            Proceed(hand);
        }

        private GrabTypes grabbedWithType;
        public virtual void Proceed(Hand hand)
        {
            UpdateInControl(hand);
            //*
            if (isInControl)
            {
                Transform newTrans = terminus.Control(this.transform, hand.transform.position, hand.transform.rotation);
                if (newTrans)
                    this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
            //*/
        }

        protected void UpdateInControl(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting(); // ~SteamVR Plugin v2.3.2
            bool isGrabEnding = hand.IsGrabbingWithType(grabbedWithType) == false;

            if (startingGrabType != GrabTypes.None)
            {
                grabbedWithType = startingGrabType;
                isInControl = true;
            }
            else if(grabbedWithType != GrabTypes.None && isGrabEnding)
            {
                isInControl = false;
            }
        }

        public void UpdateInstructions(List<Interface> interfaces)
        {
            for (int i = 0; i < interfaces.Count; i++)
            {
                terminus.UpdateInterface(interfaces[i]);
            }
        }

        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        private void OnAttachedToHand(Hand hand)
        {
            //textMesh.text = "Attached to hand: " + hand.name;
        }


        //-------------------------------------------------
        // Called when this GameObject is detached from the hand
        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        private void HandAttachedUpdate(Hand hand)
        {
            //textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + ( Time.time - attachTime ).ToString( "F2" );
        }


        //-------------------------------------------------
        // Called when this attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusAcquired(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called when another attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusLost(Hand hand)
        {
        }
    }
}


