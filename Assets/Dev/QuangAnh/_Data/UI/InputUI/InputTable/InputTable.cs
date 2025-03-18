using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace MathCounting {
    public class InputTable : NewMonobehavior {

        [SerializeField] protected InputField inputField;

        public InputField InputField => inputField;

        protected override void Awake() {
            base.Awake();
        }

        protected override void Start() {
            this.transform.gameObject.SetActive(false);
        }

        protected override void OnValidate() {
            base.OnValidate();
            this.LoadComponents();
        }

        protected override void LoadComponents() { 
            base.LoadComponents();
            this.LoadInputField();
        }

        protected virtual void LoadInputField() {

            if (inputField != null) return;
            this.inputField = GetComponentInChildren<InputField>();
            Debug.Log(transform.name + ": LoadInputField" + gameObject);

        }
        public void Show() {
            transform.gameObject.SetActive(true);
        }
        public void Hide() {
            transform.gameObject.SetActive(false);
        }


       


    }

}

