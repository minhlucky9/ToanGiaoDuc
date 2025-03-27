using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathCounting
{
    public class CheckingNumber : NewMonobehavior
    {
        [SerializeField] private string answer; 
        
        public bool isCorrect = false;

        public string Answer => answer;

        public virtual void SetObj(string answer) {
            this.answer = answer;
        }

/*        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImage();
        }*/


        public bool  DoChecking (string number) {

            if (answer == number) {

                if (isCorrect) return true;      

                this.isCorrect = true;
                return true; 
            }
            ResultCtrl.Instance.MistakeCount++;
            this.isCorrect = false;
            return false;

        }   



    }
}
