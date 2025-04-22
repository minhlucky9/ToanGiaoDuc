using UnityEngine;

namespace MathConnection
{
    public class DraggableObject : MonoBehaviour
    {
        public int objectCount;
        public float zoomScale = 1.2f;
        public Transform startPoint;
        public float connectDistanceThreshold = 1f;

        private bool isDragging;
        private Vector3 originalScale;
        private LineRenderer lineRenderer;
        public NumberSlot currentConnectedSlot;

        private void Start()
        {
            originalScale = transform.localScale;

            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
            lineRenderer.positionCount = 0;
            lineRenderer.startWidth = 0.025f;
            lineRenderer.endWidth = 0.025f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;

            lineRenderer.sortingLayerName = "Connections";
            lineRenderer.sortingOrder = -1;

            Debug.Log($"[DraggableObject] {name} khởi tạo thành công.");
        }

        private void OnMouseDown()
        {
            isDragging = true;
            ZoomIn();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint != null ? startPoint.position : transform.position);
            Debug.Log($"[DraggableObject] {name} đang được kéo.");
        }

        private void OnMouseUp()
        {
            isDragging = false;
            ZoomOut();

            NumberSlot closestSlot = GetClosestSlotToMouse();
            if (closestSlot != null)
            {
                if (currentConnectedSlot != closestSlot)
                {
                    if (currentConnectedSlot != null)
                    {
                        currentConnectedSlot.connectedObjects.Remove(this);
                        currentConnectedSlot = null;
                    }
                    closestSlot.AddConnection(this);
                    Debug.Log($"[DraggableObject] {name} kết nối với {closestSlot.name}");
                }
            }
            else
            {
                ClearLine();
                Debug.Log($"[DraggableObject] {name} không kết nối được, xóa đường nối.");
            }
        }

        private void Update()
        {
            if (isDragging)
            {
                Vector3 mouseWorldPos = GetMouseWorldPosition();
                lineRenderer.SetPosition(1, mouseWorldPos);
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(-6.52f - Camera.main.transform.position.z);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = -6.52f;
            return mouseWorldPos;
        }

        private NumberSlot GetClosestSlotToMouse()
        {
            NumberSlot[] slots = FindObjectsOfType<NumberSlot>();
            Vector3 mouseWorldPos = GetMouseWorldPosition();

            NumberSlot closestSlot = null;
            float minDistance = float.MaxValue;

            foreach (var slot in slots)
            {
                float distance = Vector3.Distance(mouseWorldPos, slot.transform.position);
                if (distance < minDistance && distance <= connectDistanceThreshold)
                {
                    minDistance = distance;
                    closestSlot = slot;
                }
            }
            return closestSlot;
        }

        private void ZoomIn() => transform.localScale = originalScale * zoomScale;
        private void ZoomOut() => transform.localScale = originalScale;

        public void ConnectToSlot(NumberSlot slot)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint != null ? startPoint.position : transform.position);
            lineRenderer.SetPosition(1, slot.transform.position);
            currentConnectedSlot = slot;
            Debug.Log($"[DraggableObject] {name} đã kết nối với {slot.name}");
        }

        private void ClearLine()
        {
            if (currentConnectedSlot != null)
            {
                currentConnectedSlot.connectedObjects.Remove(this);
            }
            currentConnectedSlot = null;
            lineRenderer.positionCount = 0;
        
        }

        public void ClearLineRenderer()
        {
            lineRenderer.positionCount = 0;
  
        }
    }
}
