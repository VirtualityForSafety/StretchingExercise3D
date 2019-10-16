using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class BoolVariableState : VariableState
    {
        public Parameter<bool> value;
		public BoolVariableState(Terminus _sub, string _variableName, bool _value): base(_sub, _variableName)
		{
			name = "BoolVariableState";
			description = "Check a bool variable of a subject";
            value = new Parameter<bool>(_value);
		}

        public bool GetValue()
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
                return this.GetValue() == (other as BoolVariableState).GetValue() ? 0: base.Diff(other);
        }

        public override bool Equals(object obj)
        {
            if((obj as BoolVariableState) != null)
                return base.Equals(obj) && value.GetValue().Equals((obj as BoolVariableState).value.GetValue());
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override string ToString()
        {
            return base.ToString() + ":(bool)" + value.GetValue();
        }

        public override int CompareTo(object obj)
        {
            if ((obj as BoolVariableState) != null)
                return value.GetValue().CompareTo((obj as BoolVariableState).value.GetValue());
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
