using UnityEngine;
using UnityEngine.UI;

namespace ColorPaint
{
    public class RandomColorShapesUI : MonoBehaviour
    {
        public Image[] shapes;
        private Color[] colors = {
            Color.red,
            Color.yellow,
            Color.green,
            Color.blue,
            new Color(1.0f, 0.75f, 0.8f),
            new Color(0.5f, 0.0f, 0.5f),
            new Color(1.0f, 0.5f, 0.0f)
        };

        void Start()
        {
            AssignRandomColors();
        }

        void AssignRandomColors()
        {
            foreach (Image shape in shapes)
            {
                shape.color = colors[Random.Range(0, colors.Length)];
            }
        }
    }
}
