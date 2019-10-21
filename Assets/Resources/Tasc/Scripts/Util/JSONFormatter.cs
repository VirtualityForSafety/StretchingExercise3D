using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class JSONFormatter
    {
        public static string ToJSON(Terminus obj)
        {
            string result = "\"" + obj.name + "\":{";
            Collider collider = obj.GetComponent<Collider>();
            result += "\"terminus\":{";
            //result += "{\"rotation\":" + Vec3ToJSON(transform.rotation.eulerAngles);
            //result += "{\"localRotation\":" + Vec3ToJSON(transform.localRotation.eulerAngles);
            //result += ",\"position\":" + Vec3ToJSON(transform.position);
            //result += ",\"scale\":" + Vec3ToJSON(transform.localScale);
            if (collider != null)
            {
                result += "\"center\":" + Vec3ToJSON(collider.bounds.center);
                result += ",\"size\":" + Vec3ToJSON(collider.bounds.size);
                result += ",\"min\":" + Vec3ToJSON(collider.bounds.min);
                result += ",\"max\":" + Vec3ToJSON(collider.bounds.max);
            }

            return result + "}}";
        }

        public static string ToJSON(MonoBehaviour obj)
        {
            string result = "\""+obj.name+"\":{";
            Transform transform = obj.transform;
            Collider collider = obj.GetComponent<Collider>();
            result += "\"terminus\":{";
            //result += "{\"rotation\":" + Vec3ToJSON(transform.rotation.eulerAngles);
            //result += "{\"localRotation\":" + Vec3ToJSON(transform.localRotation.eulerAngles);
            //result += ",\"position\":" + Vec3ToJSON(transform.position);
            //result += ",\"scale\":" + Vec3ToJSON(transform.localScale);
            if (collider != null)
            {
                result += "\"center\":" + Vec3ToJSON(collider.bounds.center);
                result += ",\"size\":" + Vec3ToJSON(collider.bounds.size);
                result += ",\"min\":" + Vec3ToJSON(collider.bounds.min);
                result += ",\"max\":" + Vec3ToJSON(collider.bounds.max);
            }

            return result + "}}";
        }

        private static string Vec3ToJSON(Vector3 vector)
        {
            return "{" + "\"x\":" + vector.x.ToString() + ","
                + "\"y\":" + vector.y.ToString() + ","
                + "\"z\":" + vector.z.ToString() + "}";
        }
    }
}

