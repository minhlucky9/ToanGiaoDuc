using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ColorPaint
{
    public class ColorPicker : MonoBehaviour
    {
        public static Color selectedColor = Color.white;
        private static Button selectedButton;

        public void SelectColor(string colorName)
        {

            selectedColor = colorName switch
            {
                "Red" => Color.red,
                "Orange" => new Color(1.0f, 0.5f, 0.0f),
                "Yellow" => Color.yellow,
                "Green" => Color.green,
                "Blue" => Color.blue,
                "Purple" => new Color(0.5f, 0.0f, 0.5f),
                "Pink" => new Color(1.0f, 0.75f, 0.8f),
                _ => Color.white
            };

            Debug.Log($"🎨 Màu được chọn: {colorName} | Giá trị: {selectedColor}");
            MoveButtonLeft();
        }

        private void MoveButtonLeft()
        {
            if (EventSystem.current?.currentSelectedGameObject is not GameObject clickedObject) return;
            if (!clickedObject.TryGetComponent(out Button clickedButton)) return;

            if (selectedButton != null)
            {
                RectTransform oldTransform = selectedButton.GetComponent<RectTransform>();
                oldTransform.anchoredPosition += new Vector2(20, 0);
            }

            RectTransform newTransform = clickedButton.GetComponent<RectTransform>();
            newTransform.anchoredPosition += new Vector2(-20, 0);

            selectedButton = clickedButton;
        }
    }
}
