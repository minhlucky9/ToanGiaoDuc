using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MathCounting {
    public class ResultLogUI : NewMonobehavior {

        [SerializeField] protected TextMeshProUGUI assignmentTimeText;
        [SerializeField] protected TextMeshProUGUI CorrectCountText;
        [SerializeField] protected TextMeshProUGUI WrongCountText;
        [SerializeField] protected TextMeshProUGUI MistakeCountText;
        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadAssignmentTimeText();
            this.LoadCorrectCountText();
            this.LoadWrongCountText();
            this.LoadMistakeCountText();
        }

        protected virtual void LoadCorrectCountText() { 
            if (this.CorrectCountText != null) return;
            this.CorrectCountText = transform.Find("ResultDetail").Find("CorrectCount").GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadCorrectCountText", gameObject);
        }

        protected virtual void LoadWrongCountText() {
            if (this.WrongCountText != null) return;
            this.WrongCountText = transform.Find("ResultDetail").Find("WrongCount").GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadWrongCountText", gameObject);
        }

        protected virtual void LoadMistakeCountText() {
            if (this.MistakeCountText != null) return;
            this.MistakeCountText = transform.Find("ResultDetail").Find("MistakeCount").GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadMistakeCountText", gameObject);
        }

        protected virtual void LoadAssignmentTimeText() {
            if (this.assignmentTimeText != null) return;
            this.assignmentTimeText = transform.Find("ResultDetail").Find("AssignmentTime").GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadAssignmentTimeText", gameObject);
        }

        protected virtual void OnEnable() {

            int total = ResultCtrl.Instance.QuestionCount;
            int correct = ResultCtrl.Instance.GetCorrectCount();
/*
            this.assignmentTimeText.text = ResultCtrl.Instance.TimerDuration.DurationUI.text;*/
            this.CorrectCountText.text = correct.ToString();
            this.WrongCountText.text = (total - correct).ToString();
            this.MistakeCountText.text = ResultCtrl.Instance.MistakeCount.ToString();
        }


        protected override void Awake() {
            base.Awake();
            Hide();
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void Toggle() {
            gameObject.SetActive(!gameObject.activeSelf);
        }

    }
}
