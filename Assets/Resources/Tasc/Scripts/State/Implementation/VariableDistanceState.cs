using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class VariableDistanceState : InterState
    {
        public VariableState stateVar1;
        public VariableState stateVar2;
        public Parameter<float> value; // value for distance comparison threshold

        public VariableDistanceState(VariableState _sub1, float _value)
        {
            name = "VariableDistanceState";
            description = "Distance of variable <" + _sub1.variableName + ">";
            stateVar1 = _sub1;
            stateVar2 = null;
            state1 = _sub1;
            state2 = null;
            value = new Parameter<float>(_value);
        }

        public VariableDistanceState(VariableState _sub1, VariableState _sub2, float _value = Mathf.NegativeInfinity):this(_sub1, _value)
        {
            description = "Distance of variable <"+ _sub1.variableName + "> between "+_sub1.name+ " and " + _sub2.name;
            stateVar2 = _sub2;
            state2 = _sub2;
            if (_value == Mathf.NegativeInfinity)
            {
                if ((stateVar1 != null && stateVar2 != null) && (stateVar1.GetType()==stateVar2.GetType()))
                {
                    //Debug.Log(stateVar1.GetType());
                    //Debug.Log(stateVar2.GetType());
                    //Debug.Log(stateVar1.Diff(stateVar2));
                    value = new Parameter<float>(stateVar1.Diff(stateVar2));
                }
                else
                    value = null;
            }
            else
                value = new Parameter<float>(_value);
        }
        public VariableDistanceState(VariableDistanceState _vds, VariableState _sub2):this(_vds.stateVar1,_sub2, Mathf.NegativeInfinity)
        {
        }

        public override bool Equals(object obj)
        {
            //Debug.Log(value.GetValue());
            //Debug.Log((obj as VariableDistanceState).value.GetValue());
            if (this.GetType() != obj.GetType())
                return false;
            else if (value == null)
                throw new System.Exception("The comparison results null.");
            else
                return value.GetValue().Equals((obj as VariableDistanceState).value.GetValue());
        }

        public override string ToString()
        {
            return stateVar1.ToString() + ":" + (stateVar2 == null ? "null" : stateVar2.ToString()) + "=" + (value == null ? "null" : value.GetValue().ToString());
        }

        public override int CompareTo(object obj)
        {
            if (this.GetType() != obj.GetType())
                return 1;
            else if (value == null)
                throw new System.Exception("The comparison results null.");
            else
                return value.GetValue().CompareTo((obj as VariableDistanceState).value.GetValue());
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
