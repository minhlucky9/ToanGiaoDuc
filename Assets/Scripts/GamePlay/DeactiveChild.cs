using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveChild : MonoBehaviour
{
    public int deactiveFrom;
    public void DeactiveChildAll()
    {
        for(int i=deactiveFrom; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
