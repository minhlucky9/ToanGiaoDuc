using UnityEngine;
using UnityEngine.UI;

namespace ColorPaint
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Game Elements")]
        [SerializeField] private Paintable[] paintables;
        [SerializeField] private Image[] targetShapes;

        private int mistakeCount = 0;
        private float timeElapsed = 0f;
        private bool isTimerRunning = true;

        private const float COLOR_SIMILARITY_THRESHOLD = 0.1f;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else { Destroy(gameObject); return; }
        }

        private void Start()
        {
            if (paintables == null || paintables.Length == 0)
                paintables = FindObjectsOfType<Paintable>();

            if (targetShapes == null || targetShapes.Length == 0)
                targetShapes = FindObjectsOfType<Image>();

            StartTimer();
        }

        private void Update()
        {
            if (isTimerRunning)
            {
                timeElapsed += Time.deltaTime;
            }
        }

        public void AddMistake()
        {
            mistakeCount++;
        }

        public void ShowScore()
        {
            if (paintables == null || targetShapes == null)
            {
                Debug.LogError("❗ Paintables hoặc TargetShapes chưa được gán!");
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

            StopTimer();
            string timeResult = GetFormattedTime();

            Debug.Log($"[GameManager] ✅ Đúng: {correctCount} | ❌ Sai: {wrongCount} | ⚠️ Lỗi: {mistakeCount} | ⏱ Thời gian: {timeResult}");
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

            StartTimer();
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

            Debug.Log("🛠 Đã tự động tô đúng màu cho tất cả hình.");
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

        // Timer Logic
        private void StartTimer()
        {
            timeElapsed = 0f;
            isTimerRunning = true;
        }

        private void StopTimer()
        {
            isTimerRunning = false;
        }

        private string GetFormattedTime()
        {
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
