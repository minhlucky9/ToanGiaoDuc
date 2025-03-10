using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;


namespace MathCounting {
    public class ImagePrefab : BtnAbstact {

        // Need Refactor
        [SerializeField] protected PlayableDirector timeLine;
        [SerializeField] protected TextMeshProUGUI number;
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected CheckingNumber checkingNumber;
    
        public CheckingNumber CheckingNumber => checkingNumber;

        [SerializeField] protected Image image;

        private bool isZoomingIn = false;
        private Coroutine zoomCoroutine;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTimeLine();
            this.LoadTextMeshPro();
            this.LoadCanvas();
            this.LoadCheckingNumber();
            this.LoadImage();
        }

        protected virtual void LoadTimeLine() {
            if (this.timeLine != null) return;
            this.timeLine = GetComponent<PlayableDirector>();
            Debug.Log(transform.name + ": LoadTimeLine " + gameObject);
        }

        protected virtual void LoadTextMeshPro() {
            if (number != null) return;
            this.number = transform.GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadTextMeshPro" + gameObject);
        }

        protected virtual void LoadCanvas() {
            if (canvas != null) return;
            this.canvas = transform.GetComponentInChildren<Canvas>();
            Debug.Log(transform.name + ": LoadCanvas" + gameObject);
        }


        protected virtual void LoadCheckingNumber() {
            if (checkingNumber != null) return;
            this.checkingNumber = transform.GetComponentInChildren<CheckingNumber>();
            Debug.Log(transform.name + ": LoadCheckingNumber" + gameObject);
        }

        protected virtual void LoadImage() { 
           
            this.image = transform.GetComponentInChildren<Image>();

            image.sprite = CheckingNumber.ImagePrefabObj.image;

        } 

        public virtual void EnterNumber(string text) {
            number.text = text;
        }

        public virtual void OrderLayer(int order) {
            /*Debug.Log(transform.name + ": OrderLayer" + gameObject);*/
            canvas.sortingOrder = order;
        }


        protected override void OnClick() {

            if (!CountdownCtrl.Instance.isGameStarted) return;
            if (mathCounting.isAnyActive) return;

            mathCounting.SetImagePrefab(this);
            mathCounting.InputTable.OnActive();

            ToggleZoom();
        }

        private void ToggleZoom() {
            if (zoomCoroutine != null) {
                StopCoroutine(zoomCoroutine);
            }
            isZoomingIn = !isZoomingIn;
            zoomCoroutine = StartCoroutine(Zoom());
        }

        private IEnumerator Zoom() {

            if (isZoomingIn) timeLine.time = 0;
            else timeLine.time = 0.5f;
            timeLine.Play();
            if (isZoomingIn) {
                while (timeLine.time < 0.5f) {
                    
                    yield return null;
                }
            } else {
                while (timeLine.time > 0) {
                    timeLine.time -= Time.deltaTime;
                    yield return null;
                    
                }
                this.OrderLayer(1);
            }
            
            timeLine.Stop();
        }

        public virtual void ZoomOut() {
            ToggleZoom();
        }

    }
}

