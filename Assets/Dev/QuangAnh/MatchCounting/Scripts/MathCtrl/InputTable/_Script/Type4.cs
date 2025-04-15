using MathCounting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class Type4 : InputTable {

        public override string AddInputValue( string value ) {

            inputValue += value;
            MathCtrl.Instance.ConfirmAnswer();
            return inputValue;

        }


    }
}
