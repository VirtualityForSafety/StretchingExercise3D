using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    public class ConditionPublisher
    {
        // Instance
        private static readonly ConditionPublisher instance = new ConditionPublisher();
        public delegate void OnCheckDelegate(State state);
        public event OnCheckDelegate OnCheck;

        public static ConditionPublisher Instance
        {
            get
            {
                return instance;
            }
        }

        private ConditionPublisher()
        {

        }

        public void Send(State state)
        {
            if(this.OnCheck != null)
            {
                this.OnCheck(state);
            }
        }
    }

}

