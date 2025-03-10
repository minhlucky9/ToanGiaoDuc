using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class ResultCtrl : Singleton<ResultCtrl> {
        
        [SerializeField] protected TimerDurationCtrl timerDuration;
        [SerializeField] protected List<CheckingNumber> checkingNumbers = new();

        public int MistakeCount = 0;

        public int QuestionCount = 0;

        public TimerDurationCtrl TimerDuration => timerDuration;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTimerDuration();
            this.LoadCheckingNumber();
        }

        public virtual int CheckingCorrect() {

            int CorrectCount = 0;

            foreach(CheckingNumber checkingNumber in checkingNumbers) {
                if(checkingNumber.isCorrect) {
                    CorrectCount++;
                }
            }

            return CorrectCount;
        }

        protected virtual void LoadCheckingNumber() {
            if(checkingNumbers.Count > 0) return;

            checkingNumbers = new List<CheckingNumber>(GameObject.FindObjectsOfType<CheckingNumber>());

            this.QuestionCount = checkingNumbers.Count;

            Debug.Log(transform.name + " LoadCheckingNumber: " + checkingNumbers.Count);
        }


        protected virtual void LoadTimerDuration() {
            if(timerDuration != null) return;

            timerDuration = GameObject.FindAnyObjectByType<TimerDurationCtrl>();

            Debug.Log(transform.name + " LoadTimerDuration: " + timerDuration);
        }



    }
}

