using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinkPair
{
    public class LinkableObject : MonoBehaviour
    {
        public LinkableObject linkedObject = null;
        private LineRenderer lineRenderer;
        private Camera mainCamera;
        [SerializeField] private bool isDragging = false;
        private Vector3 dragStartPos;
        public int type;

        private LinkPairManager manager;

        // Giới hạn vùng kéo
        [SerializeField] private BoxCollider boardBoundary;

        void Start()
        {
            mainCamera = Camera.main;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.enabled = false;

            GameObject Board = GameObject.FindGameObjectWithTag("BoardCanvas");
            boardBoundary = Board.GetComponent<BoxCollider>();

            manager = GameObject.FindGameObjectWithTag("LinkPairManager").GetComponent<LinkPairManager>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Ưu tiên đối tượng khi click
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Linkables")))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        dragStartPos = transform.position;
                        lineRenderer.enabled = true;
                        lineRenderer.SetPosition(0, dragStartPos);
                    }
                }
            }

            if (isDragging)
            {
                Vector3 mousePosition = Input.mousePosition;
                float cameraDistance = Mathf.Abs(transform.position.z - mainCamera.transform.position.z);
                mousePosition.z = cameraDistance;
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

                // Kiểm tra xem bị trí mới có trong vùng giới hạn không
                if (boardBoundary.bounds.Contains(worldPosition))
                {
                    lineRenderer.SetPosition(1, worldPosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!isDragging) return;
                isDragging = false;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
                LinkableObject target = null;

                // Đọc qua các raycast hit
                foreach (RaycastHit hit in hits)
                {
                    target = hit.collider.GetComponent<LinkableObject>();

                    // Nếu trúng object không có script LinkableObject hoặc chính nó, đi tiếp
                    if (target == null || target == this)
                    {
                        continue;
                    }
                    // Nếu object đang nối không phải loại đang xét, tính là sai, không xét điều kiện khác
                    else if (type != manager.validType1 && type != manager.validType2)
                    {
                        manager.UpdateWrongCount(false);
                        lineRenderer.enabled = false;
                        return;
                    }
                    // Nếu object tìm được cùng loại, hoặc không trong loại đang xét, tính là nối hụt
                    else if (target.type == type || (target.type != manager.validType1 && target.type != manager.validType2))
                    {
                        lineRenderer.enabled = false;
                        if (linkedObject)
                        {
                            Unlink();
                        }

                        manager.UpdateWrongCount(false);
                    }
                    // Nếu object tìm được đã link, báo, +1 lần làm sai
                    else if (target.linkedObject != null && target != this)
                    {
                        lineRenderer.enabled = false;
                        if (linkedObject)
                        {
                            Unlink();
                        }

                        manager.UpdateWrongCount(false);
                        return;
                    }
                    // Nếu trúng object có script, object chưa được link
                    else if (target.linkedObject == null && target != this && target.type != type)
                    {
                        // Nếu object hiện tại và object đang nối cũng chưa link, link 2 obj
                        if (linkedObject == null)
                        {
                            Link(target);
                            return;
                        }
                        // Nếu obj hiện tại đã link, unlink và link lại
                        else
                        {
                            Unlink();
                            Link(target);
                            return;
                        }
                    }
                }

                // Sau khi duyệt xong, không tìm được object đích
                // Nếu đang link, unlink obj
                if (linkedObject != null && target == null)
                {
                    Unlink();
                    lineRenderer.enabled = false;
                }
                // Nếu không, +1 lần làm sai
                else
                {
                    manager.UpdateWrongCount(false);
                    lineRenderer.enabled = false;
                }
            }
        }

        public void Link(LinkableObject target)
        {
            target.linkedObject = this;
            linkedObject = target;
/*            LinkPairManager.RecordCorrectPair();
*/
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }

        void Unlink()
        {
            if (linkedObject != null)
            {
                linkedObject.linkedObject = null;
                linkedObject.lineRenderer.enabled = false;
            }
            linkedObject = null;
            lineRenderer.enabled = false;
        }
    }
}
