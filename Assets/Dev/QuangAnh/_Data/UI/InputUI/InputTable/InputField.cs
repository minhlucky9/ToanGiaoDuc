using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



namespace MathCounting {
    public class InputField : NewMonobehavior {

        [SerializeField] protected TextMeshProUGUI textInputField;  
        
        public TextMeshProUGUI TextInputField => textInputField;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTextMeshPro();
        }

        protected virtual void LoadTextMeshPro() {
            if (textInputField != null) return;
            this.textInputField = transform.GetComponentInChildren<TextMeshProUGUI>();
            textInputField.text = "0";
            Debug.Log(transform.name + ": LoadTextMeshPro" + gameObject);
        }

        public void AddNumber( string number ) {
            if (textInputField == null || textInputField.text.Length >= 3) return;
            textInputField.text = (textInputField.text == "0") ? "" : textInputField.text;

            textInputField.text += number.Substring(0, Mathf.Min(number.Length, 3 - textInputField.text.Length));
        }

        public void ClearNumber() {
            textInputField.text = "0";
        }

        

    }

}

