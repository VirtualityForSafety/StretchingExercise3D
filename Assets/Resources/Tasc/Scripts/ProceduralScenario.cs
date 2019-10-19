using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Scenario
    {
        public string name;
        public string description;
        List<Task> scenario;

        public bool isActivated;

        public Scenario(string _name, string _description)
        {
            name = _name;
            description = _description;
            scenario = new List<Task>();
            isActivated = false;
        }

        public void Add(Task t)
        {
            scenario.Add(t);
        }

        public void MakeProcedure()
        {
            for (int i = 0; i < scenario.Count - 1; i++)
            {
                scenario[i].SetNext(TaskEndState.Correct, scenario[i + 1]);
            }
        }

        public void Activate()
        {
            isActivated = true;
            scenario[0].Activate();
        }

        void exportTerminusJSON()
        {
            /*
            Tasc.Asset[] assets = (Tasc.Asset[])GameObject.FindObjectsOfType(typeof(Tasc.Asset));
            string terminuses = "{";
            foreach (Tasc.Asset asset in assets)
            {
                terminuses += asset.ToJSON() + ",";
            }
            terminuses = terminuses.Remove(terminuses.Length - 1, 1) + "}";
            //Debug.Log(terminuses);
            System.IO.File.WriteAllText(System.IO.Path.Combine("Assets\\Data", "terminus.json"), terminuses);
            isTerminusExported = true;
            */
        }

        public void Proceed()
        {
            for (int i = 0; i < scenario.Count; i++)
            {
                scenario[i].Proceed();
                if (scenario[i].HasFinished())
                    scenario.RemoveAt(i);
            }
        }
    }
}