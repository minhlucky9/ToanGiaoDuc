using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace ColorPaint
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Scoreboard UI")]
        public GameObject scorePanel;
        public TextMeshProUGUI correctText;
        public TextMeshProUGUI wrongText;
        public TextMeshProUGUI mistakeText;
        public TextMeshProUGUI timeText;
        public TimerScript timerScript;

        [Header("Game Elements")]
        public Paintable[] paintables;
        public Image[] targetShapes;

        public int mistakeCount = 0;
        private const float COLOR_SIMILARITY_THRESHOLD = 0.1f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            scorePanel.SetActive(false);
        }

        public void AddMistake()
        {
            mistakeCount++;
        }

        public void ShowScore()
        {
            if (paintables == null || targetShapes == null)
            {
                Debug.LogError("Paintables hoặc TargetShapes chưa được gán!");
                return;
            }

            int correctCount = 0;
            int wrongCount = 0;

            foreach (Paintable paintable in paintables)
            {
                if (paintable == null) continue;

                ShapeIdentifier paintableShape = paintable.GetComponent<ShapeIdentifier>();
                if (paintableShape == null) continue;

                Color? targetColor = GetTargetColor(paintableShape.shapeType);
                if (targetColor == null) continue;

                Color currentColor = paintable.GetCurrentColor();

                if (!paintable.IsPainted() || IsWhiteColor(currentColor))
                {
                    wrongCount++;
                    continue;
                }

                if (AreColorsSimilar(currentColor, targetColor.Value))
                {
                    correctCount++;
                }
                else
                {
                    wrongCount++;
                }
            }

            timerScript.StopTimer();

            correctText.text = $"{correctCount}";
            wrongText.text = $" {wrongCount}";
            mistakeText.text = $"{mistakeCount}";
            timeText.text = $" {timerScript.GetFormattedTime()}";

            scorePanel.SetActive(true);

            Debug.Log($"[GameManager] Kết quả: Đúng: {correctCount} - Sai: {wrongCount} - Lỗi: {mistakeCount} - Thời gian: {timerScript.GetFormattedTime()}");
        }

        public void ResetGame()
        {
            mistakeCount = 0;

            if (paintables != null)
            {
                foreach (var paintable in paintables)
                {
                    paintable?.Erase();
                }
            }

            scorePanel.SetActive(false);
            timerScript.StartTimer();
        }

        public void AutoCorrect()
        {
            foreach (var paintable in paintables)
            {
                if (paintable == null) continue;

                ShapeIdentifier shapeID = paintable.GetComponent<ShapeIdentifier>();
                if (shapeID == null) continue;

                Color? targetColor = GetTargetColor(shapeID.shapeType);
                if (targetColor != null)
                {
                    paintable.PaintWithColor(targetColor.Value);
                }
            }

            Debug.Log("🛠 Tự động tô đúng màu cho tất cả hình!");
        }

        public Color? GetTargetColor(ShapeType shapeType)
        {
            foreach (var img in targetShapes)
            {
                var id = img.GetComponent<ShapeIdentifier>();
                if (id != null && id.shapeType == shapeType)
                {
                    return img.color;
                }
            }
            return null;
        }

        public bool AreColorsSimilar(Color a, Color b)
        {
            return ColorDistance(a, b) < COLOR_SIMILARITY_THRESHOLD;
        }

        private float ColorDistance(Color a, Color b)
        {
            return Mathf.Abs(a.r - b.r) +
                   Mathf.Abs(a.g - b.g) +
                   Mathf.Abs(a.b - b.b);
        }

        private bool IsWhiteColor(Color color)
        {
            const float whiteThreshold = 0.95f;
            return color.r >= whiteThreshold &&
                   color.g >= whiteThreshold &&
                   color.b >= whiteThreshold;
        }
    }
}
