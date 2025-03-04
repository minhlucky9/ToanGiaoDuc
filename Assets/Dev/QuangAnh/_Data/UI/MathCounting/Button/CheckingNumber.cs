using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting
{
    public class CheckingNumber : NewMonobehavior
    {
        [SerializeField] private ImagePrefabObj imagePrefabObj;
        
        public ImagePrefabObj ImagePrefabObj => imagePrefabObj;


        public void SetObj( ImagePrefabObj newObj ) { 
            this.imagePrefabObj = newObj;
        }

        public bool  DoChecking (string number) {
            Debug.Log("Checking Number: " + imagePrefabObj.finishNumber + " || " + number);
            if (imagePrefabObj.finishNumber == number) return true;
            

            return false;

        }





    }
}
