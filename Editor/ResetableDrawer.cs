using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AttributeExt
{
    [CustomPropertyDrawer(typeof(ResetableAttribute), true)]
    public class ResetableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position,
                                       SerializedProperty property,
                                       GUIContent label)
        {
            if (GUI.Button(position, "R"))
            {
                var objs = property.serializedObject.targetObject;
                var handle = objs as IResetterHandle;
                if (handle != null)
                {
                    handle.Reset();
                }
                else
                {
                    Debug.LogWarning("The class does not implement 'IResetterHandle' Interface "+ property.serializedObject.GetType());
                }
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.EndProperty();

            //EditorGUI.PropertyField(position, property, label, true);

        }
    }

}