using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiet5_Inputs : MonoBehaviour
{
    public Text inputText;
    public SS_NumberPanel InputPanel;
    public bool canInteract = true;
    public enum InputType { ObjA, Plus, Minus, ObjB, Equal, ObjAns }
    public InputType inputSlot;
    [Header("Stats")]
    public bool tuKT = false;
    public bool hasFixedInput;
    public enum neededType { Int, String}
    public neededType inputType;
    public int neededInputInt;
    public string neededInputString;

    private int inputedInt;
    private string inputedString;
    private int wrongChoiceCount = 0;

    private void Awake()
    {
        if (tuKT)
        {
            GetComponentInParent<Tiet5_TuKT>().tuKT = tuKT;
            if (inputSlot == InputType.ObjA) { GetComponentInParent<Tiet5_TuKT>().InputA.Add(this); }
            else if (inputSlot == InputType.ObjB) { GetComponentInParent<Tiet5_TuKT>().InputB.Add(this); }
            else if (inputSlot == InputType.ObjAns) { GetComponentInParent<Tiet5_TuKT>().InputAns.Add(this); }
            else if (inputSlot == InputType.Plus || inputSlot == InputType.Minus) { GetComponentInParent<Tiet5_TuKT>().operatorType = inputSlot; }
        }
    }
    private void Start()
    {
        if (hasFixedInput)
        {
            if (inputType == neededType.String)
            {
                switch (inputSlot)
                {
                    case InputType.Equal:
                        neededInputString = "=";
                        break;
                    case InputType.Minus:
                        neededInputString = "-";
                        break;
                    case InputType.Plus:
                        neededInputString = "+";
                        break;
                }
                inputText.text = neededInputString;
            }
            else
            {
                inputText.text = neededInputInt.ToString();
            }
        }
    }
    private void OnMouseDown()
    {
        InputPanel.turnOff();
        if (GameManager.Instance.canPlay && !hasFixedInput && canInteract)
        {
            InputPanel.turnOn(new Vector3(440, 76, -10));
            InputPanel.areaGP = this;
        }
    }

    public void CheckAnswer(int num)
    {
        bool isRight = true;
        if (inputType == neededType.Int)
        {
            if(num != neededInputInt) { wrongChoiceCount++; isRight = false; }
            inputedInt = num;
        }
        else { wrongChoiceCount++; isRight = false; }

        if (GameManager.Instance.canShowReaction)
        {
            if (isRight) { TimeLineManage.INSTANCE.RightPlay(); }
            else { TimeLineManage.INSTANCE.WrongPlay(); }
        }
    }
    public void CheckAnswer(string str)
    {
        bool isRight = true;
        if (inputType == neededType.String)
        {
            if (str != neededInputString) { wrongChoiceCount++; isRight = false; }
            inputedString = str;
        }
        else { wrongChoiceCount++; isRight = false; }

        if (GameManager.Instance.canShowReaction)
        {
            if (isRight) { TimeLineManage.INSTANCE.RightPlay(); }
            else { TimeLineManage.INSTANCE.WrongPlay(); }
        }
    }

    public int getWrongChoiceCount() { return wrongChoiceCount; }
    public int getInputInt() { return inputedInt; }
    public string getInputedString() { return inputedString; }
}
