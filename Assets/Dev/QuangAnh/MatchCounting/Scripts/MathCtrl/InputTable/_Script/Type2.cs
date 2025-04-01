using MathCounting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class Type2 : InputTable {

        public override string AddInputValue( string value ) {

            inputValue += value;
            MathCtrl.Instance.ConfirmAnswer();
            return inputValue;
            
        }


    }
}
