using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tasc
{
    public class Button : Terminus
    {
        public bool isPushed = false;
        const string variableName = "isPushed";
        /*/
        public Button(string _name) : base(_name)
        {
            Initialize();
        }
        */

        public override void Initialize()
        {
            base.Initialize();
        }

        public override string ToString()
        {
            return base.ToString() + " : Value (" + (isPushed)+")";
        }

        public void Log()
        {
            if (GlobalLogger.isLogging == true)
            {
                GlobalLogger.addLogDataOnce(new GlobalLogger.LogDataFormat(name, GlobalLogger.DataType.BOOL, isPushed));
            }
        }

        public override Transform Control(Transform terminus, Vector3 controlVector, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            isPushed = true;
            Send();
            return terminus;
        }

        public override void Send()
        {
            ConditionPublisher.Instance.Send(new BoolVariableState(this, variableName, isPushed));
        }

        private void Start()
        {
            Initialize();
        }
    }
}