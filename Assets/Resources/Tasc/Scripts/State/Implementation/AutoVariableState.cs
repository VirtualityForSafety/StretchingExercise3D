using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class AutoVariableState : IntraState
    {
        VariableState variableState;
		public AutoVariableState(Terminus _sub, string _variableName, bool _value)
        {
            variableState = new BoolVariableState(_sub, _variableName, _value);
        }

        public AutoVariableState(Terminus _sub, string _variableName, int _value)
        {
            variableState = new IntVariableState(_sub, _variableName, _value);
        }

        public AutoVariableState(Terminus _sub, string _variableName, float _value)
        {
            variableState = new FloatVariableState(_sub, _variableName, _value);
        }

        public AutoVariableState(Terminus _sub, string _variableName, Vector3 _value)
        {
            variableState = new VectorVariableState(_sub, _variableName, _value);
        }

        public VariableState GetVariableState()
        {
            return variableState;
        }

        public override bool Equals(object obj)
        {
            return variableState.Equals(obj);
        }

        public virtual float Diff(VariableState other)
        {
            return variableState.Diff(other);
        }

        public override string ToString()
        {
            return variableState.ToString();
        }

        public override int CompareTo(object obj)
        {
            return variableState.CompareTo(obj);
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
