using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_InputAreaLT3 : MonoBehaviour
{
    [HideInInspector] public Text inputText;
    public GameObject InputPanel;
    public enum States { Null, Smaller, Bigger, Equal}
    [HideInInspector] public States neededState;
    [HideInInspector] public States curState;

    private int m_wrongChoiceCount = 0;
    public int wrongChoiceCount { get { return m_wrongChoiceCount; } }

    private void Start()
    {
        SS_InputAreaManagerLT3 manager = GetComponentInParent<SS_InputAreaManagerLT3>();

        int index = manager.inputAreas.IndexOf(this);
        inputText = manager.InputTexts[index];
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.canPlay)
        {
            SS_InputPanel panel = InputPanel.GetComponent<SS_InputPanel>();
            panel.turnOn(new Vector3(transform.position.x, transform.position.y, 5.75f));
            panel.area = this;
        }
    }

    public void changeState(States state)
    {
        neededState = state;
    }

    public void checkAnswer(States state)
    {
        curState = state;
        bool isRight = true;
        if (curState != neededState)
        {
            m_wrongChoiceCount++;
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
    }
}
