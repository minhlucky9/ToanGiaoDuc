using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPhantichNumberwrite : MonoBehaviour
{
    public GameObject[] num;
    public GameObject[] numWrite;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void resetNumberWrite()
    {
        for(int i = 0; i < num.Length; i++)
        {
            num[i].SetActive(false);
        }

        for (int i = 0; i < numWrite.Length; i++)
        {
            numWrite[i].SetActive(true);
        }


    }
}
