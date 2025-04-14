using MathCounting;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dev.QuangAnh.WallDragDrop {
    [ExecuteAlways]
    [RequireComponent(typeof(BoxCollider2D))]
    public class DragObject : NewMonobehavior{
       

        public Canvas canvas;
        public RectTransform reactTransform;
        public BoxCollider2D boxCollider2D;
        public Image imageComponent; 
        public DragObjectCtrl dragObjectCtrl;
        
        private bool isDragging = false;
        private Vector3 offset;
        private Camera mainCam;
        private Vector3 orginalPos;
        private bool isFinish = false;


        [Header("Target")]
        public DragDropEnum targetEnum;
       
        public Sprite imageDragObject;

        protected override void Start() {
            base.Start();
            mainCam = Camera.main;
            orginalPos = transform.position;
        }

        protected override void OnValidate() {
            base.OnValidate();
            if(imageComponent == null) return;
            imageComponent.sprite = imageDragObject;
#if UNITY_EDITOR
            EditorApplication.delayCall += () =>
            {
                if (imageComponent != null)
                    imageComponent.SetNativeSize();
            };
#endif
        }

        protected override void LoadComponents() {
            
            base.LoadComponents();
            this.LoadBoxCollider2D();
            this.LoadCanvas();
            this.LoadRectTransform();
            this.LoadDragObjectCtrl();
            this.LoadImageCompo();

        }


        protected virtual void LoadImageCompo() {
            if (this.imageComponent != null) return;
            this.imageComponent = this.GetComponentInChildren<Image>();
            Debug.Log(transform.name + ": LoadImageCompo: ", gameObject);
        }

        protected virtual void LoadDragObjectCtrl() { 
            if(this.dragObjectCtrl != null) return;
            this.dragObjectCtrl = GameObject.FindAnyObjectByType<DragObjectCtrl>();
            Debug.Log(transform.name + ": LoadDragObjectCtrl: ", gameObject);

        }

        protected virtual void LoadBoxCollider2D() { 
            if (this.boxCollider2D != null) return;
            this.boxCollider2D = this.GetComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.35f, 0.35f);
            Debug.Log(transform.name + ": LoadBoxCollider2D: ", gameObject);
        }

        protected virtual void LoadRectTransform() { 
            if (this.reactTransform != null) return;
            this.reactTransform = this.GetComponent<RectTransform>();
            Debug.Log(transform.name + ": LoadRectTransform: ", gameObject);
        }

        protected void LoadCanvas() {
            if (this.canvas != null) return;
            this.canvas = this.transform.parent.GetComponentInParent<Canvas>();
            Debug.Log(transform.name + ": LoadCanvas: ", gameObject);
        }


        void OnMouseDown() {
          /*  Debug.Log($"{name} OnMouseDown", gameObject);*/
            if(isFinish) return;
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
        }

        void OnMouseUp() {
            isDragging = false;
            TrySnap();
        }

        void Update() {
            if (isDragging) {
                Vector3 worldPos = GetMouseWorldPosition();

                RectTransform canvasRect = canvas.GetComponent<RectTransform>();

                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, mainCam, out localPoint);

                Vector2 objSize = reactTransform.rect.size;

                // Clamp localPoint to canvas rect
                Rect rect = canvasRect.rect;
                float halfWidth = objSize.x / 2f;
                float halfHeight = objSize.y / 2f;

                localPoint.x = Mathf.Clamp(localPoint.x, rect.xMin + halfWidth, rect.xMax - halfWidth);
                localPoint.y = Mathf.Clamp(localPoint.y, rect.yMin + halfHeight, rect.yMax - halfHeight);


                // Convert back to world position
                Vector3 clampedWorldPos = canvas.transform.TransformPoint(localPoint);
                transform.position = clampedWorldPos + offset;
            }
        }

        Vector3 GetMouseWorldPosition() {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return mainCam.ScreenToWorldPoint(mousePos);
        }

        void TrySnap() {

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.00001f);

            bool isCorrectSlot = false;

            foreach (var slot in dragObjectCtrl.slotDrops) {
                if (isCorrectSlot = slot.Checking(this)) return;
            }
        }

        public void BackPosition() { 
            
            Debug.Log(transform.name + ": BackPosition", gameObject);
            transform.position = this.orginalPos;
            transform.rotation = Quaternion.identity;
        }

        public void SetIsFinish( bool isFinish ) { 
            this.isFinish = isFinish;
        }
    }
}
