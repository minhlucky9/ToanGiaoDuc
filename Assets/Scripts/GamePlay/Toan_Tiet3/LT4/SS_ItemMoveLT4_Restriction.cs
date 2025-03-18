using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_ItemMoveLT4_Restriction : MonoBehaviour
{
    public Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void changeStartPos(Vector3 newPos)
    {
        startPos = newPos;
    }
}
