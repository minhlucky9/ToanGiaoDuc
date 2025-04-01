using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {

    public class btnDelete : BtnAbstact {
        protected override void OnClick() {

            // Clear Value
            MathCtrl.Instance.InputTable.ClearInputValue();
        }

    }


}
