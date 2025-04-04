using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MathCounting {
    public class ResultCtrl : Singleton<ResultCtrl> {
        
        
        [SerializeField] protected List<CheckingNumber> checkingNumbers = new();

        public int MistakeCount = 0;

        public int QuestionCount = 0;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadCheckingNumber();
        }

        public virtual int GetCorrectCount() {

            int CorrectCount = 0;

            foreach(CheckingNumber checkingNumber in checkingNumbers) {
                if(checkingNumber.isCorrect) {
                    CorrectCount++;
                }
            }

            return CorrectCount;
        }

        public virtual int GetWrongCount() {
            return this.QuestionCount - this.GetCorrectCount();
        }

        public virtual int GetMistakeCount() {
            return this.MistakeCount;
        }

        protected virtual void LoadCheckingNumber() {
            if (checkingNumbers.Count > 0 && !this.checkingNumbers.Any(c => c == null)) return;

            checkingNumbers = new List<CheckingNumber>(GameObject.FindObjectsOfType<CheckingNumber>());

            this.QuestionCount = checkingNumbers.Count;

            Debug.Log(transform.name + " LoadCheckingNumber: " + checkingNumbers.Count);
        }





    }
}

