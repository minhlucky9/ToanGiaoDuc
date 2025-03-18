using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemColorPainting : MonoBehaviour
{
    [SerializeField]
    private Sprite[] listObject;
    public int type = 0;
    [SerializeField]
    private GameObject[] allPos;
    public void SetSprite(int x)
    {
        for(int i = 0; i < allPos.Length; i++)
        {
            allPos[i].GetComponent<SpriteRenderer>().sprite = listObject[x];
        }
    }
}
