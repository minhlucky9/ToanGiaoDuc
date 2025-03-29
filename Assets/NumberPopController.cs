using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberPopController : MonoBehaviour
{
    public TMP_InputField inputField;
    public int result = 0;
    public int limit = 1000;
    public void Init(int initNum)
    {
        result = initNum;
        inputField.text = result.ToString();
    }
    public void Delete()
    {
        result = 0;
        inputField.text="";
    }
    public void Num(int x)
    {
        result = result * 10 + x;
        inputField.text = result.ToString();
    }
    public void OnNumChange()
    {
        if (inputField.text.ToString() == "")
        {

        }
        else
        {
            int tmp = int.Parse(inputField.text.ToString());
            if (tmp < limit)
            {
                result = tmp;
            }
            inputField.text = result.ToString();
        }
        
    }
}
