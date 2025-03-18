using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManageLuyenTap3 : MonoBehaviour
{
    [SerializeField]
    private List<AreaLuyenTap3> listArea;

    public List<AreaLuyenTap3> GetList()
    {
        return listArea;
    }
    public void Check(GameObject itemCheck)
    {
        ItemMoveLuyenTap3 item = itemCheck.GetComponent<ItemMoveLuyenTap3>();
        for (int i = 0; i < listArea.Count; i++)
        {
            listArea[i].CheckPositionOfItem(item);
        }
    }
}
