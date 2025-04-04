using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace MathCounting {

    public class AutoCompleteAnswer : NewMonobehavior {
        [SerializeField] protected List<CheckingNumber> checkingNumbers = new();


        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadCheckingNumber();
        }

        public virtual void AutoComplete() {

            foreach (CheckingNumber number in checkingNumbers) { 
                if (number != null) {
                    number.CompleteAnswer();
                }
            }

        }


        protected virtual void LoadCheckingNumber() {

            checkingNumbers = new List<CheckingNumber>(GameObject.FindObjectsOfType<CheckingNumber>());

            Debug.Log(transform.name + " LoadCheckingNumber: " + checkingNumbers.Count);
        }

    }
}

