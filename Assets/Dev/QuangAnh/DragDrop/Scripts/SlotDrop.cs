using MathCounting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.QuangAnh.WallDragDrop
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SlotDrop : NewMonobehavior {
        [SerializeField] protected Image image;
        [SerializeField] protected BoxCollider2D boxCollider2D;

        [Header("Checking")]
        public bool isCorrect = false;
        public DragDropEnum accpectEnum;
        public float maxSnapDistance = 0.3f;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImage();
            this.LoadBoxCollider2D();
        }

        protected virtual void LoadBoxCollider2D() { 
            if(this.boxCollider2D != null) return;
            this.boxCollider2D = this.GetComponent<BoxCollider2D>();
            this.boxCollider2D.size = new Vector2(0.3f, 0.3f);
            Debug.Log(transform.name + ": LoadBoxCollider2D: ", gameObject);
        }

        protected virtual void LoadImage() {
            if (this.image != null) return;
            this.image = GetComponentInChildren<Image>();
            Debug.Log(transform.name + ": LoadImage: ", gameObject);
        }

        public virtual bool Checking(DragObject dragObject) {

            if (this.isCorrect) return false;

            float distance = Vector2.Distance(dragObject.transform.position, this.transform.position);

            if (distance <= maxSnapDistance) {
                if (dragObject.targetEnum == accpectEnum && dragObject.targetEnum != DragDropEnum.none) {
                    /* Debug.Log($"Snapped to slot: {dragObject.transform.name}", this.transform.gameObject);*/
                    dragObject.transform.position = this.transform.position + new Vector3(0, 0, -0.00001f);
                    dragObject.transform.rotation = this.transform.rotation;
                    dragObject.SetIsFinish(true);
                    this.isCorrect = true;

                    StartCoroutine(dragObject.dragObjectCtrl.Animation(true));

                    return true;
                } else {

                    StartCoroutine(dragObject.dragObjectCtrl.Animation(false));
                    ResultLog.Instance.MistakeAnswer++;
                    dragObject.BackPosition();
                    return false;

                }
            }

            return false;

        }


    }
}
