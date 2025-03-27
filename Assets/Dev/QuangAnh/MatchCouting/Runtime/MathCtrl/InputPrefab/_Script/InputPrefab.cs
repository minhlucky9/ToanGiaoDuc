using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MathCounting
{
    public abstract class InputPrefab : BtnAbstact {

        [SerializeField] protected TextMeshProUGUI number;
        [SerializeField] protected CheckingNumber checkingNumber;
        public CheckingNumber CheckingNumber => checkingNumber;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTextMeshPro();
            this.LoadCheckingNumber();
        }
        protected virtual void LoadTextMeshPro() {
            if (number != null) return;
            this.number = transform.GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadTextMeshPro " + gameObject);
        }

        protected virtual void LoadCheckingNumber() {
            if (checkingNumber != null) return;
            this.checkingNumber = transform.GetComponentInChildren<CheckingNumber>();
            Debug.Log(transform.name + ": LoadCheckingNumber " + gameObject);
        }



        public virtual void EnterNumber( string text ) {
            // Input table take number to prefab
            number.text = text;
        }
    }
}
