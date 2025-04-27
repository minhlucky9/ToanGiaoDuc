using MathCounting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.QuangAnh.WallDragDrop {
    public class ResultLog : Singleton<ResultLog> {
    
        [SerializeField] protected DragObjectCtrl dragDropCtrl;

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
            this.LoadDragObjectCtrl();
        }

        protected virtual void LoadDragObjectCtrl() {
            if (this.dragDropCtrl != null) return;
            this.dragDropCtrl = GameObject.FindAnyObjectByType<DragObjectCtrl>();
            Debug.Log(transform.name + ": LoadDragObjectCtrl: ", gameObject);
        }


        protected virtual void CheckResult() {
            foreach (var slot in dragDropCtrl.slotDrops) {

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
