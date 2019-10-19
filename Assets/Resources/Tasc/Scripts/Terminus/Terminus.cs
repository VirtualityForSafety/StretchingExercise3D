using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Terminus : TransferElement
    {
        public Collider terminusChecker;

        private Vector3 previousPosition;        

        public virtual void Send()
        {

        }

        public virtual void Update()
        {
            ProceedWhenTransformChanged();
        }

        public virtual void ProceedWhenTransformChanged()
        {
            if (transform.hasChanged)
            {
                SendMoveState();
                transform.hasChanged = false;
            }
        }

        protected void SendMoveState()
        {
            Vector3 currPos = gameObject.transform.position;
            if (previousPosition != currPos)
            {
                ConditionPublisher.Instance.Send(new MoveState(this));
                previousPosition = currPos;
            }
        }

        public string ToJSON()
        {
            return JSONFormatter.ToJSON(this);
        }

        public virtual void Initialize()
        {
            name = transform.name;
        }

        public virtual Transform Control(Transform terminus, Vector3 controlVector, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            return null;
        }

        public virtual void UpdateInterface(Interface relatedInterface)
        {
            relatedInterface.SetInformation(ToString());
        }

        public override string ToString()
        {
            return name;
        }
    }

    
}