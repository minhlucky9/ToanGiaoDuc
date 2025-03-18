using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshEdit : MonoBehaviour
{
    public int order;
    private void Start()
    {
        GetComponent<MeshRenderer>().sortingOrder = order;
    }
}
