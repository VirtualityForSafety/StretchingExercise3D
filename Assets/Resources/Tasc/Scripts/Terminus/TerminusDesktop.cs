using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tasc
{
    // support for desktop version
    public class TerminusDesktop : Terminus
    {
        public float strength = 10.0f;
        public Vector2 lastPosition = Vector2.zero;
        public Vector2 dragDirection = Vector2.zero;
        public Vector2 currentPosition = Vector2.zero;
        public Texture2D normalHand;
        public Texture2D grabHand;
        public bool isControllable;

        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        protected void Start()
        {
            //Load a text file (Assets/Resources/Text/textFile01.txt)
            normalHand = Resources.Load<Texture2D>("Tasc/Texture/handfinger");
            grabHand = Resources.Load<Texture2D>("Tasc/Texture/handgrab");
            isControllable = false;
        }

        public void OnMouseEnter()
        {
            Cursor.SetCursor(normalHand, hotSpot, cursorMode);
        }

        public void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        public virtual void OnMouseDrag()
        {
            currentPosition = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y")) * strength;
            dragDirection = currentPosition - lastPosition;
            lastPosition = currentPosition;
            if (isControllable)
            {
                Transform newTrans = Control(this.transform, currentPosition, Quaternion.identity, true);
                if (newTrans)
                    this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
        }

        public void OnMouseDown()
        {
            Cursor.SetCursor(grabHand, hotSpot, cursorMode);
        }

        public void OnMouseUp()
        {
            Cursor.SetCursor(normalHand, hotSpot, cursorMode);
        }
    }
}

