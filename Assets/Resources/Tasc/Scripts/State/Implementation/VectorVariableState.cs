using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class VectorVariableState : VariableState
    {
        public Parameter<Vector3> value;
        public VectorVariableState(Terminus _sub, string _variableName, Vector3 _value) : base(_sub, _variableName)
        {
            name = "VectorVariableState";
            description = "Check an Vector3 variable of a subject";
            value = new Parameter<Vector3>(_value);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && value.GetValue().Equals((obj as VectorVariableState).value.GetValue());
        }

        public override int CompareTo(object obj)
        {
            return 1;
        }

        public Vector3 GetValue()
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
                return Vector3.Distance(this.GetValue(), (other as VectorVariableState).GetValue());
        }

        public override string ToString()
        {
            return base.ToString() + ":(Vector3)" + value.GetValue();
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
