using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_ItemMoveLT5 : MonoBehaviour
{
    private BoxCollider thisBoxCollider;
    private Transform thisTransform;
    [SerializeField]
    private LayerMask AreaMask;
    [SerializeField]
    private SoSanh_LT5_lvlMap levelMap;
    Camera mainCam;
    private SS_AreaReceiverLT5 areaDad;

    public int itemValue;

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
        levelMap = transform.GetComponentInParent<SoSanh_LT5_lvlMap>();
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
        Debug.Log("Check request sent");
        levelMap.itemManager.ReleaseItemMoving();
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCam.transform.position.z;
        Ray ray = mainCam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, AreaMask))
        {
            SS_AreaReceiverLT5 area = hit.transform.GetComponent<SS_AreaReceiverLT5>();
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
            GetComponent<SS_ItemMoveLT5_Restriction>().lT5 = null;
        }
    }
    public void SetAreaDad(SS_AreaReceiverLT5 area)
    {
        areaDad = area;
    }

    public SS_AreaReceiverLT5 GetAreaDad()
    {
        return areaDad;
    }
    public void SetItemMoving()
    {
        levelMap.itemManager.SetCurrentMovingItem(gameObject);
    }
}
