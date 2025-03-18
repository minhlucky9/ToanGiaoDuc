
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Timeline;
using System.Linq;
using Unity.VisualScripting;
using mathCounting;
namespace MathCounting {
    
    [RequireComponent(typeof(Button))]
    public class ImagePrefab : BtnAbstact {

        // Need Refactor
        [SerializeField] protected PlayableDirector timeLine;
        [SerializeField] protected TextMeshProUGUI number;
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected CheckingNumber checkingNumber;
        [SerializeField] protected ZoomPosition zoomPositon;

        protected Vector3 zoomPos = new Vector3(0.01f, 1.69f, 6.09f);


        public CheckingNumber CheckingNumber => checkingNumber;
        protected Image image;

        private bool isZoomingIn = false;
        private Coroutine zoomCoroutine;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTimeLine();
            this.LoadTextMeshPro();
            this.LoadCanvas();
            this.LoadCheckingNumber();
            this.LoadZoomPositon();
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

        protected virtual void LoadTextMeshPro() {
            if (number != null) return;
            this.number = transform.GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadTextMeshPro " + gameObject);
        }

        protected virtual void LoadCanvas() {
            if (canvas != null) return;
            this.canvas = transform.GetComponentInChildren<Canvas>();
            Debug.Log(transform.name + ": LoadCanvas " + gameObject);
        }


        protected virtual void LoadCheckingNumber() {
            if (checkingNumber != null) return;
            this.checkingNumber = transform.GetComponentInChildren<CheckingNumber>();
            Debug.Log(transform.name + ": LoadCheckingNumber " + gameObject);
        }
        


        public virtual void EnterNumber(string text) {
            // Input table take number to prefab
            number.text = text;
        }

        public virtual void OrderLayer(int order) {
            // Update Layer in TimeLine
            /*Debug.Log(transform.name + ": OrderLayer" + gameObject);*/
            canvas.sortingOrder = order;
        }


        protected override void OnClick() {
            // Onclick action
            if (!CountdownCtrl.Instance.isGameStarted) return;
            if (MathCounting.Instance.isAnyActive) return;

            MathCounting.Instance.SetActivePrefab(this);
            MathCounting.Instance.InputTable.Show();

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

                transform.position = Vector3.MoveTowards(transform.position, zoomPos, Time.deltaTime * 5);
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

