using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting
{
    public class CheckingNumber : NewMonobehavior
    {
        [SerializeField] private ImagePrefabObj imagePrefabObj;
        
        public bool isCorrect = false;
        public ImagePrefabObj ImagePrefabObj => imagePrefabObj;

        public void SetObj( ImagePrefabObj newObj ) { 
            this.imagePrefabObj = newObj;
        }

        public bool  DoChecking (string number) {

            if (imagePrefabObj.finishNumber == number) {

                if (isCorrect) return true;      

                this.isCorrect = true;
                return true; 
            }
            ResultCtrl.Instance.MistakeCount++;
            this.isCorrect = false;
            return false;

        }





    }
}
