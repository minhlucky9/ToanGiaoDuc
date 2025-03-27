using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputCallBackPopupPanel : MonoBehaviour
{

    #region Singleton
    public static InputCallBackPopupPanel INSTANCE;
    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        inputField.onFocusSelectAll = true;
    }
    #endregion
    public TMP_InputField inputField;
    private GameObject panelChild;
    System.Action<int> callBackFunc;
    System.Action callBackFalseFunc;
    public int result = 0;
    public int limit = 1000;
    
    private void Start()
    {
        panelChild = transform.GetChild(0).gameObject;
    }
    public void Active(int initValue)
    {
        callBackFalseFunc = null;
        callBackFunc= null;
        if (inputField != null)
        {
            result = initValue;
            if (result == -1) result = 0;
            inputField.text = string.Empty + result;
            inputField.ActivateInputField();
        }
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Active(int initValue, System.Action<int> callBackFunction)
    {
        callBackFunc = callBackFunction;
        callBackFalseFunc = null;
        if (inputField != null)
        {
            result = initValue;
            if (result == -1) result = 0;
            inputField.text = string.Empty + result;
            inputField.ActivateInputField();
        }
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Active(int initValue, System.Action<int> callBackFunction, System.Action OnDeactiveAction)
    {
        callBackFunc = callBackFunction;
        callBackFalseFunc = OnDeactiveAction;
        if (inputField != null)
        {
            result = initValue;
            if (result == -1) result = 0;
            inputField.text = string.Empty + result;
            inputField.ActivateInputField();
        }
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Deactive()
    {
        if (callBackFalseFunc != null)
        {
            callBackFalseFunc.Invoke();
        }
        try
        {
            GetComponent<Animator>().Play("out");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public bool IsActive()
    {
        if (panelChild != null)
            return panelChild.activeSelf;
        return false;
    }
    public void ButtonSubmit()
    {
        callBackFunc?.Invoke(result);
        Deactive();
    }
    public void Init(int initNum)
    {
        result = initNum;
        inputField.text = result.ToString();
    }
    public void Delete()
    {
        result = 0;
        inputField.text = "0";
    }
    public void Num(int x)
    {
        int tmp = result * 10 + x;
        //tmp = tmp % 10;
        if (tmp < limit)
        {
            result = tmp;
            inputField.text = result.ToString();
        }
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
