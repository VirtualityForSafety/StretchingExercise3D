using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    public class WalkingBySwing : Action
    {
        // Player's speed
        public float forwardSpeed = 3.8f;
        public float backwardSpeed = 2.0f;
        public float rotateSpeed = 1.0f;
        public float maxSpeed = 6.0f;
        Vector3 moveDirection;

        private OculusActor actor;
        public WalkingBySwing(OculusActor _actor) : base(Type.WalkingBySwing, new Condition(new BoolVariableState(_actor, "isWalkable", false), RelationalOperator.Equal))
        {
            actor = _actor;
        }

        public void Walk(Transform transform)
        {
            float walkingSpeed = 0;

            Hand[] hands = actor.GetHands();
            for(int i=0; i<hands.Length; i++)
            {
                walkingSpeed += hands[i].GetComponent<InputOTouch>().GetSwingMagnitude();
            }
            MakeMove(transform, walkingSpeed);
        }

        private void MakeMove(Transform transform, float walkingSpeed)
        {
            Vector2 inputVec = new Vector2(1, 0);

            walkingSpeed = Mathf.Min(walkingSpeed, maxSpeed);
            // always move along the camera forward as it is the direction that it being aimed at
            Quaternion targetRot = Camera.main.transform.rotation * Quaternion.Inverse(transform.rotation);
            Vector3 targetForward = targetRot * Vector3.forward;
            Vector3 targetRight = targetRot * Vector3.right;


            Vector3 desiredMove = targetForward * inputVec.y + targetRight * inputVec.x;
            //Vector3 desiredMove = m_Camera.transform.forward * m_Input.x + m_Camera.transform.right * m_Input.y;
            //Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it

            moveDirection.x = desiredMove.x * walkingSpeed;
            moveDirection.z = desiredMove.z * walkingSpeed;
            transform.position += moveDirection * Time.fixedDeltaTime;
        }
    }
}
