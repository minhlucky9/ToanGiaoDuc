using UnityEngine;
using TMPro;

namespace MathConnection
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameObject scorePanel;
        public TextMeshProUGUI correctText;
        public TextMeshProUGUI wrongText;
        public TextMeshProUGUI mistakeText;
        public TextMeshProUGUI timeText;
        public TimerScript timerScript;

        public int mistakeCount = 0;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            scorePanel.SetActive(false);
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

            timerScript.StopTimer();

            correctText.text = $" {correctCount}";
            wrongText.text = $" {wrongCount}";
            mistakeText.text = $" {mistakeCount}";
            timeText.text = $" {timerScript.GetFormattedTime()}";

            Debug.Log($"[GameManager] Kết quả: Đúng: {correctCount} - Sai: {wrongCount} - Lỗi: {mistakeCount}");

            scorePanel.SetActive(true);
            ClearAllLines();
        }

        public void ClearAllLines()
        {
            var draggables = FindObjectsOfType<DraggableObject>();
            foreach (var draggable in draggables)
            {
                draggable.ClearLineRenderer();
            }
            Debug.Log("[GameManager] Đã xóa tất cả đường nối.");
        }

        public void ResetGame()
        {
            mistakeCount = 0;
            var draggables = FindObjectsOfType<DraggableObject>();
            foreach (var draggable in draggables)
            {
                draggable.ClearLineRenderer();
                draggable.transform.position = draggable.startPoint.position;
                draggable.currentConnectedSlot = null;
            }

            Debug.Log("[GameManager] Đã reset trò chơi.");

            scorePanel.SetActive(false);
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
    }
}
