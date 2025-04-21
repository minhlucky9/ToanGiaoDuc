using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LinkPair
{
    [CustomPropertyDrawer(typeof(RowData))]
    public class PhasePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create a new VisualElement to be the root the property UI.
            var container = new VisualElement();

            // Create drawer UI using C#.
            var popup = new UnityEngine.UIElements.PopupWindow();
            popup.text = "Phase properties";
            popup.Add(new PropertyField(property.FindPropertyRelative("validTypes"), "Object types (int)"));
            popup.Add(new PropertyField(property.FindPropertyRelative("allowedComparators"), "Questions"));
            container.Add(popup);

            // Return the finished UI.
            return container;
        }
    }

}