using UnityEngine;

namespace MathConnection
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public int mistakeCount = 0;

        private float timeElapsed = 0f;
        private bool isTimerRunning = true;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
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
            Debug.Log($"[GameManager] Số lỗi hiện tại: {mistakeCount}");
        }

        public void ShowScore()
        {
            int correctCount = 0;
            int wrongCount = 0;

            var draggables = FindObjectsOfType<DraggableObject>();

            foreach (var draggable in draggables)
            {
                NumberSlot slot = draggable.currentConnectedSlot;
                bool isCorrectNow = (slot != null && draggable.objectCount == slot.numberSlot);

                if (isCorrectNow)
                    correctCount++;
                else
                    wrongCount++;
            }

            StopTimer();

            Debug.Log($"[GameManager] Kết quả:");
            Debug.Log($"- Đúng: {correctCount}");
            Debug.Log($"- Sai: {wrongCount}");
            Debug.Log($"- Lỗi: {mistakeCount}");
            Debug.Log($"- Thời gian: {GetFormattedTime()}");

            ClearAllLines();
        }

        public void ClearAllLines()
        {
            var draggables = FindObjectsOfType<DraggableObject>();
            foreach (var draggable in draggables)
            {
                draggable.ClearLineRenderer();
            }
        }

        public void ResetGame()
        {
            mistakeCount = 0;
            timeElapsed = 0f;
            StartTimer();

            var draggables = FindObjectsOfType<DraggableObject>();
            foreach (var draggable in draggables)
            {
                draggable.ClearLineRenderer();
                draggable.transform.position = draggable.startPoint.position;
                draggable.currentConnectedSlot = null;
            }

            Debug.Log("[GameManager] Đã reset trò chơi.");
        }

        public void AutoCorrectConnections()
        {
            var draggables = FindObjectsOfType<DraggableObject>();
            var slots = FindObjectsOfType<NumberSlot>();

            Debug.Log("[GameManager] Đang tự động kết nối...");

            foreach (var draggable in draggables)
            {
                if (draggable.currentConnectedSlot != null)
                {
                    draggable.currentConnectedSlot.connectedObjects.Remove(draggable);
                    draggable.currentConnectedSlot = null;
                }

                NumberSlot correctSlot = null;
                foreach (var slot in slots)
                {
                    if (draggable.objectCount == slot.numberSlot)
                    {
                        correctSlot = slot;
                        break;
                    }
                }

                if (correctSlot != null)
                {
                    correctSlot.AddConnection(draggable);
                    draggable.ConnectToSlot(correctSlot);
                    Debug.Log($"[GameManager] Đối tượng {draggable.name} được kết nối với {correctSlot.name}");
                }
                else
                {
                    Debug.LogWarning($"[GameManager] Không tìm thấy số đúng cho {draggable.name}");
                }
            }
        }

        // Timer Methods
        private void StopTimer()
        {
            isTimerRunning = false;
        }

        private void StartTimer()
        {
            timeElapsed = 0f;
            isTimerRunning = true;
        }

        private string GetFormattedTime()
        {
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
