using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageCallBackPopupPanel : MonoBehaviour, IPanel
{
# region Singleton
    public static MessageCallBackPopupPanel INSTACNE;
    private void Awake()
    {
        if(INSTACNE == null)
        {
            INSTACNE = this;
        }
    }
    #endregion
    private GameObject panelChild;
    System.Action<bool> callBackFunc;
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private GameObject YNObject;
    [SerializeField]
    private GameObject MObject;
    public void Active()
    {
        SetYNOn(false);
        callBackFunc = null;
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        try
        {
            panelChild = transform.GetChild(0).gameObject;
        }
        catch { }
    }
    public void Active(string question)
    {
        SetYNOn(false);
        callBackFunc = null;
        if (question != null)
        {
            questionText.text = question;
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
    public void Active(string question, System.Action<bool> callBackFunction)
    {
        callBackFunc = callBackFunction;
        SetYNOn(true);
        if (question != null)
        {
            questionText.text = question;
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
    public void Active(string question, System.Action<bool> callBackFunction, bool isAlone)
    {
        callBackFunc = callBackFunction;
        SetYNOn(false);
        if (question != null)
        {
            questionText.text = question;
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
    private void SetYNOn(bool on)
    {
        if (on)
        {
            YNObject.SetActive(true);
            MObject.SetActive(false);
        }
        else
        {
            YNObject.SetActive(false);
            MObject.SetActive(true);
        }
    }
    public void Deactive()
    {
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
    public void ButtonYes()
    {
        Deactive();
        callBackFunc?.Invoke(true);
    }
    public void ButtonNo()
    {
        Deactive();
        callBackFunc?.Invoke(false);
    }
}
