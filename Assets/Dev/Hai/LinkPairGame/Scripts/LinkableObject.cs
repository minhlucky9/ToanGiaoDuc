using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private LinkPairManager linkPairManager;

        // Giới hạn vùng kéo
        [SerializeField] private BoxCollider boardBoundary;

        // Giới hạn tương tác với object
        [SerializeField] private bool isInteractable = true;
        // Lưu scale gốc để phóng to object
        private Vector3 originalScale;

        void Start()
        {
            mainCamera = Camera.main;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.enabled = false;

            originalScale = transform.localScale;

            GameObject Board = GameObject.FindGameObjectWithTag("BoardCanvas");
            boardBoundary = Board.GetComponent<BoxCollider>();

            linkPairManager = GameObject.FindGameObjectWithTag("LinkPairManager").GetComponent<LinkPairManager>();
        }

        void Update()
        {


            if (Input.GetMouseButtonDown(0) && isInteractable)
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
                        transform.localScale *= 1.2f;
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

            if (Input.GetMouseButtonUp(0) && isInteractable)
            {

               transform.localScale = originalScale;

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
                    else if (type != linkPairManager.validType1 && type != linkPairManager.validType2)
                    {
                        linkPairManager.UpdateWrongCount(false);
                        Link(target);
                        return;
                    }
                    // Nếu object tìm được cùng loại, hoặc nếu target không trong loại đang xét, tính là nối hụt
                    else if (target.type == type || (target.type != linkPairManager.validType1 && target.type != linkPairManager.validType2))
                    {
                        lineRenderer.enabled = false;
                        if (linkedObject)
                        {
                            Unlink();
                        }

                        linkPairManager.UpdateWrongCount(false);
                        return;
                    }
                    // Nếu object tìm được đã link, báo, +1 lần làm sai
                    else if (target.linkedObject != null && target != this)
                    {
                        lineRenderer.enabled = false;
                        if (linkedObject)
                        {
                            Unlink();
                        }

                        linkPairManager.UpdateWrongCount(false);
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
                    linkPairManager.UpdateWrongCount(false);
                    lineRenderer.enabled = false;
                }
            }
        }

        public void Link(LinkableObject target)
        {
            target.linkedObject = this;
            linkedObject = target;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }

        public void Unlink()
        {
            if (linkedObject != null)
            {
                linkedObject.linkedObject = null;
                linkedObject.lineRenderer.enabled = false;
            }
            linkedObject = null;
            lineRenderer.enabled = false;
        }

        public void ChangeInteractability(bool state)
        {
            isInteractable = state;
        }

        public void ChangeOpacity(float value)
        {
            Image image = gameObject.GetComponent<Image>();
            Color targetColor = image.color;
            targetColor.a = value;

            image.color = targetColor;
        }
    }
}
