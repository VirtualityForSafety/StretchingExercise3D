using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class MoveState : VectorVariableState
    {
        public MoveState(Terminus _sub, string _variableName="moveState") : base(_sub, _variableName, Vector3.zero)
        {
            name = "MoveState";
            description = "Moving event of a subject";
            value = new Parameter<Vector3>(_sub.transform.position);
        }

        public bool hasSameTerminusWith(MoveState other)
        {
            return subject.name.Equals(other.subject.name);
        }

        public static bool areFromSameTerminus(MoveState state1, MoveState state2)
        {
            if (state1.hasSameTerminusWith(state2) || state2.hasSameTerminusWith(state1))
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if ((obj as MoveState) != null)
                return subject.name.Equals((obj as MoveState).subject.name) && value.Equals((obj as MoveState).value);
            else
                return false;
        }

        public override int CompareTo(object obj)
        {
            if ((obj as MoveState) != null)
                return subject.name.CompareTo((obj as MoveState).subject.name);
            else
                return 1;
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
