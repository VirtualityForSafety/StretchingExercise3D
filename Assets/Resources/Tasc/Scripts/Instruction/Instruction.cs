using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Instruction
    {
        public enum TaskContext { None, Training, Tutorial, Assessment };
        public Information information;
        //public string content;
        public string name;

        int narrationInterval;
        private bool isNarrationStarted = false;
        private bool isNarrationEnded = false;
        public Dictionary<string, int> contextLookup;

        public Instruction()
        {
            name = "";
            information = new Information();
            contextLookup = new Dictionary<string, int>();
        }

        public Instruction(string givenTitle)
        {
            name = givenTitle;
            information = new Information();
        }

        public void SetDefaultTitleAndDescription(string title, string inputContent)
        {
            SetContentWithContext(title, Information.Context.Title);
            SetDefaultDescription(inputContent);
        }

        public void SetDefaultDescription(string inputContent)
        {
            SetContentWithContext(inputContent, Information.Context.Narration);
            SetContentWithContext(inputContent, Information.Context.Description);
        }

        public void SetContentWithContext(string inputContent, Information.Context context)
        {
            information.SetContent(inputContent, context);
        }

        public string GetContentWithContext(Information.Context context)
        {
            return information.GetContent(context);
        }

        public void Proceed(List<Interface> interfaces, bool isAudioEnabled = true)
        {
            if (!isNarrationStarted)
            {
                //Narrator.speak("안녕하십니까요.");
                for(int i=0; i< interfaces.Count; i++)
                {
                    if(interfaces[i] is VoiceInterface)
                    {
                        if (isAudioEnabled)
                            interfaces[i].Transfer(information.GetContent(interfaces[i].context));
                    }
                    else
                    {
                        if(interfaces[i]!=null)
                            interfaces[i].Transfer(information.GetContent(interfaces[i].context));
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

        public bool isAudioInstructionEnded()
        {
            return isNarrationEnded;
        }

        
    }
}