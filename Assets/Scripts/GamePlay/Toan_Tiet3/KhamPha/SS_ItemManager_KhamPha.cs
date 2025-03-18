using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_ItemManager_KhamPha : MonoBehaviour
{
    [SerializeField]
    private GameObject itemMoving;

    public GameObject GetCurrentMovingItem()
    {
        return itemMoving;
    }
    public void SetCurrentMovingItem(GameObject item)
    {
        itemMoving = item;
    }
    public void ReleaseItemMoving()
    {
        SetCurrentMovingItem(null);
    }
}
