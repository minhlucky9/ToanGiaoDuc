using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChoosePiece
{
    public class PieceInspector : MonoBehaviour
    {
        public float rotationSpeed = 300f;
        private bool isInspecting = false;


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Ưu tiên đối tượng khi click
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Inspectables")))
                {

                    if (hit.collider.gameObject == gameObject)
                    {
                        isInspecting = true;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!isInspecting) return;
                isInspecting = false;
            }

            if (isInspecting)
            {
                float rotX = -Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

                Quaternion deltaRotation = Quaternion.Euler(rotY, rotX, 0);
                transform.rotation = deltaRotation * transform.rotation;
            }
        }
    }
}

