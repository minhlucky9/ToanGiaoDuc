using MathCounting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.QuangAnh.WallDragDrop {
    public class ResultLog : Singleton<ResultLog> {
    
        [SerializeField] protected List<SlotDrop> slotDrops = new();

        public int CorrectAnswer;
        public int WrongAnswer;
        public int MistakeAnswer;

        protected override void Start() {
            this.CorrectAnswer = 0;
            this.WrongAnswer = 0;
            this.MistakeAnswer = 0;
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadSlotDrops();
        }

        protected virtual void LoadSlotDrops() { 
            if (slotDrops.Count > 0 && !slotDrops.Any(c => c == null)) return;
            this.slotDrops = new List<SlotDrop>(FindObjectsOfType<SlotDrop>());
            Debug.Log(transform.name + ": LoadSlotDrops: ", gameObject);
        }


        protected virtual void CheckResult() {
            foreach (var slot in slotDrops) {

                if (slot.isCorrect) CorrectAnswer++;
                else WrongAnswer++;

            }
        }

        public virtual void LogAnswer() { 
            CheckResult();

            Debug.Log("Correct Answer" + CorrectAnswer);
            Debug.Log("Wrong Answer" + WrongAnswer);
            Debug.Log("Mistake Answer" + MistakeAnswer);

            this.CorrectAnswer = 0;
            this.WrongAnswer = 0;
            this.MistakeAnswer = 0;
        }



    } 

}
