using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PairObject
{
    public class DraggableObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] RectTransform rectTransform;
        RectTransform parentRectTransform;
        Vector2 startPosition;

        public bool isTaken { get; set; }
        public bool isChosen {  get; set; }
        bool isInteractable = true;

        private void Awake()
        {
            parentRectTransform = transform.root.GetComponent<RectTransform>();
            startPosition = rectTransform.localPosition;
        }
        IEnumerator GoToSnapPoint(Vector2 targetPosition)
        {
            while (Vector2.Distance(rectTransform.localPosition, targetPosition) > 0.05f)
            {
                rectTransform.localPosition = Vector2.MoveTowards(
                    rectTransform.localPosition,
                    targetPosition,
                    1000 * Time.deltaTime
                );
                yield return null;
            }
        }

        public void AutoSnap()
        {
            if (isTaken) return;

            SnapPoint[] allSnapPoints = FindObjectsOfType<SnapPoint>();
            if (allSnapPoints.Length <= 0) return;

            foreach (SnapPoint snapPoint in allSnapPoints)
            {
                if (snapPoint.IsCurrentDraggableObjectNull())
                {
                    StopInteraction();
                    snapPoint.SetDraggableObject(this);

                    Vector2 targetPosition = parentRectTransform.
                        InverseTransformPoint(snapPoint.transform.position);
                    StartCoroutine(GoToSnapPoint(targetPosition));
                    return;
                }
            }
        }

        public void BackToStartPosition() { rectTransform.localPosition = startPosition; }

        #region Interaction Functions
        public void OnPointerDown(PointerEventData eventData)
        {
            isChosen = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isInteractable && isChosen)
            {
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
                rectTransform.localPosition = localPoint;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isChosen = false;
        }

        public void StopInteraction()
        {
            isInteractable = false;
        }

        #endregion
    }
}
