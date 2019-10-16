using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
	public class Parameter<T>: IComparable<Parameter<T>>, IEquatable<Parameter<T>>
	{
        private T value;

		public Parameter(T t)
		{
			value = t;
            if (OnValueChange != null)
                OnValueChange(value);
        }

		public T GetValue()
		{
			return value;
		}

        public void SetValue(T _value) {
            value = _value;
            if (OnValueChange != null)
                OnValueChange(value);
        }

        public delegate void OnValueChangeDelegate(T value);
        public event OnValueChangeDelegate OnValueChange;


        public int CompareTo(Parameter<T> other)
		{
			if (value.GetType() != other.value.GetType())
				throw new FormatException();
			else
				return Comparer<T>.Default.Compare(value, other.GetValue());
		}

		public bool Equals(Parameter<T> other)
		{
            if (value.GetType() != other.value.GetType())
                throw new FormatException();
			else
				return object.Equals(value, other.GetValue());
		}

        /*
		static void Equals<T>(T a, T b) where T : class
		{
			Console.WriteLine(a == b);
		}
        */
	}
}
