using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class btnConfirm : BaseBtn {

        [SerializeField] protected FinishUI FinishUI;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadFinishUI();
        }

        protected virtual void LoadFinishUI() {
            if (this.FinishUI != null) return;
            this.FinishUI = GetComponentInParent<FinishUI>();
            Debug.Log(transform.name + ": LoadFinishUI", gameObject);
        }

        protected override void OnClick() {
            
            this.FinishUI.ConfirmUI.Toggle();
            this.FinishUI.ResultLogUI.Toggle();
           
        }
    }
}
