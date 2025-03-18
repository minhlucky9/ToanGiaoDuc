using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveLuyenTap3 : MonoBehaviour
{
    private BoxCollider thisBoxCollider;
    private Transform thisTransform;
    private Vector3 defaultScale, v3Tmp;
    [SerializeField]
    private LayerMask AreaMask;
    [SerializeField]
    private LuyenTap3LevelMap levelMap;
    Camera mainCam;
    private MovableItem movableScript;
    private AreaLuyenTap3 areaDad;
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
        levelMap = transform.parent.parent.parent.parent.GetComponent<LuyenTap3LevelMap>();
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
        if(Physics.Raycast(ray, out hit, 100, AreaMask))
        {
            AreaLuyenTap3 area = hit.transform.GetComponent<AreaLuyenTap3>();
            if(areaDad!= null)
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
            if(areaDad != null)
            {
                areaDad.ReleaseItem(this);
                areaDad = null;
            }
        }
    }
    public void SetAreaDad(AreaLuyenTap3 area)
    {
        areaDad = area;
    }
    public void SetItemMoving()
    {
        levelMap.itemManager.SetCurrentMovingItem(this.gameObject);
    }
}
