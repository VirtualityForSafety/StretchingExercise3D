using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tasc
{
    public class Billboard : VisualInterface
    {
        // Update is called once per frame
        void Update()
        {
            if (Camera.main != null)
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }
}


