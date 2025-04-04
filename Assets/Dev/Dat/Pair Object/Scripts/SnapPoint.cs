using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PairObject
{
    public class SnapPoint : MonoBehaviour
    {
        DraggableObject currentDraggableObject;
        BoxCollider boxCollider;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                HandleDraggableObjectsInRange();
            }
        }

        void HandleDraggableObjectsInRange()
        {
            Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, Quaternion.identity);

            foreach (Collider col in colliders)
            {
                DraggableObject draggableObject = col.GetComponent<DraggableObject>();
                if (draggableObject != null)
                {
                    if (IsCurrentDraggableObjectNull())
                    {
                        currentDraggableObject = draggableObject;
                        draggableObject.isTaken = true;
                        currentDraggableObject.transform.position = transform.position;
                    }
                    else if(currentDraggableObject != draggableObject)
                    {
                        draggableObject.BackToStartPosition();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsCurrentDraggableObjectNull()) return;

            DraggableObject draggableObject = other.GetComponent<DraggableObject>();
            if (draggableObject != null && currentDraggableObject == draggableObject)
            {
                currentDraggableObject.isTaken = false;
                currentDraggableObject = null;
            }
        }

        public bool IsCurrentDraggableObjectNull() { return currentDraggableObject == null; }

        public void SetDraggableObject(DraggableObject draggableObject)
        {
            currentDraggableObject = draggableObject;
        }
    }

}