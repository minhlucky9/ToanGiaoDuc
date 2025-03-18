using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_InputOperatorLT7 : MonoBehaviour
{
    public SS_RandomLT7.oper neededOper;
    public SS_RandomLT7.oper curOper;
    public SS_InputPanel operPanel;
    public Text inputText;

    private void OnMouseDown()
    {
        if (GetComponentInParent<SS_RandomLT7>().canAnswer)
        {
            operPanel.turnOn(new Vector3(transform.position.x, transform.position.y, transform.position.z - .2f));
            operPanel.areaLT7 = this;
        }
    }

    public void CheckAnswer(SS_RandomLT7.oper inputOper)
    {
        bool isRight = true;
        if(inputOper != neededOper)
        {
            GetComponentInParent<SS_RandomLT7>().WrongCount++;
            isRight = false;
        }
        curOper = inputOper;
        
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
