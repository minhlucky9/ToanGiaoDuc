
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Timeline;
using System.Linq;
using Unity.VisualScripting;

namespace MathCounting {
    
    public class ImagePrefab : InputPrefab {

        //
        [SerializeField] protected PlayableDirector timeLine;
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected ZoomPosition zoomPositon;
        protected Vector3 zoomPos = new Vector3(0.01f, 1.69f, 6.09f);
        private bool isZoomingIn = false;
        private Coroutine zoomCoroutine;
        [SerializeField] private Image imageUI;

        [Header("Initalize Image Prefab")]
        [SerializeField] protected Sprite image;
        [SerializeField] protected string CorrectAnswer = "";


        protected override void OnValidate() {
            base.OnValidate();
            this.SetChangeValue();
        }

        protected void SetChangeValue() {
            if (this.CheckingNumber.Answer == this.CorrectAnswer) return;
            this.CheckingNumber.SetObj(this.CorrectAnswer);
            this.imageUI.sprite = image;
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTimeLine();
            this.LoadTextMeshPro();
            this.LoadCanvas();
            this.LoadImageUI();
            this.LoadCheckingNumber();
            this.LoadZoomPositon();
        }

        protected virtual void LoadImageUI() { 
            if (imageUI != null) return;
            this.imageUI = GetComponentInChildren<Image>();
            Debug.Log(transform.name + ": LoadImageUI " + gameObject);
        }


        protected virtual void LoadZoomPositon() {
            if (zoomPositon != null && zoomPositon != default) return;
            this.zoomPositon = GameObject.FindAnyObjectByType<ZoomPosition>();
            Debug.Log(transform.name + ": LoadZoomPositon " + gameObject);
        }

        protected virtual void LoadTimeLine() {
            if (this.timeLine != null) return;
            this.timeLine = GetComponentInParent<PlayableDirector>();
            Debug.Log(transform.name + ": LoadTimeLine " + gameObject);
        }

        protected virtual void LoadCanvas() {
            if (canvas != null) return;
            this.canvas = transform.GetComponentInChildren<Canvas>();
            Debug.Log(transform.name + ": LoadCanvas " + gameObject);
        }




        public virtual void OrderLayer(int order) {
            // Update Layer in TimeLine
            /*Debug.Log(transform.name + ": OrderLayer" + gameObject);*/
            canvas.sortingOrder = order;
        }


        protected override void OnClick() {
            // Onclick action
           /* Debug.Log(transform.name + ": OnClick" + gameObject);*/
            if (MathCtrl.Instance.isAnyActive) return;

            MathCtrl.Instance.SetActivePrefab(this);
            MathCtrl.Instance.InputTable.Show();

            ToggleZoom();
        }

        private void ToggleZoom() {
            // Toggle Animation ZoomIn/Out
            

            if (zoomCoroutine != null) {
                StopCoroutine(zoomCoroutine);
            }
            isZoomingIn = !isZoomingIn;
            zoomCoroutine = StartCoroutine(Zoom());
        }
        private Vector3 originalPosition;
        private Quaternion originalRotation;

        private IEnumerator Zoom() {
            if (isZoomingIn) {
                
                originalPosition = transform.position;

                timeLine.time = 0;
                timeLine.Play();

                zoomPositon.Show();

                while (timeLine.time < 0.5f){

                    /* Debug.Log(zoomPositon.transform.position);
                     Debug.Log(zoomPositon.transform.rotation);*/
                    if (zoomPositon.transform.position != zoomPos || zoomPositon.transform.rotation != Quaternion.identity) {
                        transform.position = Vector3.MoveTowards(transform.position, zoomPositon.transform.position, Time.deltaTime * 5);
                    } else transform.position = Vector3.MoveTowards(transform.position, zoomPos, Time.deltaTime * 5);
                    
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * 200);
                    yield return null;
                }
                

            } else {
                timeLine.time = 0.5f;
                timeLine.Play();

                while (timeLine.time > 0) {
                    transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * 5);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * 200);
                    timeLine.time -= Time.deltaTime;
                    yield return null;
                }
                zoomPositon.Hide();
                this.OrderLayer(1);
            }

            timeLine.Stop();
        }

        public virtual void ZoomOut() {
            // Zoom Out
            ToggleZoom();
        }



    }
}

