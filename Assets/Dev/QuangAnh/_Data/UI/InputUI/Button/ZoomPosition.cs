using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class ZoomPosition : NewMonobehavior {

        protected override void Start() {
           Hide();
        }


        public virtual void Show() {
            this.gameObject.SetActive(true);
        }

        public virtual void Hide() {
            this.gameObject.SetActive(false);
        }

    }
}
