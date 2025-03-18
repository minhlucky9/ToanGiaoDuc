using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_InputPanel : MonoBehaviour
{
    [HideInInspector] public SS_InputAreaLT3 area;
    [HideInInspector] public SS_InputArea_KhamPha areaKP;
    [HideInInspector] public SS_InputOperatorLT7 areaLT7;
    public Animator anim;

    public void smaller()
    {
        area.inputText.text = "<";
        area.checkAnswer(SS_InputAreaLT3.States.Smaller);
        turnOff();
    }
    public void bigger()
    {
        area.inputText.text = ">";
        area.checkAnswer(SS_InputAreaLT3.States.Bigger);
        turnOff();
    }
    public void equal()
    {
        area.inputText.text = "=";
        area.checkAnswer(SS_InputAreaLT3.States.Equal);
        turnOff();
    }

    public void smallerKP()
    {
        areaKP.inputText.text = "<";
        areaKP.CheckAnswer(SS_InputAreaLT3.States.Smaller);
        turnOff();
    }
    public void biggerKP()
    {
        areaKP.inputText.text = ">";
        areaKP.CheckAnswer(SS_InputAreaLT3.States.Bigger);
        turnOff();
    }
    public void equalKP()
    {
        areaKP.inputText.text = "=";
        areaKP.CheckAnswer(SS_InputAreaLT3.States.Equal);
        turnOff();
    }

    public void smallerLT7()
    {
        areaLT7.inputText.text = "<";
        areaLT7.CheckAnswer(SS_RandomLT7.oper.Smaller);
        turnOff();
    }
    public void biggerLT7()
    {
        areaLT7.inputText.text = ">";
        areaLT7.CheckAnswer(SS_RandomLT7.oper.Bigger);
        turnOff();
    }
    public void equalLT7()
    {
        areaLT7.inputText.text = "=";
        areaLT7.CheckAnswer(SS_RandomLT7.oper.Equal);
        turnOff();
    }

    public void turnOff()
    {
        area = null;
        gameObject.SetActive(false);
    }

    public void turnOn(Vector3 pos)
    {
        gameObject.SetActive(true);
        anim.SetTrigger("Start");
        transform.position = pos;
    }
}
