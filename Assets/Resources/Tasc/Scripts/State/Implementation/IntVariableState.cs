using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class IntVariableState : VariableState
    {
        public Parameter<int> value;
        public IntVariableState(Terminus _sub, string _variableName, int _value) : base(_sub, _variableName)
        {
            name = "IntVariableState";
            description = "Check an int variable of a subject";
            value = new Parameter<int>(_value);
        }

        public int GetValue()
        {
            return value.GetValue();
        }

        public override float Diff(VariableState other)
        {
            if (other == null)
                return base.Diff(other);
            if (this.GetType() != other.GetType())
                return base.Diff(other);
            else
                return Mathf.Abs(this.GetValue() - (other as IntVariableState).GetValue());
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && value.GetValue().Equals((obj as IntVariableState).value.GetValue());
        }

        public override string ToString()
        {
            return base.ToString() + ":(int)" + value.GetValue();
        }

        public override int CompareTo(object other)
        {
            if (other == null)
                return base.CompareTo(other);
            if (this.GetType() != other.GetType())
                return base.CompareTo(other);
            return GetValue().CompareTo((other as IntVariableState).GetValue());
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
