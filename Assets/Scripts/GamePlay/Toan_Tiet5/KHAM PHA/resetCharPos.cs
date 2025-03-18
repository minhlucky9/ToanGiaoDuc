using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCharPos : MonoBehaviour
{
    public static resetCharPos INSTANCE;

    public Transform posGiaovien;
    public Transform posLan;
    public Transform posHai;

    public GameObject canvasGiaovien;
    public GameObject canvasLan;
    public GameObject canvasHai;


    Vector3 tempGiaovien;
    Vector3 tempLan;
    Vector3 tempHai;

    private void Awake()
    {
        INSTANCE = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        tempGiaovien = posGiaovien.rotation.eulerAngles;
        tempLan = posLan.rotation.eulerAngles;
        tempHai = posHai.rotation.eulerAngles;

    }



    public void resetPos()
    {
        
        posGiaovien.rotation = Quaternion.Euler(tempGiaovien);
        posLan.rotation = Quaternion.Euler(tempLan);
        posHai.rotation = Quaternion.Euler(tempHai);

        canvasGiaovien.SetActive(false);
        canvasLan.SetActive(false);
        canvasHai.SetActive(false);

    }
}
