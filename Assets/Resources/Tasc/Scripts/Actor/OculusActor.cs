using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    public class OculusActor : Actor
    {
        public bool isWalkable = true;
        Hand[] hands;
        List<Action> actions;

        public void Start()
        {
            hands = GetComponentsInChildren<Hand>();
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i].gameObject.AddComponent<InputOTouch>();
            }

            actions = new List<Action>();
            actions.Add(new WalkingBySwing(this));
        }

        public override void Update()
        {
            base.Update();
            HandleControllerInput();
            HandleActions();
        }

        public Hand[] GetHands()
        {
            return hands;
        }

        public void SetActorUnwalkable()
        {
            ConditionPublisher.Instance.Send(new BoolVariableState(this, "isWalkable", false));
        }

        public void SetActorWalkable()
        {
            ConditionPublisher.Instance.Send(new BoolVariableState(this, "isWalkable", true));
        }

        private void HandleControllerInput()
        {
            for(int i=0; i<hands.Length; i++)
            {
                GrabTypes starting = hands[i].GetGrabStarting();
                if (starting != GrabTypes.None)
                {
                    hands[i].GetComponent<InputOTouch>().isGrabing = true;
                    hands[i].GetComponent<InputOTouch>().grabType = starting;
                    ConditionPublisher.Instance.Send(new OTouchDownState(this, (int)starting));
                }

                GrabTypes ending = hands[i].GetGrabEnding();
                if (ending != GrabTypes.None)
                {
                    hands[i].GetComponent<InputOTouch>().isGrabing = false;
                    ConditionPublisher.Instance.Send(new OTouchUpState(this, (int)ending));
                }
                else
                {
                    if(hands[i].GetComponent<InputOTouch>().isGrabing)
                        ConditionPublisher.Instance.Send(new OTouchHoldState(this, (int)hands[i].GetComponent<InputOTouch>().grabType));
                }
            }
        }

        private void HandleActions()
        {
            for(int i=0; i<actions.Count; i++)
            {
                if(actions[i] as WalkingBySwing != null)
                {
                    (actions[i] as WalkingBySwing).Walk(this.transform);
                }
            }
        }
    }
}