using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemLuyenTap5 : MonoBehaviour
{
    public List<ItemLuyenTap1> listItem;
    public List<int> listValueRequire;
    [SerializeField]
    private int numberOfItem;
    private void Start()
    {
        for(int i=0; i<listValueRequire.Count; i++)
        {
            listItem[i].SetRequiredValue(listValueRequire[i]);
            listItem[i].SetDefaultTrue();
            listItem[i].canEdit = false;
        }
        
        int minIndex = Mathf.RoundToInt(listItem.Count / 2-1);
        int randStartPos = Random.Range(minIndex, listItem.Count-numberOfItem);
        int lastPos = randStartPos + numberOfItem;
        if(lastPos> listItem.Count)
        {
            Debug.LogError("Luyện tập 5 random không nổi!");
            return;
        }
        for(int i=randStartPos; i< lastPos; i++)
        {
            listItem[i].SetDefaultFalse();
            listItem[i].canEdit = true;
            listItem[i].isLV5Check = true;
        }
    }
}
