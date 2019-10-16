using UnityEngine;
using System.Collections;

namespace Tasc
{
	public class Interface : MonoBehaviour
	{
        public Information.Context context = Information.Context.Default;
		public virtual void Transfer(string msg) { }
	}
}

