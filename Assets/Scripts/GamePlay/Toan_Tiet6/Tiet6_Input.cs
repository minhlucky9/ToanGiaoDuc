using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiet6_Input : MonoBehaviour
{
    [HideInInspector] public Text inputText;
    public Tiet6_inputPanel inputPanel;
    public Tiet6_inputPanel wordPanel;
    public bool canInteract = true;

    [Header("Stats")]
    public bool showFixedInput;
    public enum type { INT, STRING}
    public type curType;
    public string neededInput;

    private string inputedString;
    private int wrongChoiceCount = 0;

    private void Awake()
    {
        inputText = transform.GetChild(0).GetComponent<Text>();
    }
    private void Start()
    {
        
        if(showFixedInput) { inputText.text = neededInput; }
    }
    private void OnMouseDown()
    {
        inputPanel.turnOff();
        if (GameManager.Instance.canPlay && !showFixedInput && canInteract)
        {
            if(curType == type.INT)
            {
                inputPanel.turnOn(new Vector3(0, 0, 0));
                inputPanel.areaT6 = this;
            }
            else
            {
                wordPanel.turnOn(new Vector3(0, 0, 0));
                wordPanel.areaT6 = this;
            }
        }
    }
    public void CheckAnswer(string str)
    {
        bool isRight = true;
        if (str != neededInput) { wrongChoiceCount++; isRight = false; }
        inputedString = str;

        if (GameManager.Instance.canShowReaction)
        {
            if (isRight) { TimeLineManage.INSTANCE.RightPlay(); }
            else { TimeLineManage.INSTANCE.WrongPlay(); }
        }
    }

    public void showText() { inputText.text = neededInput; }
    public int getWrongChoiceCount() { return wrongChoiceCount; }
    public string getInput() { return inputedString; }
}
