using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class btnComplete : BaseBtn {

        [SerializeField] protected FinishUI finishUI;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadFinishUI();
        }

        protected virtual void LoadFinishUI() {
            if (this.finishUI != null) return;
            this.finishUI = transform.parent.GetComponentInChildren<FinishUI>();
            Debug.Log(transform.name + ": LoadFinishUI", gameObject);
        }

        protected override void OnClick() {
            finishUI.gameObject.SetActive(true);
            finishUI.ConfirmUI.Toggle();
        }


    }
}