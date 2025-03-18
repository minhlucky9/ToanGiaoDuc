using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;




namespace MathCounting {
    public class btnNumber : BtnAbstact {

        [SerializeField] protected int numberValue;
        [SerializeField] protected TextMeshProUGUI number;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTextMeshPro();
        }

        protected virtual void LoadTextMeshPro() { 
            if(number != null) return;  
            this.number = transform.GetComponentInChildren<TextMeshProUGUI>();
            this.numberValue = int.TryParse(number.text, out int result) ? result : 0;
            Debug.Log(transform.name + ": LoadTextMeshPro" + gameObject);
        }


        protected override void OnClick() {
           MathCounting.Instance.InputTable.InputField.AddNumber(numberValue.ToString());
        } 


    }
}

