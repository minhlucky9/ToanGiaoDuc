using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputList
{
    public List<GameObject> Bo;
    public List<GameObject> Que;
}

public class Tiet6_RandGO_Mul : MonoBehaviour
{
    public List<Tiet6_Input> C_DV;
    public List<InputList> C_DV_GO;

    private void Start()
    {
        for(int i = 0; i < C_DV.Count/2; i++)
        {
            int rand = Random.Range(9, 99);
            C_DV[i].neededInput = rand.ToString();
            C_DV[i + C_DV.Count / 2].neededInput = IntToString(rand);

            int chuc = rand / 10;
            int donvi = rand % 10;
            InputList list = C_DV_GO[i];
            for (int j = 0; j < chuc; j++)
            {
                list.Bo[j].SetActive(true);
            }
            for (int j = 0; j < donvi; j++)
            {
                list.Que[j].SetActive(true);
            }
        }
    }

    string IntToString(int num)
    {
        string str = "";

        if(num / 10 == 1) {
            str = " mười";
            if( num % 10 == 1){ str += " một"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " bốn"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 2)
        {
            str = " hai mươi";
            if(num%10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 3)
        {
            str = " ba mươi";
            if (num % 10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 4)
        {
            str = " bốn mươi";
            if (num % 10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 5)
        {
            str = " năm mươi";
            if (num % 10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 6)
        {
            str = " sáu mươi";
            if (num % 10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 7)
        {
            str = " bảy mươi";
            if (num % 10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 8)
        {
            str = " tám mươi";
            if (num % 10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        else if (num / 10 == 9)
        {
            str = " chín mươi";
            if (num % 10 == 1) { str += " mốt"; }
            else if (num % 10 == 2) { str += " hai"; }
            else if (num % 10 == 3) { str += " ba"; }
            else if (num % 10 == 4) { str += " tư"; }
            else if (num % 10 == 5) { str += " lăm"; }
            else if (num % 10 == 6) { str += " sáu"; }
            else if (num % 10 == 7) { str += " bảy"; }
            else if (num % 10 == 8) { str += " tám"; }
            else if (num % 10 == 9) { str += " chín"; }
        }
        return str;
    }
}
