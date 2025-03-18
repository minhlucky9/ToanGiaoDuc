using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManage : MonoBehaviour
{
    [SerializeField]
    private GameObject itemMoving;
    [SerializeField]
    AreaManage areaManager;
    private void Start()
    {
        areaManager = transform.parent.GetChild(1).GetComponent<AreaManage>();
    }

    public GameObject GetCurrentMovingItem()
    {
        return itemMoving;
    }
    public void SetCurrentMovingItem(GameObject item)
    {
        itemMoving = item;
    }
    public void SendCheckRequest()
    {
        SetCurrentMovingItem(itemMoving);
        areaManager.Check(itemMoving);
        itemMoving = null;
    }
}
