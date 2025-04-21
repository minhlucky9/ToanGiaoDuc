using System.Collections;
using UnityEngine;

namespace MathCounting {
    public class Type3 : InputTable {
        //Setting for animation
        private float moveDistance = 50f;
        private float animationDuration = 0.3f;

        private Vector3 originalScale;
        private Vector3 originalPosition;

        protected override void Awake() {
            base.Awake();
            originalScale = transform.localScale;
        }

        public void Show( Vector3 newPosition ) {
            originalPosition = newPosition; 
            transform.position = newPosition; 
            transform.localScale = Vector3.zero;
            moveDistance = Screen.height * 0.1f;
            transform.gameObject.SetActive(true);
            StartCoroutine(AnimateShow());
        }

        public override void Hide() {
            StartCoroutine(AnimateHide());
        }

        private IEnumerator AnimateShow() {
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration) {
                float t = elapsedTime / animationDuration;
                transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
                transform.position = Vector3.Lerp(originalPosition, originalPosition + new Vector3(0, moveDistance, 0), t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = originalScale;
            transform.position = originalPosition + new Vector3(0, moveDistance, 0);
        }

        private IEnumerator AnimateHide() {
            float elapsedTime = 0f;
            Vector3 targetPosition = originalPosition + new Vector3(0, moveDistance, 0);

            while (elapsedTime < animationDuration) {
                float t = elapsedTime / animationDuration;
                transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
                transform.position = Vector3.Lerp(targetPosition, originalPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = Vector3.zero;
            transform.position = originalPosition;
            transform.gameObject.SetActive(false);
        }

        public override string AddInputValue( string value ) {
            inputValue += value;
            MathCtrl.Instance.ConfirmAnswer();
            return inputValue;
        }
    }
}
