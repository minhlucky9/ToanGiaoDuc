using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace MathCounting {

    public class MathCounting : NewMonobehavior {

        [SerializeField] protected Transform zoomPosition;
        [SerializeField] protected InputTable inputTable;
        [SerializeField] protected ImagePrefab activeImagePrefab;
        public Transform ZoomPosition => zoomPosition;
        public InputTable InputTable => inputTable;
        public ImagePrefab ActiveImagePrefab => activeImagePrefab;

        public bool isAnyActive = false;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadZoomPosition();
            this.LoadInputTable();
        }

        protected virtual void LoadZoomPosition() {
            if (zoomPosition != null) return;
            this.zoomPosition = transform.Find("ZoomPosition");
            Debug.Log(transform.name + ": LoadZoomPosition", gameObject);
        }

        protected virtual void LoadInputTable() {
            if (inputTable != null) return;
            this.inputTable = GameObject.FindAnyObjectByType<InputTable>();
            Debug.Log(transform.name + ": LoadInputTable", gameObject);
        }

        public virtual void SetImagePrefab(ImagePrefab prefab) {
            this.activeImagePrefab = prefab;
            this.isAnyActive = true;
        }
        

    }


}

