using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace MathCounting {
    public class TimerDurationUI : NewMonobehavior {
        [SerializeField] protected TextMeshProUGUI durationUI;

        public TextMeshProUGUI DurationUI => durationUI;

        private float elapsedTime = 0f;
        private bool isRunning = false;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTextMeshPro();
        }

        protected virtual void LoadTextMeshPro() {
            if (this.durationUI != null) return;
            this.durationUI = GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadTextMeshPro", gameObject);
        }

        public void StartTimer() {
            Debug.Log("Start counting time");
            ResetTimer();
            isRunning = true;
            StartCoroutine(UpdateTimer());
        }

        public void ResetTimer() {
            isRunning = false;
            elapsedTime = 0f;
            UpdateUIText();
        }

        private IEnumerator UpdateTimer() {
            while (isRunning) {
                yield return new WaitForSeconds(1f);
                elapsedTime += 1f;
                UpdateUIText();
            }
        }

        private void UpdateUIText() {
            if (durationUI != null) {
                int hours = (int)(elapsedTime / 3600);
                int minutes = (int)((elapsedTime % 3600) / 60);
                int seconds = (int)(elapsedTime % 60);
                durationUI.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
            }
        }

    }
}
