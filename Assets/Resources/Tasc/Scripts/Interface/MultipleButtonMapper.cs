using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class MultipleButtonMapper : VisualInterface
    {
        public List<InteractableButton> interactableButtons;
        private int recentlyPushedIdx = -1;
        private string result = "";
        public override void SetInformation(string msg)
        {
            for (int i=0; i< interactableButtons.Count; i++)
            {
                if(interactableButtons[i].isPushed){
                    if (i == recentlyPushedIdx)
                        interactableButtons[i].isPushed = false;
                    else
                    {
                        recentlyPushedIdx = i;
                        result = interactableButtons[i].name;
                    }
                }
            }
            Set3DText("Pushed: " + (result ==""? "None":result));
        }
    }
}

