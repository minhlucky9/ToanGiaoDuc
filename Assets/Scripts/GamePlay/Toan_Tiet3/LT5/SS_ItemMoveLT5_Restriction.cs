using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_ItemMoveLT5_Restriction : MonoBehaviour
{
    public Vector3 startPos;
    public SS_AreaReceiverLT5 lT5;

    private void Start()
    {
        startPos = transform.position;
    }

    public void OnMouseDown()
    {
        startPos = transform.position;
    }

    public void changeStartPos(Vector3 newPos)
    {
        startPos = newPos;
    }
}
