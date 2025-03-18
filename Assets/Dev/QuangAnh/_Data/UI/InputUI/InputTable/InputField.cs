using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MathCounting {
    
    public abstract class InputField : NewMonobehavior {

        [SerializeField] protected TextMeshProUGUI textInputField;  
        [SerializeField] protected InputTable inputTable;
        public TextMeshProUGUI TextInputField => textInputField;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTextMeshPro();
            this.LoadInputTable();
        }


        protected virtual void FixedUpdate() {
            this.UpdatingText();
        }

        protected virtual void UpdatingText() {
            if(inputTable.InputValue == null || inputTable.InputValue == "") textInputField.text = "0" ;
            textInputField.text = inputTable.InputValue;
        }


        protected virtual void LoadInputTable() { 
            if (inputTable != null) return;
            this.inputTable = MathCounting.Instance.InputTable;
            Debug.Log(transform.name + ": LoadInputTable" + gameObject);
        }


        protected virtual void LoadTextMeshPro() {
            if (textInputField != null) return;
            this.textInputField = transform.GetComponentInChildren<TextMeshProUGUI>();
            textInputField.text = "0";
            Debug.Log(transform.name + ": LoadTextMeshPro" + gameObject);
        }

        

    }

}

