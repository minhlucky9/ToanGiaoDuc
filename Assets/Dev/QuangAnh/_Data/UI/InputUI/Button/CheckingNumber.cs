using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathCounting
{
    public class CheckingNumber : NewMonobehavior
    {
        [SerializeField] private ImageScriptableObj imagePrefabObj;
        [SerializeField] private Image image;
        
        public bool isCorrect = false;
        public ImageScriptableObj ImagePrefabObj => imagePrefabObj;

        public void SetObj( ImageScriptableObj newObj ) { 
            this.imagePrefabObj = newObj;
            this.LoadImage();
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
        protected virtual void LoadImage() {
            //Update Scriptable Obj to prefabs
            this.image = transform.parent.GetComponentInChildren<Image>();
            /*Debug.Log(transform.name + ": LoadImage" + gameObject);*/
            image.sprite = imagePrefabObj.image;

        }




    }
}
