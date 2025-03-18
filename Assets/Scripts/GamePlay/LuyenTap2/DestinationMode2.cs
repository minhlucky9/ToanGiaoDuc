using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestinationMode2 : MonoBehaviour
{
    [SerializeField]
    private int id;

    [SerializeField]
    private Transform pointToDrawR;
    [SerializeField]
    private Transform pointToDrawL;

    private void Start()
    {
        if(pointToDrawR == null && transform.childCount >0)
        {
            pointToDrawR = transform.GetChild(0);
        }
        if (pointToDrawL == null && transform.childCount > 1)
        {
            pointToDrawL = transform.GetChild(1);
        }
        try
        {
            transform.GetChild(2).GetComponent<TextMeshPro>().text = string.Empty + id;
        }
        catch { }
    }
    
    public Vector3 GetPointDrawPositionRight()
    {
        if(pointToDrawR!=null)
            return pointToDrawR.position;
        return transform.position;
    }
    public Vector3 GetPointDrawPositionLeft()
    {
        if (pointToDrawL != null)
            return pointToDrawL.position;
        return transform.position;
    }
    public int GetID()
    {
        return id;
    }
}
