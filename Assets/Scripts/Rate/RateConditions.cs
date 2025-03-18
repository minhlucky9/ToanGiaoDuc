using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RateConditions 
{
    public int id;
    public string tenTieuChi;
    public bool checkKhamPha;
    public bool checkLuyenTap;
    public bool checkTuKiemTra;
    public bool checkDangBai;
    public InputSource inputData;
    public string levelType = "";
    public RateCondition[] listCondition;

    public string getRate(float ratio)
    {
        for(int i=0; i<listCondition.Length; i++)
        {
            if (listCondition[i].checkConditionOk(ratio))
            {
                return listCondition[i].outPut;
            }
        }
        return "Khong co danh gia xxx !";
    }
}
public enum InputSource
{
    Time, RightPerCent, WrongPercent, RightCount, WrongCount, WrongSelectCount
}
