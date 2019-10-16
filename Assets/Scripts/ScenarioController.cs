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

        public List<Interface> interfaces;
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

            Task introduction = new Task("Intro", "");
            introduction.instruction = new Instruction(introduction.name);
            introduction.instruction.SetContentWithContext("Tutorial", Information.Context.Title);
            introduction.instruction.SetContentWithContext("Hi. Let's start stretching exercise. In this training you are asked to remember five poses in order.", Information.Context.Narration);
            introduction.instruction.SetContentWithContext("Hi. Let's start stretching exercise.\nIn this training you are asked to remember five poses in order.", Information.Context.Description);
            introduction.exit = new Condition(new TaskState(introduction, TaskProgressState.Started), Condition.RelationalOperator.Equal, new TimeState(0,0,3));
            scenario.Add(introduction);

            Task task1 = new Task("Task1", "");
            task1.instruction = new Instruction(task1.name);
            task1.instruction.SetContentWithContext("Forward bend pose", Information.Context.Title);
            task1.instruction.SetContentWithContext("First, forward bend pose. In a standing position, lean forward and extend your arms downwards. Grip controller when you are done.", Information.Context.Narration);
            task1.instruction.SetContentWithContext("First, forward bend pose. \nIn a standing position, lean forward and extend your arms downwards. \nGrip controller when you are done.", Information.Context.Description);
            task1.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task1);

            Task task2 = new Task("Task2", "");
            task2.instruction = new Instruction(task2.name);
            task2.instruction.SetContentWithContext("Triangle pose", Information.Context.Title);
            task2.instruction.SetContentWithContext("Second, triangle pose. Tilt your waist to the left with your left arm lowered to the floor. In this state, your right arm is fully extended in the direction of the sky and your gaze is directed at the tip of your right hand. Grip controller when you are done.", Information.Context.Narration);
            task2.instruction.SetContentWithContext("Second, triangle pose. \nTilt your waist to the left with your left arm lowered to the floor. \nIn this state, your right arm is fully extended in the direction of the sky \nand your gaze is directed at the tip of your right hand. \nGrip controller when you are done.", Information.Context.Description);
            task2.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task2);

            Task task3 = new Task("Task3", "");
            task3.instruction = new Instruction(task3.name);
            task3.instruction.SetContentWithContext("Side bend stretch", Information.Context.Title);
            task3.instruction.SetContentWithContext("Third, side bend stretch. Place your right hand on your right waist. Keep your left arm straight in the sky. Slowly tilt your upper body to the right in this position. Grip controller when you are done.", Information.Context.Narration);
            task3.instruction.SetContentWithContext("Third, side bend stretch. \nPlace your right hand on your right waist. \nKeep your left arm straight in the sky. \nSlowly tilt your upper body to the right in this position.", Information.Context.Description);
            task3.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task3);

            Task task4 = new Task("Task4", "");
            task4.instruction = new Instruction(task4.name);
            task4.instruction.SetContentWithContext("Mountain pose", Information.Context.Title);
            task4.instruction.SetContentWithContext("Fourth, mountain pose. With your shoulders relaxed, place your arms above your head in an upright position and stretch your arms all the way up. Point your eyes forward. Grip controller when you are done.", Information.Context.Narration);
            task4.instruction.SetContentWithContext("Fourth, mountain pose. \nWith your shoulders relaxed, place your arms above your head in an upright position \nand stretch your arms all the way up. Point your eyes forward.", Information.Context.Description);
            task4.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task4);

            Task task5 = new Task("Task5", "");
            task5.instruction = new Instruction(task5.name);
            task5.instruction.SetContentWithContext("Neck relaxing pose", Information.Context.Title);
            task5.instruction.SetContentWithContext("Fifth, neck relaxing pose. Place your hands on your back in a standing position. In this position, turn your head and eyes toward the sky. Grip controller when you are done.", Information.Context.Narration);
            task5.instruction.SetContentWithContext("Fifth, neck relaxing pose. \nPlace your hands on your back in a standing position. \nIn this position, turn your head and eyes toward the sky. ", Information.Context.Description);
            task5.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(task5);

            Task ending = new Task("Finish", "");
            ending.instruction = new Instruction(ending.name);
            ending.instruction.SetContentWithContext("Finish", Information.Context.Title);
            ending.instruction.SetContentWithContext("Well done! Your training is successfully terminated.", Information.Context.Narration);
            ending.instruction.SetContentWithContext("Well done! Your training is successfully terminated.", Information.Context.Description);
            ending.exit = new Condition(new OTouchDownState(actor, (int)Valve.VR.InteractionSystem.GrabTypes.Grip), Condition.RelationalOperator.Equal);
            scenario.Add(ending);

            scenario.MakeProcedure();

            scenario.Activate();
        }

        // Update is called once per frame
        void Update()
        {
            if (scenario != null)
                scenario.Proceed(interfaces);
        }
    }
}

