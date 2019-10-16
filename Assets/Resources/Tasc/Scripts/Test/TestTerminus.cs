using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasc;
public class TestTerminus : Terminus {
    public bool isPushed = false;
    public float incrementValue = 0;
    public Vector3 incrementVector = Vector3.zero;
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
        return base.ToString() + " : Value (" + (isPushed) + ")";
    }

    public override void Update()
    {
        base.Update();
        if (UnityEngine.Input.GetKeyUp(KeyCode.Z))
        {
            isPushed = true;
            incrementValue += 0.5f;
            //Debug.Log(incrementValue);
            incrementVector = Vector3.zero;
            Send();
        }
    }

    public override void Send()
    {
        ConditionPublisher.Instance.Send(new BoolVariableState(this, "isPushed", isPushed));
        ConditionPublisher.Instance.Send(new IntVariableState(this, "incrementValue_int", (int)incrementValue));
        ConditionPublisher.Instance.Send(new FloatVariableState(this, "incrementValue", incrementValue));
        ConditionPublisher.Instance.Send(new VectorVariableState(this, "incrementVector", incrementVector));
    }

    private void Start()
    {
        Initialize();
    }
}
