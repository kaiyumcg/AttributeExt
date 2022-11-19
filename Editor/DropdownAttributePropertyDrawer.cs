//Original: https://github.com/datsfain/EditorCools/tree/main/Assets/Tools/DropdownStringAttribute
//Modified for null or empty list, 

using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Linq;

namespace AttributeExt
{
    [CustomPropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var options = GetOptions(property);
            if (options == null || options.Length == 0)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.BeginProperty(position, label, property);
                DrawDropdown(position, property, label, options);
                EditorGUI.EndProperty();
            }
        }

        private string[] GetOptions(SerializedProperty property)
        {
            var attr = attribute as DropdownAttribute;
            var methodName = attr.MethodName;

            var objectType = fieldInfo.DeclaringType;

            var methodOwnerType = attr.Location == DropdownAttribute.MethodLocation.PropertyClass ? objectType : attr.MethodOwnerType;

            var methodInfo = methodOwnerType.GetMethod
                (methodName,
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.Instance);

            if (methodInfo == null)
            {
                Debug.LogError($"Method {methodName} In {methodOwnerType.FullName} Could Not Be Found!");
                return null;
            }
            var methodInfoReturnValueIsStringArray = methodInfo.ReturnType == typeof(string[]);
            if (!methodInfoReturnValueIsStringArray)
            {
                Debug.LogError($"Method {methodName} In {methodOwnerType.FullName} Does Not Have A Return Type Of {typeof(string[]).FullName}");
                return null;
            }

            var invokeReference = attr.Location == DropdownAttribute.MethodLocation.StaticClass ? null : property.serializedObject.targetObject;
            var returnValue = methodInfo.Invoke(invokeReference, null) as string[];
            return returnValue;
        }

        private void DrawDropdown(Rect position, SerializedProperty property, GUIContent label, string[] options)
        {
            if (options == null || options.Length == 0)
            {
                options = new string[] { "error! no data defined!" };
            }
            
            var selectedIndex = Array.IndexOf(options, property.stringValue);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
            if (options.Contains(property.stringValue) == false) 
            { 
                selectedIndex = 0;
                property.stringValue = options[selectedIndex];
            }

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                selectedIndex = EditorGUI.Popup(EditorGUI.PrefixLabel(position, label), selectedIndex, options);
                if (check.changed || string.IsNullOrEmpty(property.stringValue) || string.IsNullOrWhiteSpace(property.stringValue))
                {
                    property.stringValue = options[selectedIndex];
                }
            }
        }
    }
}