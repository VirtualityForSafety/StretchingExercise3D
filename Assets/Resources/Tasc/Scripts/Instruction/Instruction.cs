using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Instruction
    {
        public enum TaskContext { None, Training, Tutorial, Assessment };
        public Information information;
        public string name;
        protected List<TransferElement> interfaces;
        int narrationInterval;
        private bool isNarrationStarted = false;
        private bool isNarrationEnded = false;

        public Instruction(List<TransferElement> givenInterfaces)
        {
            name = "";
            interfaces = givenInterfaces;
            information = new Information();
        }

        public Instruction(string givenTitle, List<TransferElement> givenInterfaces)
        {
            name = givenTitle;
            interfaces = givenInterfaces;
            information = new Information();
        }

        public Instruction(Instruction another)
        {
            name = another.name;
            information = new Information(another.information);
            interfaces = new List<TransferElement>();
            for(int i=0; i< another.interfaces.Count; i++)
                interfaces.Add(another.interfaces[i]); 
        }

        public void SetContent(string context, string inputContent)
        {
            information.SetContent(context, inputContent);
        }

        public string GetContent(string context)
        {
            return information.GetContent(context);
        }

        public virtual void Proceed(bool isAudioEnabled = true)
        {
            if (!isNarrationStarted)
            {
                for(int i=0; i< interfaces.Count; i++)
                {
                    if(interfaces[i] is VoiceInterface)
                    {
                        if (isAudioEnabled)
                            interfaces[i].SetInformation(information.GetContent(interfaces[i].type));
                    }
                    else
                    {
                        if(interfaces[i]!=null)
                            interfaces[i].SetInformation(information.GetContent(interfaces[i].type));
                    }
                }
                
                isNarrationStarted = true;
                narrationInterval = GlobalConstraint.NARRATION_INTERVAL;
            }
            else
            {
                if (!isNarrationEnded && narrationInterval < 0 ) // && !AudioInformation.isSpeaking())
                    isNarrationEnded = false;
                narrationInterval--;
            }
        }

        public virtual void WrapUp()
        {

        }

        public bool isAudioInstructionEnded()
        {
            return isNarrationEnded;
        }

        
    }
}