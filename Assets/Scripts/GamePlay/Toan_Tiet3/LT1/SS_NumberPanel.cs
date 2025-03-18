using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_NumberPanel : MonoBehaviour
{
    [HideInInspector] public SS_InputAreaLT1 area1;
    [HideInInspector] public SS_InputAreaLT6 area6;
    [HideInInspector] public SS_InputArea_KhamPha areaKP;
    [HideInInspector] public Tiet5_Inputs areaGP;
    public int Side;
    public void keyBoard(int num)
    {
        area1.inputText.text = num.ToString();
        area1.CheckAnswer(num, Side);
        turnOff();
    }

    public void KeyPad(int num)
    {
        area6.inputText.text = num.ToString();
        area6.CheckAnswer(num);
        turnOff();
    }

    public void NumPad(int num)
    {
        areaKP.inputText.text = num.ToString();
        areaKP.CheckAnswer(num);
        turnOff();
    }

    public void Number(int num)
    {
        areaGP.inputText.text = num.ToString();
        areaGP.CheckAnswer(num);
        turnOff();
    }
    public void String(string str)
    {
        areaGP.inputText.text = str;
        areaGP.CheckAnswer(str);
        turnOff();
    }
    public void smaller()
    {
        area1.inputText.text = "<";
        area1.CheckAnswer(SS_RandomLT1.QuestionState.Smaller);
        turnOff();
    }
    public void bigger()
    {
        area1.inputText.text = ">";
        area1.CheckAnswer(SS_RandomLT1.QuestionState.Bigger);
        turnOff();
    }
    public void equal()
    {
        area1.inputText.text = "=";
        area1.CheckAnswer(SS_RandomLT1.QuestionState.Equal);
        turnOff();
    }
    public void delete()
    {
        area1.inputText.text = null;
    }
    public void turnOff()
    {
        area1 = null;
        area6 = null;
        areaKP = null;
        gameObject.SetActive(false);
    }

    public void turnOn(Vector3 Position)
    {
        gameObject.SetActive(true);
        GetComponent<RectTransform>().localPosition = Position;
    }
    
}
