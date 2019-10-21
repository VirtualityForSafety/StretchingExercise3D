using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tasc
{
    public class VisualInterface : Interface
    {
        public override void SetInformation(string msg)
        {
            Set3DText(msg);
            Set2DText(msg);
        }

        public virtual void Set3DText(string givenText)
        {
            if (this.GetComponent<TextMesh>() != null)
            {
                this.GetComponent<TextMesh>().text = givenText;
            }
        }

        public virtual void Set2DText(string givenText)
        {
            if (this.GetComponent<Text>() != null)
            {
                this.GetComponent<Text>().text = givenText;
            }
        }
    }
}


