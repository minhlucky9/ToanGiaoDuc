using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemLuyenTap2 : MonoBehaviour
{
    public List<Transform> listPosition;
    public List<ItemMoveMode2> listItem;
    private void Start()
    {
        int randomCount = listPosition.Count;
        List<int> randomIndex = new List<int>();
        for(int i=0; i<randomCount; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = Random.Range((int)0, (int)listItem.Count);
            }
            while (randomIndex.Contains(randomNumber));
            randomIndex.Add(randomNumber);
        }
        if(randomIndex.Count != listPosition.Count)
        {
            Debug.Log("Loi random");
        }
        else
        {
            for(int i=0; i<listPosition.Count; i++)
            {
                listItem[randomIndex[i]].transform.position = listPosition[i].position;
                listItem[randomIndex[i]].gameObject.SetActive(true);
            }
        }
    }
}
