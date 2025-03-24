using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MathCounting { 
    public class btnDone : BtnAbstact {

        protected override void OnValidate() {
            base.OnValidate();
            this.LoadComponents();
        }

        protected override void OnClick() {
            
            MathCtrl.Instance.ConfirmAnswer();

        }

        



    }
}
