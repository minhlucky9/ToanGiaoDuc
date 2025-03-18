using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_InputAreaLT6 : MonoBehaviour
{
    private int neededNum;
    public Text inputText;
    public GameObject InputPanel;
    [HideInInspector] public int AreaInputedNumber;
    SS_RandomLT6 dad;

    public void setNeededNum(int Amount)
    {
        neededNum = Amount;
    }
    public int getNeededNum()
    {
        return neededNum;
    }

    private void Start()
    {
        dad = GetComponentInParent<SS_RandomLT6>();
    }

    private void OnMouseDown()
    {
        if (dad.ableToWrite)
        {
            SS_NumberPanel panel = InputPanel.GetComponent<SS_NumberPanel>();
            panel.turnOn(new Vector3(412.2f, 61.1f, 0));
            panel.area6 = this;
        }
    }

    public void CheckAnswer(int num)
    {
        bool isRight = true;
        if (neededNum != num)
        {
            GetComponentInParent<SS_RandomLT6>().wrongChoiceCount++;
            isRight = false;
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
}
