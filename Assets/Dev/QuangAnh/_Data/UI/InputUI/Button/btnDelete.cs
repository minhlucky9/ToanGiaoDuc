using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {

    public class btnDelete : BtnAbstact {
        protected override void OnClick() {
            MathCounting.Instance.InputTable.InputField.ClearNumber();
        }

    }


}
