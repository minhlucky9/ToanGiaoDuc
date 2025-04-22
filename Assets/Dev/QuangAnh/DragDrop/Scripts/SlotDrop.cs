using MathCounting;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.QuangAnh.WallDragDrop
{
    [RequireComponent(typeof(BoxCollider2D))]

    public class SlotDrop : NewMonobehavior {
        public Image image;
        [SerializeField] protected BoxCollider2D boxCollider2D;

        public Sprite spriteImage;

        [Header("Checking")]
        public bool isCorrect = false;
        public float maxSnapDistance = 0.3f;

        protected override void OnValidate() {
            base.OnValidate();
            if (image == null) return;
            image.sprite = spriteImage;
#if UNITY_EDITOR
            EditorApplication.delayCall += () => {
                if (image != null)
                    image.SetNativeSize();
            };
#endif
        }


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


            if(dragObject == null) return false;
            if (this.isCorrect) return false;

            float distance = Vector2.Distance(dragObject.transform.position, this.transform.position);

            if (distance <= maxSnapDistance) {
                if (dragObject.m_slotDrop == this && dragObject.m_slotDrop != null) {
                    /* Debug.Log($"Snapped to slot: {dragObject.transform.name}", this.transform.gameObject);*/
                    dragObject.transform.position = this.transform.position;
                    dragObject.transform.rotation = this.transform.rotation;
                    dragObject.SetIsFinish(true);
                    dragObject.boxCollider2D.enabled = false;
                    this.boxCollider2D.enabled = false;
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
