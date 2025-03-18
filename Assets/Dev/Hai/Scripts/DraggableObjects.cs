using UnityEngine;

namespace DragDrop
{
    public class DraggableObject : MonoBehaviour
    {
        private float cameraDistance;
        [SerializeField]private bool isDragging = false;
        [SerializeField]private DropZone currentDropZone;
        // Giới hạn vùng kéo
        [SerializeField]private BoxCollider boardBoundary;

        void Start()
        {
            GameObject Board = GameObject.FindGameObjectWithTag("BoardCanvas");
            boardBoundary = Board.GetComponent<BoxCollider>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Ưu tiên đối tượng khi click
                int draggableLayerMask = LayerMask.GetMask("Draggables");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, draggableLayerMask))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                    }
                }
            }

            if (isDragging && Input.GetMouseButton(0))
            {
                // Lấy vị trí chuột theo góc camera
                cameraDistance = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = cameraDistance;
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                
                // Kiểm tra xem bị trí mới có trong vùng giới hạn không
                if (boardBoundary.bounds.Contains(newPosition))
                {
                    transform.position = newPosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if (currentDropZone != null)
                {
                    currentDropZone.AddObject(gameObject);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Dropzones"))
            {
                DropZone dropZone = other.GetComponent<DropZone>();
                if (dropZone != null)
                {
                    currentDropZone = dropZone;
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Dropzones"))
            {
                DropZone dropZone = other.GetComponent<DropZone>();
                if (dropZone != null && currentDropZone == dropZone)
                {
                    currentDropZone.RemoveObject(gameObject);
                    currentDropZone = null;
                }
            }
        }
    }
}
