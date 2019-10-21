using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Annotation: Instruction
    {
        ModelPoser model;
        bool isVisible;

        public Annotation(List<TransferElement> givenInterfaces): base(givenInterfaces)
        {
        
        }

        public Annotation(string name, List<TransferElement> givenInterfaces) : base(name, givenInterfaces)
        {

        }

        public Annotation(Annotation another): base(another)
        {
            model = another.model;
            isVisible = another.isVisible;
        }

        public void SetModel(ModelPoser _model)
        {
            model = _model;
        }

        public void SetVisibility(bool visible)
        {
            isVisible = visible;
            if (isVisible)
                model.show();
            else
                model.hide();
            for (int i = 0; i < interfaces.Count; i++)
            {
                interfaces[i].SetVisibility(isVisible);
            }
        }

        public override void Proceed(bool isAudioEnabled = true)
        {
            if (!isVisible)
                SetVisibility(true);
            
            for(int i=0; i<interfaces.Count; i++)
            {
                if (information.GetContent(interfaces[i].type).Contains("forward bend pose"))
                    model.takePose(0);
                else if (information.GetContent(interfaces[i].type).Contains("triangle pose"))
                    model.takePose(1);
                else if (information.GetContent(interfaces[i].type).Contains("side bend stretch"))
                    model.takePose(2);
                else if (information.GetContent(interfaces[i].type).Contains("mountain pose"))
                    model.takePose(3);
                else if (information.GetContent(interfaces[i].type).Contains("neck relaxing pose"))
                    model.takePose(4);
                else
                {
                    model.hide();
                }
                if (interfaces[i].type == "LeftHandGuide")
                    interfaces[i].SetPose(model.getLeftHandPos(), Quaternion.identity);
                else if (interfaces[i].type == "RightHandGuide")
                    interfaces[i].SetPose(model.getRightHandPos(), Quaternion.identity);
                else if (interfaces[i].type == "HeadGuide")
                    interfaces[i].SetPose(model.getHeadPos(), Quaternion.identity);
            }
        }

        public override void WrapUp()
        {
            SetVisibility(false);
        }
    }
}
