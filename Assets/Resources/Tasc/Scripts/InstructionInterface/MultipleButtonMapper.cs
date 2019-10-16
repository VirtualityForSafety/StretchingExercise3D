using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class MultipleButtonMapper : Annotation
    {
        public List<InteractableButton> interactableButtons;
        private int recentlyPushedIdx = -1;
        private string result = "";
        public override void Transfer(string msg)
        {
            for (int i=0; i< interactableButtons.Count; i++)
            {
                if((interactableButtons[i].terminus as Button).isPushed){
                    if (i == recentlyPushedIdx)
                        (interactableButtons[i].terminus as Button).isPushed = false;
                    else
                    {
                        recentlyPushedIdx = i;
                        result = interactableButtons[i].terminus.name;
                    }
                }
            }
            Set3DText("Pushed: " + (result ==""? "None":result));
        }
    }

}

