using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlockNumber
{
    public class ModelController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 5f;
        [SerializeField] Transform modelContainerT;

        RectTransform imageRectTransform;
        Vector3 lastInputPosition;

        void Update()
        {
            if (!IsMouseOverUI() || modelContainerT == null) return;

            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                Vector3 deltaInput = GetCurrentInputPosition() - lastInputPosition;
                float rotationX = deltaInput.x * rotationSpeed * Time.deltaTime;
                float rotationY = deltaInput.y * rotationSpeed * Time.deltaTime;
                modelContainerT.Rotate(Vector3.up, -rotationX, Space.World);
                modelContainerT.Rotate(Vector3.right, rotationY, Space.World);
                lastInputPosition = GetCurrentInputPosition();
            }
            else lastInputPosition = GetCurrentInputPosition();
        }

        public void SetModelImage(RawImage image)
        {
            if (image == null) return;
            imageRectTransform = image.rectTransform;
        }

        Vector3 GetCurrentInputPosition()
        {
            if (Input.touchCount > 0) return Input.GetTouch(0).position;
            else return Input.mousePosition;
        }

        bool IsMouseOverUI()
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                imageRectTransform, Input.mousePosition, Camera.main);
        }
    }
}
