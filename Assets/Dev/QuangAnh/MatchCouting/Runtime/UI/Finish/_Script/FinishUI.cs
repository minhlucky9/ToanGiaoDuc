using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class FinishUI : NewMonobehavior {
        [SerializeField] protected ConfirmUI confirmUI;
        [SerializeField] protected ResultLogUI resultLogUI;

        public ConfirmUI ConfirmUI => this.confirmUI;
        public ResultLogUI ResultLogUI => this.resultLogUI;


        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadConfirmUI();
            this.LoadResultLogUI();
            
        }

        protected override void Start() {
            this.transform.gameObject.SetActive(false);
        }

        protected virtual void LoadConfirmUI() {
            if (this.confirmUI != null) return;
            this.confirmUI = GetComponentInChildren<ConfirmUI>();
            Debug.Log(transform.name + ": LoadConfirmUI", gameObject);
        }

        protected virtual void LoadResultLogUI() {
            if (this.resultLogUI != null) return;
            this.resultLogUI = GetComponentInChildren<ResultLogUI>();
            Debug.Log(transform.name + ": LoadResultLogUI", gameObject);
        }



    }

}