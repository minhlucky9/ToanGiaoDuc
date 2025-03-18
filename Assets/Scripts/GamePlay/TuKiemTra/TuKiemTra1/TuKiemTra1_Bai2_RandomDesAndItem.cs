using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuKiemTra1_Bai2_RandomDesAndItem : MonoBehaviour
{
    public ItemMoveMode2[] listItem;
    public Transform[] listPosition;
    //public DestinationMode2[] listDes;
    public int itemCountRequire;
    //public int destinationCountRequire;

    private void Start()
    {
        if(itemCountRequire >= listPosition.Length)
        {
            return;
        }
        for(int i=0; i<listItem.Length; i++)
        {
            listItem[i].gameObject.SetActive(false);
        }
        List<int> listItemVal = new List<int>();
        
        for(int i=0; i<itemCountRequire; i++)
        {
            int rand = -1;
            do
            {
                rand = Random.Range(0, listItem.Length);
            }
            while (listItemVal.Contains(rand));
            listItemVal.Add(rand);
        }
        for(int i=0; i<listItemVal.Count; i++)
        {
            ItemMoveMode2 item = listItem[listItemVal[i]];
            item.transform.position = listPosition[i].position;
            item.gameObject.SetActive(true);
        }
    }
}
