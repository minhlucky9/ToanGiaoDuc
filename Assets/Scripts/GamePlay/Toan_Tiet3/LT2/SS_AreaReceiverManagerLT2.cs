using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_AreaReceiverManagerLT2 : MonoBehaviour
{
    [SerializeField] List<SS_AreaReceiverLT2> listArea;

    public List<SS_AreaReceiverLT2> GetList()
    {
        return listArea;
    }
    public void Check(GameObject itemCheck)
    {
        SS_ItemMoveLT2 item = itemCheck.GetComponent<SS_ItemMoveLT2>();
        for (int i = 0; i < listArea.Count; i++)
        {
            listArea[i].CheckPositionOfItem(item);
        }
    }
}
