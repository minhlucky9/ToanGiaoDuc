using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MathCounting {
    public class ResultCtrl : Singleton<ResultCtrl> {
        
        [SerializeField] protected TimerDurationUI timerDuration;
        [SerializeField] protected List<CheckingNumber> checkingNumbers = new();

        public int MistakeCount = 0;

        public int QuestionCount = 0;

        public TimerDurationUI TimerDuration => timerDuration;

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
            if (checkingNumbers.Count > 0 && !this.checkingNumbers.Any(c => c == null)) return;

            checkingNumbers = new List<CheckingNumber>(GameObject.FindObjectsOfType<CheckingNumber>());

            this.QuestionCount = checkingNumbers.Count;

            Debug.Log(transform.name + " LoadCheckingNumber: " + checkingNumbers.Count);
        }


        protected virtual void LoadTimerDuration() {
            if(timerDuration != null) return;

            timerDuration = GameObject.FindAnyObjectByType<TimerDurationUI>();

            Debug.Log(transform.name + " LoadTimerDuration: " + timerDuration);
        }



    }
}

