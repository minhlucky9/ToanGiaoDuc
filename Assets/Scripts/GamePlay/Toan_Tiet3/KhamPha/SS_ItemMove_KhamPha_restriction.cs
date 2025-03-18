using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_ItemMove_KhamPha_restriction : MonoBehaviour
{
    public Vector3 startPos;
    public SS_AreaReceiver_KhamPha lT5;

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
