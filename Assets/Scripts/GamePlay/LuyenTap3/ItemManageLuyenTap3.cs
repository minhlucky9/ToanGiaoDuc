using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManageLuyenTap3 : MonoBehaviour
{
    [SerializeField]
    private GameObject itemMoving;
    LuyenTap3LevelMap levelMap;
    private void Start()
    {
        levelMap = transform.parent.parent.GetComponent<LuyenTap3LevelMap>();
    }

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
