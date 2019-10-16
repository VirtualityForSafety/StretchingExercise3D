using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class FloatVariableState : VariableState
    {
        public Parameter<float> value;
        public FloatVariableState(Terminus _sub, string _variableName, float _value) : base(_sub, _variableName)
        {
            name = "FloatVariableState";
            description = "Check an float variable of a subject";
            value = new Parameter<float>(_value);
        }

        public float GetValue()
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
                return Mathf.Abs(this.GetValue() - (other as FloatVariableState).GetValue());
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && value.GetValue().Equals((obj as FloatVariableState).value.GetValue());
        }

        public override string ToString()
        {
            return base.ToString() + ":(float)" + value.GetValue();
        }

        public override int CompareTo(object other)
        {
            if (other == null)
                return base.CompareTo(other);
            if (this.GetType() != other.GetType())
                return base.CompareTo(other);
            return GetValue().CompareTo((other as FloatVariableState).GetValue());
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
