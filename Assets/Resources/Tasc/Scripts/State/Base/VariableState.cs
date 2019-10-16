using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class VariableState : IntraState
    {
        public Parameter<string> variableName;
		public VariableState(Terminus _sub, string _variableName)
		{
			name = "VariableState";
			description = "Check a variable of a subject";
			subject = _sub;
			variableName = new Parameter<string>(_variableName);
		}

        public override bool Equals(object obj)
        {
            return HasSameIdentity(obj);
        }

        private bool HasSameIdentity(object obj)
        {
            return subject.name.Equals((obj as VariableState).subject.name) && variableName.GetValue().Equals((obj as VariableState).variableName.GetValue());
        }

        public static bool IsSameVariable(VariableState v1, VariableState v2)
        {
            return v1.HasSameIdentity(v2);
        }

        public virtual float Diff(VariableState other)
        {
            return 999999;
        }

        public override string ToString()
        {
            return name + ":" + variableName.GetValue();
        }

        public override int CompareTo(object obj)
        {
            return base.CompareTo(obj);
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
