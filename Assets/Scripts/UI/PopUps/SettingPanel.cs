using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour, IPanel
{
    public static SettingPanel INSTANCE;
    private bool activeStatus = false;
    private GameObject panelChild;
    public Sprite SoundOnSprite, SoundOffSprite;
    public Image SoundButtonImg;
    private void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
    }
    public void Active()
    {
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
    public void Active1()
    {
        try
        {
            GetComponent<Animator>().Play("in1");
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
    public void Deactive1()
    {
        try
        {
            GetComponent<Animator>().Play("out1");
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
    public void ButtonSoundClick()
    {
        if (SoundManager.instance.soundBool)
        {

        }
        else
        {

        }
    }
}
