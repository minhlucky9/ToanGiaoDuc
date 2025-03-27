using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Camera.main != null)
        {
            Destroy(gameObject);
        }
    }
}
