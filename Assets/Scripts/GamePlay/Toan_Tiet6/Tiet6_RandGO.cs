using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet6_RandGO : MonoBehaviour
{
    public bool TachSoHang;

    public Tiet6_AreaReceiver C_DVArea;
    public Tiet6_Input C_DV;
    public Tiet6_Input hangChuc;
    public Tiet6_Input hangDonVi;
    public Transform hangChuc_GO;
    public Transform hangDonVi_GO;

    private void Start()
    {
        int rand = Random.Range(9, 99);
        if (TachSoHang)
        {
            int chuc = rand / 10;
            int donvi = rand % 10;

            for (int j = 0; j < chuc; j++)
            {
                hangChuc_GO.GetChild(j).gameObject.SetActive(true);
            }
            for (int j = 0; j < donvi; j++)
            {
                hangDonVi_GO.GetChild(j).gameObject.SetActive(true);
            }

            hangChuc.neededInput = chuc.ToString();
            hangDonVi.neededInput = donvi.ToString();
        }
        else
        {
            C_DV.neededInput = rand.ToString();
            C_DV.showText();
            C_DVArea.setItemCountRequire(rand);
        }
    }
}
