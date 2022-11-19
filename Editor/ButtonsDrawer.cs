//original: https://github.com/datsfain/EditorCools/tree/main/Assets/Tools/InspectorButton

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AttributeExt
{
    public class ButtonsDrawer
    {
        public readonly List<IGrouping<string, Button>> ButtonGroups;

        public ButtonsDrawer(object target)
        {
            Debug.Log("tick");
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = target.GetType().GetMethods(flags);
            var buttons = new List<Button>();
            var rowNumber = 0;

            foreach (MethodInfo method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null)
                    continue;


                Debug.Log("method paise. name: " + method.Name);
                buttons.Add(new Button(method, buttonAttribute));
            }

            ButtonGroups = buttons.GroupBy(button =>
            {
                var attribute = button.ButtonAttribute;
                var hasRow = attribute.HasRow;
                return hasRow ? attribute.Row : $"__{rowNumber++}";
            }).ToList();
        }

        public void DrawButtons(IEnumerable<object> targets)
        {
            foreach (var buttonGroup in ButtonGroups)
            {
                if (buttonGroup.Count() > 0)
                {
                    var space = buttonGroup.First().ButtonAttribute.Space;
                    if (space != 0) EditorGUILayout.Space(space);
                }
                using (new EditorGUILayout.HorizontalScope())
                {
                    foreach (var button in buttonGroup)
                    {
                        button.Draw(targets);
                    }
                }
            }
        }
    }
}