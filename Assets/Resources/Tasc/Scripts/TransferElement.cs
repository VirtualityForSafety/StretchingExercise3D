using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class TransferElement: MonoBehaviour
    {
        public string type = "not decided";
        public List<string> context;

        Renderer currentRenderer;

        public void Start()
        {
            context = new List<string>();
        }

        void Awake()
        {
            currentRenderer = GetComponentInChildren<Renderer>();
        }

        public void SetVisibility(bool value)
        {
            currentRenderer.enabled = value;
        }

        public void SetPose(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }

        public virtual void SetInformation(string message)
        {

        }
    }
}
