using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathCounting
{
    public class CheckingNumber : NewMonobehavior {
        [SerializeField] private string correctAnswer;
        [SerializeField] private InputPrefab inputPrefab;

        public bool isCorrect = false;

        public string Answer => correctAnswer;


        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadInputPrefab();
        }

        protected virtual void LoadInputPrefab() {
            if (this.inputPrefab != null) return;
            this.inputPrefab = GetComponentInParent<InputPrefab>();
            Debug.Log(transform.name + ": LoadInputPrefab: ", gameObject);
        }


        public virtual void SetObj( string answer ) {
            this.correctAnswer = answer;
        }



        public bool DoChecking( string number ) {

            if (correctAnswer == number) {

                if (isCorrect) return true;

                this.isCorrect = true;
                return true;
            }
            ResultCtrl.Instance.MistakeCount++;
            this.isCorrect = false;
            return false;

        }

        public void CompleteAnswer() {

            inputPrefab.EnterNumber(this.correctAnswer);

            this.isCorrect = true;
            
        }


    }
}
