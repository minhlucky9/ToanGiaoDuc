using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainBoxcollider : MonoBehaviour
{
    private BoxCollider thisBoxCollider;
    private Transform thisTransform;
    [SerializeField]
    private int idAreaRequired;
    [SerializeField]
    private int falseTime = 0;
    public int idAreaSelected = 0;
    public int wrongSelectCount = 0;
    [SerializeField]
    AreaManage areaManager;
    [SerializeField]
    ItemManage itemManager;
    private Vector3 defaultScale, v3Tmp;
    private void Start()
    {
        if(thisBoxCollider == null)
        {
            thisBoxCollider = GetComponent<BoxCollider>();
        }
        if(thisTransform == null)
        {
            thisTransform = transform;
        }
        itemManager = thisTransform.parent.GetComponent<ItemManage>();
        areaManager = thisTransform.parent.parent.GetChild(1).GetComponent<AreaManage>();
    }
    public Vector3[] getBounds()
    {
        if (thisBoxCollider == null) return null;
        Vector2 size = Vector2.Scale( thisTransform.lossyScale, thisBoxCollider.size);
        Vector2 offset = Vector2.Scale(thisTransform.lossyScale, thisBoxCollider.center);
        Vector3 topLeft = thisTransform.position;
        Vector3 topRight = thisTransform.position;
        Vector3 botLeft = thisTransform.position;
        Vector3 botRight = thisTransform.position;
        topLeft.y = topLeft.y + (size.y / 2 + offset.y) * 0.5f;
        topRight.y = topRight.y + (size.y/2 + offset.y) * 0.5f;
        topLeft.x = topLeft.x - (size.x / 2 + offset.x) * 0.5f;
        topRight.x = topRight.x + (size.x / 2 + offset.x) * 0.5f;
        botLeft.y = botLeft.y - (size.y / 2 + offset.y) * 0.5f;
        botRight.y = botRight.y - (size.y / 2 + offset.y) * 0.5f;
        botLeft.x = botLeft.x - (size.x / 2 + offset.x) * 0.5f;
        botRight.x = botRight.x + (size.x / 2 + offset.x) * 0.5f;
        Vector3[] returnVal = { topLeft, topRight, botLeft, botRight};
        return returnVal;
    }
    public void RequestCheck()
    {
        areaManager?.Check(gameObject);
    }
    public bool IsRight()
    {
        return idAreaRequired == idAreaSelected;
    }
    public void SetAreaIdSelected(int areaId)
    {
        idAreaSelected = areaId;
        if (!IsRight())
        {
            wrongSelectCount++;
        }
    }
    public void SetItemMoving()
    {
        itemManager?.SetCurrentMovingItem(gameObject);
    }
    public int GetAreaId()
    {
        return idAreaRequired;
    }
    public void AddFalseTime()
    {
        falseTime++;
        if(falseTime > 2)
        {
            // Thuc hien nhac bai
        }
    }
    public void ResetAll()
    {
        falseTime = 0;
    }
    private void Update()
    {
        Vector3[] a = getBounds();
        Debug.DrawLine(a[0], a[1], Color.blue, 1f);
        Debug.DrawLine(a[2], a[1], Color.blue, 1f);
        Debug.DrawLine(a[3], a[1], Color.blue, 1f);
        Debug.DrawLine(a[2], a[3], Color.blue, 1f);
        if (falseTime >= 3)
        {

            
        }
    }
}
