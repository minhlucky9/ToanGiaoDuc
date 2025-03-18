using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;




namespace MathCounting {
    public class btnAnswer : BtnAbstact {

        [SerializeField] protected string numberValue; // Detect in LoadTextMeshPro using textMeshPro value
        [SerializeField] protected TextMeshProUGUI answerTextUI;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTextMeshPro();
        }

        protected virtual void LoadTextMeshPro() { 
            if(answerTextUI != null) return;  
            this.answerTextUI = transform.GetComponentInChildren<TextMeshProUGUI>();
            this.numberValue = !string.IsNullOrEmpty(answerTextUI.text) ? answerTextUI.text : "";
            Debug.Log(transform.name + ": LoadTextMeshPro" + gameObject);
        }


        protected override void OnClick() {
           MathCounting.Instance.InputTable.AddInputValue(numberValue.ToString());
        } 


    }
}

