using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathCounting {

    public abstract class BaseBtn : NewMonobehavior {
       

        [SerializeField] protected Button button;
        [SerializeField] protected InputTable inputTable;
        [SerializeField] protected MathCounting mathCounting;

        protected override void Start() {
            base.Start();
            this.AddOnClickEvent();   
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadButton();
            this.LoadCaculator();
            this.LoadMatchCounting();
        }

        protected virtual void LoadCaculator() {
            if (inputTable != null) return;
            this.inputTable = FindAnyObjectByType<InputTable>();
            Debug.Log(transform.name + ": LoadCaculator " + gameObject);

        }

        protected virtual void LoadButton() {
            if (this.button != null) return;
            this.button = GetComponent<Button>();
            Debug.LogWarning(transform.name + ": Loadbutton", gameObject);
        }

        protected virtual void LoadMatchCounting() {
            if (this.mathCounting != null) return;
            this.mathCounting = FindAnyObjectByType<MathCounting>();
            Debug.Log(transform.name + ": LoadMathCounting " + gameObject);
        }

        protected virtual void AddOnClickEvent() {
            this.button.onClick.AddListener(this.OnClick);
        }


        protected abstract void OnClick();



    }


}
