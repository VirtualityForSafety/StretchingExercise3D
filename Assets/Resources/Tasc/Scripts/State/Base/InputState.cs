using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public abstract class InputState: IntraState
    {
        public Parameter<int> value;

        public override bool Equals(object obj)
        {
            //Debug.Log(this.subject.ToString() + " : Equals");
            //Debug.Log((obj as InputState).subject.ToString() + " : Equals");
            return subject.name.Equals((obj as InputState).subject.name) && value.GetValue().Equals((obj as InputState).value.GetValue());
        }

        public override string ToString()
        {
            return name + ":" + (UnityEngine.KeyCode)value.GetValue() + " from " + subject.name;
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}
