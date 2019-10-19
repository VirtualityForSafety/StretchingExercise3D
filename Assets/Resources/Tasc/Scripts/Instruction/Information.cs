using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tasc
{
    public class Information
    {
        // will be deprecated
        public enum Context { Default, Title, Description, Status, Narration, InteractiveStatus, Guidance }

        public Dictionary<string, string> contextContent;

        public Information()
        {
            Initialize();
        }

        public Information(Information another)
        {
            contextContent = CloneDictionaryCloningValues<string, string>(another.contextContent);
        }

        public virtual void Initialize()
        {
            if (contextContent == null)
            {
                contextContent = new Dictionary<string, string>();
                contextContent.Add("Default", "");
            }
        }

        public void SetContent(string context, string content)
        {
            if (contextContent == null)
                Initialize();
            if (contextContent.ContainsKey(context))
                contextContent[context] = content;
            else
                contextContent.Add(context, content);
        }

        public virtual string GetContent(Information.Context context = Context.Default)
        {
            return GetContent(context.ToString());
        }

        public virtual string GetContent(string context)
        {
            string result = "";
            return contextContent.TryGetValue(context, out result) ? result : "ERROR: Not registered";
        }

        public Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>(Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                                                                    original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }
    }
}

