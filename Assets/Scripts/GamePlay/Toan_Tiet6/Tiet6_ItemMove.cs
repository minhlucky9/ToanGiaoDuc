using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet6_ItemMove : MonoBehaviour
{
    private BoxCollider thisBoxCollider;
    private Transform thisTransform;
    [SerializeField]
    private LayerMask AreaMask;
    [SerializeField]
    private Tiet6_LT_lvlmap levelMap;
    Camera mainCam;
    private Tiet6_AreaReceiver areaDad;
    public int ItemCount;

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
        levelMap = transform.GetComponentInParent<Tiet6_LT_lvlmap>();
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
            Tiet6_AreaReceiver area = hit.transform.GetComponent<Tiet6_AreaReceiver>();
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
        }
    }
    public void SetAreaDad(Tiet6_AreaReceiver area)
    {
        areaDad = area;
    }
    public void SetItemMoving()
    {
        levelMap.itemManager.SetCurrentMovingItem(gameObject);
    }
}
