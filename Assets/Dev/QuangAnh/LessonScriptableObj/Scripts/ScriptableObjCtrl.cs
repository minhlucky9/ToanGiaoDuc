using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MathCounting {
    public class ScriptableObjCtrl : NewMonobehavior {

        [SerializeField] private List<ImageScriptableObj> imageScriptableList = new();
        
        public List<ImageScriptableObj> ImagePrefabs => imageScriptableList;


        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImagePrefabs();
        }

        private void LoadImagePrefabs() {
            if (imageScriptableList != null && imageScriptableList.Count > 0) return;
            this.imageScriptableList = new List<ImageScriptableObj>(Resources.LoadAll<ImageScriptableObj>("/"));
            Debug.Log("Load " + imageScriptableList.Count + " ImageScriptableObj");
        }
    }
}
