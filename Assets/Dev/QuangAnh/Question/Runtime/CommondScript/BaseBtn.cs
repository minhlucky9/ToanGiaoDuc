using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestionSystem {

    [RequireComponent(typeof(Button))]
    public abstract class BaseBtn : NewMonobehavior {

        [SerializeField] protected Button button;

        protected override void Start() {
            base.Start();
            this.AddOnClickEvent();
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadButton();
            
        }

       

        protected virtual void LoadButton() {
            if (this.button != null) return;
            this.button = GetComponent<Button>();
            Debug.LogWarning(transform.name + ": Loadbutton", gameObject);
        }

        protected virtual void AddOnClickEvent() {
            this.button.onClick.AddListener(this.OnClick);
        }


        protected abstract void OnClick();
    }
}
