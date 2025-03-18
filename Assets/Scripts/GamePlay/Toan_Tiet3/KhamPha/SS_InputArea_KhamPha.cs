using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class SS_InputArea_KhamPha : MonoBehaviour
{
    public Text inputText;
    public GameObject InputPanel;

    public int neededNum = 4;
    public int AreaInputedNumber;
    public SS_InputAreaLT3.States neededState;
    public SS_InputAreaLT3.States InputedState;
    public bool IsInputNumber;

    SS_KhamPha khampha;
    private void Start()
    {
        khampha = GetComponentInParent<SS_KhamPha>();
    }
    private void OnMouseDown()
    {
        if (khampha.canInputNum)
        {
            if (IsInputNumber)
            {
                SS_NumberPanel panel = InputPanel.GetComponent<SS_NumberPanel>();
                panel.turnOn(new Vector3(174.62f, -19f, 0));
                panel.areaKP = this;
            }
            else
            {
                SS_InputPanel panel = InputPanel.GetComponent<SS_InputPanel>();
                panel.turnOn(new Vector3(transform.position.x, transform.position.y, 5f));
                panel.areaKP = this;
            }
        }
    }

    public void CheckAnswer(int _ans)
    {
        bool isRight = true;
        if(neededNum != _ans) { khampha.WrongChoiceCount++; isRight = false; }
        AreaInputedNumber = _ans;

        if (isRight)
        {
            TimeLineManage.INSTANCE.RightPlay();
        }
        else
        {
            TimeLineManage.INSTANCE.WrongPlay();
        }
    }

    public void CheckAnswer(SS_InputAreaLT3.States inputState)
    {
        bool isRight = true;
        if (inputState != neededState) { khampha.WrongChoiceCount++; isRight = false; }
        InputedState = inputState;

        if (isRight)
        {
            TimeLineManage.INSTANCE.RightPlay();
        }
        else
        {
            TimeLineManage.INSTANCE.WrongPlay();
        }
    }
}
