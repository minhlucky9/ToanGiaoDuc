using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestionSystem {
    public class ImageSwap : NewMonobehavior {

        [SerializeField] protected Image image;


        /*#3b5dff / #ef2e1b / #5dce55 */

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImage();
        }

        protected virtual void LoadImage() {
            if (this.image != null) return;
            this.image = GetComponent<Image>();
            this.DefaultImage();
            Debug.Log(transform.name + ": LoadImage: " + gameObject);
        }


        public virtual void CorrectImage() {
            this.image.color = new Color32(93, 206, 85, 255); // Màu #5dce55
        }

        public virtual void WrongImage() {
            this.image.color = new Color32(239, 46, 27, 255); // Màu #ef2e1b
        }

        public virtual void DefaultImage() {
            this.image.color = new Color32(59, 93, 255, 255); // Màu #3b5dff
        }




    }

}
