//Original: https://github.com/datsfain/EditorCools/tree/main/Assets/Tools/DropdownStringAttribute
//Modified for null or empty list

using System;
using UnityEngine;

namespace AttributeExt
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DropdownAttribute : PropertyAttribute
    {
        public enum MethodLocation { PropertyClass, StaticClass }
        public MethodLocation Location { get; private set; }
        public string MethodName { get; private set; }
        public Type MethodOwnerType { get; private set; }

        public DropdownAttribute(string methodName)
        {
            Location = MethodLocation.PropertyClass;
            MethodName = methodName;
        }

        public DropdownAttribute(Type methodOwner, string methodName)
        {
            Location = MethodLocation.StaticClass;
            MethodOwnerType = methodOwner;
            MethodName = methodName;
        }
    }
}