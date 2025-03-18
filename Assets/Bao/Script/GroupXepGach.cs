using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupXepGach : MonoBehaviour
{
    public int slotCount, itemcount, rightCount, wrongCount, wrongChoiceCount;
    public GameObject allSlot, allItems;
    public void Caculate()
    {
        slotCount = 0;
        for(int i = 0; i < allSlot.transform.childCount; i++)
        {
            if (allSlot.transform.GetChild(i).gameObject.activeSelf)
            {
                slotCount++;
            }
        }
        //slotCount = allSlot.transform.childCount;
        itemcount = allItems.transform.childCount;
        wrongCount = 0;
        rightCount = 0;
        wrongChoiceCount = 0;
        for(int i = 0; i < itemcount; i++)
        {
            ToSlotItem tsi = allItems.transform.GetChild(i).GetComponent<ToSlotItem>();
            wrongChoiceCount += tsi.wrongChoiceCount;
            if (tsi.isRight && tsi.haveSlot) 
            {
                rightCount++;
            }
        }
        wrongCount = slotCount - rightCount;
        if (wrongCount < 0) wrongCount = 0;
    }
}
