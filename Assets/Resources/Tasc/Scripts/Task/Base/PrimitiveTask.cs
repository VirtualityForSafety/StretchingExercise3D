using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class PrimitiveTask
    {
        public enum Type
        {
            None = 0,
            Create = 1,
            Destroy = 2,
            ActiveRender = 3,
            DeactiveRender = 4,
            SetPosition = 5,
            SumPosition = 6,
            SetRotation = 7,
            SumRotation = 8,
            SetScale = 9,
            SumScale = 10,
            SetBool = 11,
            SetFloat = 12,
            SumFloat = 13,
            EnableAT = 14,
            DisableAT = 15,
            SoundOn = 16,
            SoundOff = 17,
            StartParticle = 18,
            StopParticle = 19,
            AttachToCamera = 20,
            // high
            EnableWalking = 21,
            DisableWalking = 22,
            Move = 23,
            Manipulate = 24
        }
        public Type type;
        public TaskEndState taskResult;
        private TaskProgressState internalState;
        public TaskProgressState state
        {
            get { return internalState; }
            set
            {
                if (internalState == value) return;
                internalState = value;
                if (OnStateChange != null)
                    OnStateChange(internalState);
            }
        }

        public delegate void OnStateChangeDelegate(State newState);
        public event OnStateChangeDelegate OnStateChange;
    }

}
