using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemLuyenTap1 : MonoBehaviour
{
    public List<Transform> listPosition;
    public List<ItemLuyenTap1> listItem;
    public int defaultTrueMin = 1, defaultTrueMax = 3;
    private void Start()
    {
        int randomCount = listPosition.Count;
        List<int> randomIndex = new List<int>();
        for (int i = 0; i < randomCount; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = Random.Range(0, listItem.Count);
            }
            while (randomIndex.Contains(randomNumber));
            randomIndex.Add(randomNumber);
        }
        if (randomIndex.Count != listPosition.Count)
        {
            Debug.Log("Loi random");
        }
        else
        {
            for (int i = 0; i < listPosition.Count; i++)
            {
                listItem[randomIndex[i]].transform.position = listPosition[i].position;
                listItem[randomIndex[i]].defaultPos = listPosition[i];
                listItem[randomIndex[i]].gameObject.SetActive(true);
            }
        }
        int rand = Random.Range(defaultTrueMin, defaultTrueMax);
        for (int i = 0; i < rand; i++)
        {
            listItem[randomIndex[i]].SetDefaultTrue();
        }
    }
}
