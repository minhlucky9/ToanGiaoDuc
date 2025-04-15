using UnityEngine;

namespace ColorPaint
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Paintable : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Color originalColor;
        private Color lastColor = Color.white;

        private bool painted = false;
        private bool wasCorrect = false;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
            lastColor = originalColor;
        }

        void OnMouseDown()
        {
            PaintWithColor(ColorPicker.selectedColor);
        }

        public void PaintWithColor(Color newColor)
        {
            var currentColor = spriteRenderer.color;

            if (IsWhite(newColor))
            {
                if (!IsWhite(currentColor))
                {
                    Erase(); // Xoá màu
                    Debug.Log("🧽 Xoá màu - không tính lỗi.");
                }
                return;
            }

            var targetColor = GetTargetColor();
            if (targetColor.HasValue)
            {
                bool isNowCorrect = GameManager.Instance.AreColorsSimilar(newColor, targetColor.Value);

                if (!isNowCorrect)
                {
                    if (!IsWhite(currentColor))
                    {
                        Debug.Log("⚠️ Hình đã có màu - cần xóa trước.");
                        return;
                    }

                    if (wasCorrect || lastColor != newColor)
                    {
                        GameManager.Instance.AddMistake();
                        Debug.Log("🎨 Tô sai màu => +1 lỗi");
                    }

                    wasCorrect = false;
                }
                else
                {
                    wasCorrect = true;
                    Debug.Log("✅ Tô đúng màu - không lỗi.");
                }
            }

            spriteRenderer.color = newColor;
            lastColor = newColor;
            painted = !IsWhite(newColor);
        }

        public void Erase()
        {
            spriteRenderer.color = originalColor;
            painted = false;
            lastColor = originalColor;
            wasCorrect = false;
        }

        public Color GetCurrentColor() => spriteRenderer.color;

        public bool IsPainted() => painted;

        private bool IsWhite(Color color)
        {
            const float threshold = 0.95f;
            return color.r >= threshold && color.g >= threshold && color.b >= threshold;
        }

        private Color? GetTargetColor()
        {
            var shapeID = GetComponent<ShapeIdentifier>();
            return shapeID != null ? GameManager.Instance?.GetTargetColor(shapeID.shapeType) : null;
        }
    }
}
