using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PairObject
{
    public class SnapPoint : MonoBehaviour
    {
        DraggableObject currentDraggableObject;
        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && currentDraggableObject != null)
            {
                currentDraggableObject.transform.position = transform.position;
            }
        }

        public bool IsSnapped() { return currentDraggableObject != null; }

        private void OnTriggerEnter(Collider other)
        {
            if (currentDraggableObject != null) return;

            DraggableObject draggableObject = other.GetComponent<DraggableObject>();
            if (draggableObject != null)
            {
                currentDraggableObject = draggableObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (currentDraggableObject == null) return;

            DraggableObject draggableObject = other.GetComponent<DraggableObject>();
            if (draggableObject != null && currentDraggableObject == draggableObject)
            {
                currentDraggableObject = null;
            }
        }
    }

}

