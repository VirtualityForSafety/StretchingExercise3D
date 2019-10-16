using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class DistanceState : InterState
    {
        // two MoveState should not be generated from the same terminus
        public MoveState stateVar1;
        public MoveState stateVar2;
        //string identifier;
        public Parameter<float> value; // value for distance comparison threshold

        public DistanceState(MoveState _sub1, MoveState _sub2, float _value = Mathf.NegativeInfinity)
        {
            name = "DistanceState";
            description = "Distance between " + _sub1.name + " and " + _sub2.name;
            stateVar1 = _sub1;
            state1 = _sub1;
            stateVar2 = _sub2;
            state2 = _sub2;
            //identifier = stateVar1.subject.name.CompareTo(stateVar2.subject.name) < 0 ? stateVar1.subject.name + stateVar2.subject.name : stateVar2.subject.name + stateVar1.subject.name;

            if (_value == Mathf.NegativeInfinity)
            {
                if (stateVar1 != null && stateVar2 != null)
                    value = new Parameter<float>(stateVar1.Diff(stateVar2));
                else
                    value = null;
            }
            else
                value = new Parameter<float>(_value);
        }

        public DistanceState GetUpdated(MoveState state)
        {
            if (MoveState.areFromSameTerminus(stateVar1, state))
                return new DistanceState(state, stateVar2);
            else if (MoveState.areFromSameTerminus(stateVar2, state))
                return new DistanceState(stateVar1, state);
            else
                return null;
        }

        public bool hasMoveStateFromSameTerminus(MoveState state)
        {
            return MoveState.areFromSameTerminus(stateVar1, state) || MoveState.areFromSameTerminus(stateVar2, state);
        }

        public override bool Equals(object obj)
        {
            //Debug.Log(value.GetValue());
            //Debug.Log("Target : " +(obj as DistanceState).value.GetValue());
            if (this.GetType() != obj.GetType())
                return false;
            else if (value == null)
                return false;
            ////////////////////////////////////////////////////////////////////
            // not tested
            //else if (!this.identifier.Equals((obj as DistanceState).identifier)
            //    return false;
            else
                return value.GetValue().Equals((obj as DistanceState).value.GetValue());
        }

        public override int CompareTo(object obj)
        {
            if (this.GetType() != obj.GetType())
                return 1;
            else if (value == null)
                return 1;
            else
                return value.GetValue().CompareTo((obj as DistanceState).value.GetValue());
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
