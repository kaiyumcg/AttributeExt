using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttributeExt
{
    public class ToggleFieldAttribute : PropertyAttribute
    {
        public string label;
        public ToggleFieldAttribute(string label)
        {
            this.label = label;
        }
    }
}