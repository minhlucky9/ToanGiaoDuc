using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_ItemMove_KhamPha : MonoBehaviour
{
    private BoxCollider thisBoxCollider;
    private Transform thisTransform;
    [SerializeField]
    private LayerMask AreaMask;
    [SerializeField]
    private SoSanh_KhamPha_lvlmap levelMap;
    Camera mainCam;
    private SS_AreaReceiver_KhamPha areaDad;
    public bool firstItemMove = true;
    
    private void Start()
    {
        if (thisBoxCollider == null)
        {
            thisBoxCollider = GetComponent<BoxCollider>();
        }
        if (thisTransform == null)
        {
            thisTransform = transform;
        }
        mainCam = Camera.main;
        levelMap = transform.GetComponentInParent<SoSanh_KhamPha_lvlmap>();
    }
    public Vector3[] getBounds()
    {
        if (thisBoxCollider == null) return null;
        Vector2 size = Vector2.Scale(thisTransform.lossyScale, thisBoxCollider.size);
        Vector2 offset = Vector2.Scale(thisTransform.lossyScale, thisBoxCollider.center);
        Vector3 topLeft = thisTransform.position;
        Vector3 topRight = thisTransform.position;
        Vector3 botLeft = thisTransform.position;
        Vector3 botRight = thisTransform.position;
        topLeft.y = topLeft.y + (size.y / 2 + offset.y) * 0.5f;
        topRight.y = topRight.y + (size.y / 2 + offset.y) * 0.5f;
        topLeft.x = topLeft.x - (size.x / 2 + offset.x) * 0.5f;
        topRight.x = topRight.x + (size.x / 2 + offset.x) * 0.5f;
        botLeft.y = botLeft.y - (size.y / 2 + offset.y) * 0.5f;
        botRight.y = botRight.y - (size.y / 2 + offset.y) * 0.5f;
        botLeft.x = botLeft.x - (size.x / 2 + offset.x) * 0.5f;
        botRight.x = botRight.x + (size.x / 2 + offset.x) * 0.5f;
        Vector3[] returnVal = { topLeft, topRight, botLeft, botRight };
        return returnVal;
    }
    public void RequestCheck()
    {
        if (GetComponentInParent<SS_KhamPha>().canMoveItem)
        {
            Debug.Log("Check request sent");
            if (firstItemMove) { levelMap.itemManager_1.ReleaseItemMoving(); }
            else { levelMap.itemManager_2.ReleaseItemMoving(); }
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCam.transform.position.z;
            Ray ray = mainCam.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, AreaMask))
            {
                SS_AreaReceiver_KhamPha area = hit.transform.GetComponent<SS_AreaReceiver_KhamPha>();
                if (areaDad != null)
                {
                    if (!areaDad.GetInstanceID().Equals(area.GetInstanceID()))
                    {
                        areaDad.ReleaseItem(this);
                    }
                }

                area.CheckPositionOfItem(this);
            }
            else
            {
                if (areaDad != null)
                {
                    areaDad.ReleaseItem(this);
                    areaDad = null;
                }
                GetComponent<SS_ItemMove_KhamPha_restriction>().lT5 = null;
            }
        }
    }
    public void SetAreaDad(SS_AreaReceiver_KhamPha area)
    {
        areaDad = area;
    }

    public SS_AreaReceiver_KhamPha GetAreaDad()
    {
        return areaDad;
    }
    public void SetItemMoving()
    {
        if (GetComponentInParent<SS_KhamPha>().canMoveItem)
        {
            if (firstItemMove) { levelMap.itemManager_1.SetCurrentMovingItem(gameObject); }
            else { levelMap.itemManager_2.SetCurrentMovingItem(gameObject); }
        }
    }
}
