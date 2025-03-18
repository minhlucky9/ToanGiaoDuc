using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpriteLayout : MonoBehaviour
{
    public int numOfItem;
    public float spacing, ItemWitdh;
    
    public void Run()
    {
        float WIDTH = numOfItem * ItemWitdh + (numOfItem - 1) * spacing;
        float firstPos = ItemWitdh / 2 - WIDTH / 2;

        for (int i = 0; i < numOfItem; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
            child.localPosition = new Vector3(firstPos + i * (ItemWitdh + spacing), child.localPosition.y, child.localPosition.z);
        }
    }

    public void Run(int[] arr)
    {
        float WIDTH = numOfItem * ItemWitdh + (numOfItem - 1) * spacing;
        float firstPos = ItemWitdh / 2 - WIDTH / 2;

        for (int i = 0; i < numOfItem; i++)
        {
            Transform child = transform.GetChild(arr[i]);
            child.gameObject.SetActive(true);
            child.localPosition = new Vector3(firstPos + i * (ItemWitdh + spacing), child.localPosition.y, child.localPosition.z);
        }
    }

    public void inputValue(List<int> val)
    {
        for (int i = 0; i < numOfItem; i++)
        {
            Transform child = transform.GetChild(i);
            child.GetComponent<SS_AreaReceiverLT5>().Area_NeededValue = val[i];
        }
    }
}
