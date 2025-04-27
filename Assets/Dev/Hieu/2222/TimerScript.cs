using TMPro;
using UnityEngine;

namespace ColorPaint
{
    public class TimerScript : MonoBehaviour
    {
        [Header("Timer UI")]
        public TextMeshProUGUI timerText;

        private float timeElapsed = 0f;
        private bool isRunning = true;

        void Start()
        {
            isRunning = true;
        }

        void Update()
        {
            if (isRunning)
            {
                timeElapsed += Time.deltaTime;
                UpdateTimerText();
            }
        }

        void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public string GetFormattedTime()
        {
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        public void StartTimer()
        {
            timeElapsed = 0f;
            isRunning = true;
        }
    }
}
