using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Information
    {
        public enum Context { Default, Title, Description, Status, Narration, InteractiveStatus }

        public Dictionary<string, string> contextContent;

        public Information()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            if (contextContent == null)
            {
                contextContent = new Dictionary<string, string>();
                contextContent.Add("Default", "");
            }
        }

        public void SetContent(string content, Information.Context context = Context.Default)
        {
            SetContent(content, context.ToString());
        }

        public void SetContent(string content, string context)
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
            return contextContent.TryGetValue(context, out result) ? result : contextContent["Default"];
        }
    }
}

