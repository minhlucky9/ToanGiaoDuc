using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_InputAreaLT1 : MonoBehaviour
{
    public Text inputText;
    public GameObject InputPanel;
    public enum State { Number, Operator}
    public State state;
    public int Side;
    [HideInInspector] public int leftNum;
    [HideInInspector] public int rightNum;
    [HideInInspector] public int AreaInputedNumber;
    public SS_RandomLT1.QuestionState operatorNeeded;
    public SS_RandomLT1.QuestionState AreaInputedOperator;
    SS_RandomLT1 dad;

    private void Start()
    {
        inputText.gameObject.SetActive(true);
        dad = GetComponentInParent<SS_RandomLT1>();
    }
    private void OnMouseDown()
    {
        if ((dad.canWriteNumber && state == State.Number) || state == State.Operator)
        {
            SS_NumberPanel panel = InputPanel.GetComponent<SS_NumberPanel>();
            switch (state)
            {
                case State.Number:
                    panel.turnOn(new Vector3(174.62f, -19f, 0));
                    panel.Side = Side;
                    break;
                case State.Operator:
                    panel.turnOn(new Vector3(-150, -150, 0));
                    break;
            }
            panel.area1 = this;
        }
    }

    public void CheckAnswer(int num, int side)
    {
        bool isRight = true;
        if (side == 0)   //left
        {
            if (num != leftNum) { GetComponentInParent<SS_RandomLT1>().wrongChoiceCount++; isRight = false; }
        }
        else if (side == 1)
        {
            if (num != rightNum) { GetComponentInParent<SS_RandomLT1>().wrongChoiceCount++; isRight = false; }
        }

        if (GameManager.Instance.canShowReaction)
        {
            if (isRight)
            {
                TimeLineManage.INSTANCE.RightPlay();
            }
            else
            {
                TimeLineManage.INSTANCE.WrongPlay();
            }
        }
        AreaInputedNumber = num;
    }

    public void CheckAnswer(SS_RandomLT1.QuestionState Operator)
    {
        bool isRight = true;
        if(operatorNeeded != Operator) { GetComponentInParent<SS_RandomLT1>().wrongChoiceCount++; isRight = false; }
        AreaInputedOperator = Operator;
        if (GameManager.Instance.canShowReaction)
        {
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
}
