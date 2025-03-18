using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace MathCounting {

    public class MathCounting : Singleton<MathCounting> {

        
        [SerializeField] protected InputTable inputTable;
        [SerializeField] protected ImagePrefab activeImagePrefab;
        
        public InputTable InputTable => inputTable;
        public ImagePrefab ActiveImagePrefab => activeImagePrefab;

        public bool isAnyActive = false;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadInputTable();
        }

        protected virtual void LoadInputTable() {
            if (inputTable != null && inputTable != default) return;
            this.inputTable = FindAnyObjectByType<Type1>();
            Debug.Log(transform.name + ": LoadInputTable", gameObject);
        }

        public virtual void SetActivePrefab(ImagePrefab prefab) {
            this.activeImagePrefab = prefab;
            this.isAnyActive = true;
        }
        

    }


}

