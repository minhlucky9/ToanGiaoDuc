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

        bool isInteractable = true;
        bool isChosen;

        private void Awake()
        {
            parentRectTransform = transform.root.GetComponent<RectTransform>();
        }

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
    }
}
