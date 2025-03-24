using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class ConfirmUI : NewMonobehavior {
        protected override void Awake() {
            base.Awake();
            Hide();
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void Toggle() {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
