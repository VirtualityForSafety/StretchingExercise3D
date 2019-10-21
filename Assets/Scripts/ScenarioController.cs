using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class ScenarioController : MonoBehaviour
    {
        bool isTerminusExported = false;
        bool isActionExported = false;
        Scenario scenario = new Scenario("Stretching exercise", "A user is required to memorize five stretching poses in order.");

        public List<TransferElement> interfaces;
        public Actor actor;

        // Use this for initialization
        void Start()
        {
            InitializeScenario();
        }

        void InitializeScenario()
        {
            MakeTestScenario();
        }

        void MakeTestScenario()
        {
            ////////////// actor null exception 
            GuideBall leftHandBall = GameObject.Find("LeftHandPose").GetComponent<GuideBall>();
            GuideBall rightHandBall = GameObject.Find("RightHandPose").GetComponent<GuideBall>();
            GuideBall headBall = GameObject.Find("HeadPose").GetComponent<GuideBall>();

            Valve.VR.InteractionSystem.Hand[] hands = FindObjectsOfType<Valve.VR.InteractionSystem.Hand>();
            for(int i=0; i<hands.Length; i++)
            {
                if (hands[i].handType == Valve.VR.SteamVR_Input_Sources.LeftHand)
                    leftHandBall.target = hands[i].transform;
                else if (hands[i].handType == Valve.VR.SteamVR_Input_Sources.RightHand)
                    rightHandBall.target = hands[i].transform;
            }
            headBall.target = GameObject.Find("HeadCollider").transform;

            List<TransferElement> terminusesForAnnotation = new List<TransferElement>();
            terminusesForAnnotation.Add(leftHandBall);
            terminusesForAnnotation.Add(rightHandBall);
            terminusesForAnnotation.Add(headBall);
            Annotation annotation = new Annotation("Body parts guidance", terminusesForAnnotation);
            ModelPoser modelPoser = FindObjectOfType<ModelPoser>();
            annotation.SetModel(modelPoser);
            annotation.SetVisibility(false);

            Task introduction = new Task("Intro", "");
            Instruction introInstruction = new Instruction(introduction.name, interfaces);
            introInstruction.SetContent("title", "Tutorial");
            introInstruction.SetContent("narration", "Hi. Let's start stretching exercise. In this training you are asked to remember five poses in order.");
            introInstruction.SetContent("description", "Hi. Let's start stretching exercise.\nIn this training you are asked to remember five poses in order.");
            introduction.AddInstruction(introInstruction);
            introduction.exit = new Condition(new TaskState(introduction, TaskProgressState.Started), Condition.RelationalOperator.Equal, new TimeState(0,0,3));
            scenario.Add(introduction);

            Task task1 = new Task("Task1", "");
            Instruction task1Instruction = new Instruction(task1.name, interfaces);
            task1Instruction.SetContent("title", "Forward bend pose");
            task1Instruction.SetContent("narration", "First, forward bend pose. In a standing position, lean forward and extend your arms downwards. Grip controller when you are done.");
            task1Instruction.SetContent("description", "First, forward bend pose. \nIn a standing position, lean forward and extend your arms downwards. \nGrip controller when you are done.");
            task1.AddInstruction(task1Instruction);
            Annotation annotation1 = new Annotation(annotation);
            annotation1.SetContent("rightHandGuide","forward bend pose");
            annotation1.SetContent("leftHandGuide", "forward bend pose");
            annotation1.SetContent("headGuide", "forward bend pose");
            task1.AddInstruction(annotation1);
            task1.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task1);

            Task task2 = new Task("Task2", "");
            Instruction task2Instruction = new Instruction(task2.name, interfaces);
            task2Instruction.SetContent("title", "Triangle pose");
            task2Instruction.SetContent("narration", "Second, triangle pose. Tilt your waist to the left with your left arm lowered to the floor. In this state, your right arm is fully extended in the direction of the sky and your gaze is directed at the tip of your right hand. Grip controller when you are done.");
            task2Instruction.SetContent("description", "Second, triangle pose. \nTilt your waist to the left with your left arm lowered to the floor. \nIn this state, your right arm is fully extended in the direction of the sky \nand your gaze is directed at the tip of your right hand. \nGrip controller when you are done.");
            task2.AddInstruction(task2Instruction);
            Annotation annotation2 = new Annotation(annotation);
            annotation2.SetContent("rightHandGuide", "triangle pose");
            annotation2.SetContent("leftHandGuide", "triangle pose");
            annotation2.SetContent("headGuide", "triangle pose");
            task2.AddInstruction(annotation2);
            task2.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task2);

            Task task3 = new Task("Task3", "");
            Instruction task3Instruction = new Instruction(task3.name, interfaces);
            task3Instruction.SetContent("title", "Side bend stretch");
            task3Instruction.SetContent("narration", "Third, side bend stretch. Place your right hand on your right waist. Keep your left arm straight in the sky. Slowly tilt your upper body to the right in this position. Grip controller when you are done.");
            task3Instruction.SetContent("description", "Third, side bend stretch. \nPlace your right hand on your right waist. \nKeep your left arm straight in the sky. \nSlowly tilt your upper body to the right in this position.");
            task3.AddInstruction(task3Instruction);
            Annotation annotation3 = new Annotation(annotation);
            annotation3.SetContent("rightHandGuide", "side bend stretch");
            annotation3.SetContent("leftHandGuide", "side bend stretch");
            annotation3.SetContent("headGuide", "side bend stretch");
            task3.AddInstruction(annotation3);
            task3.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task3);

            Task task4 = new Task("Task4", "");
            Instruction task4Instruction = new Instruction(task4.name, interfaces);
            task4Instruction.SetContent("title", "Mountain pose");
            task4Instruction.SetContent("narration", "Fourth, mountain pose. With your shoulders relaxed, place your arms above your head in an upright position and stretch your arms all the way up. Point your eyes forward. Grip controller when you are done.");
            task4Instruction.SetContent("description", "Fourth, mountain pose. \nWith your shoulders relaxed, place your arms above your head in an upright position \nand stretch your arms all the way up. Point your eyes forward.");
            task4.AddInstruction(task4Instruction);
            Annotation annotation4 = new Annotation(annotation);
            annotation4.SetContent("rightHandGuide", "mountain pose");
            annotation4.SetContent("leftHandGuide", "mountain pose");
            annotation4.SetContent("headGuide", "mountain pose");
            task4.AddInstruction(annotation4);
            task4.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task4);

            Task task5 = new Task("Task5", "");
            Instruction task5Instruction = new Instruction(task5.name, interfaces);
            task5Instruction.SetContent("title", "Neck relaxing pose");
            task5Instruction.SetContent("narration", "Fifth, neck relaxing pose. Place your hands on your back in a standing position. In this position, turn your head and eyes toward the sky. Grip controller when you are done.");
            task5Instruction.SetContent("description", "Fifth, neck relaxing pose. \nPlace your hands on your back in a standing position. \nIn this position, turn your head and eyes toward the sky. ");
            task5.AddInstruction(task5Instruction);
            Annotation annotation5 = new Annotation(annotation);
            annotation5.SetContent("rightHandGuide", "neck relaxing pose");
            annotation5.SetContent("leftHandGuide", "neck relaxing pose");
            annotation5.SetContent("headGuide", "neck relaxing pose");
            task5.AddInstruction(annotation5);
            task5.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task5);

            Task ending = new Task("Finish", "");
            Instruction endInstruction = new Instruction(ending.name, interfaces);
            endInstruction.SetContent("title", "Finish");
            endInstruction.SetContent("narration", "Well done! Your training is successfully terminated.");
            endInstruction.SetContent("description", "Well done! Your training is successfully terminated.");
            ending.AddInstruction(endInstruction);
            ending.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(ending);

            scenario.MakeProcedure();

            scenario.Activate();
        }

        // Update is called once per frame
        void Update()
        {
            if (scenario != null)
                scenario.Proceed();
        }
    }
}

