using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePane : MonoBehaviour, IPanel
{
    public static EndGamePane Instance;
    private GameObject panelChild;
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Text RightCountText;
    [SerializeField]
    private Text WrongCountText;
    [SerializeField]
    private Text WrongChoiceText;
    [SerializeField]
    private Text TimeCountText;
    System.Action actionCallBack;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        try
        {
            panelChild = transform.GetChild(0).gameObject;
        }
        catch
        {
            Debug.LogError(gameObject.name);
        }
    }
    public void Active()
    {
        actionCallBack = null;
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        UpdateInformation();
    }
    public void Active(System.Action action)
    {
        actionCallBack = action;
        Debug.Log("Update callback End Panel");
        Debug.Log( null != actionCallBack);
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        UpdateInformation();
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
    public void UpdateInformation()
    {
        TitleText.text = GameManager.Instance.GetCurrentTietHoc().GetTenTietHoc();
        Achivement lastAchievement = GameManager.Instance.GetLastAchievement();
        RightCountText.text = string.Empty + lastAchievement.GetRightAnswerCount();
        WrongCountText.text = string.Empty + lastAchievement.GetWrongAnswerCount();
        WrongChoiceText.text = string.Empty + lastAchievement.GetWrongSelectCount();
        TimeCountText.text = UltilityCustom.FloatToStringTime(lastAchievement.GetTimeCount());
    }
    public void ButtonOK()
    {
        if (actionCallBack != null)
        {
            Debug.Log("Call the callback func");
            actionCallBack.Invoke();
        }
        else
        {
            Debug.Log("The callback func is null");
        }
        switch (GameManager.Instance.GetCurrentMode())
        {
            case ModeLessonEnum.Explore:
                Deactive();
                try
                {
                    
                }
                catch
                {
                    SelectModeLessonPanel.Instance.Active();
                    Deactive();
                    UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Select_Mode);
                    UIManager.Instance.OnHomeUI();
                }
                return;
            case ModeLessonEnum.Practice:
                LevelManager.INSTANCE.DestroyCurrentLevel();
                SelectModeLessonPanel.Instance.Active();
                SelectPracticeExcercisePanel.Instance.Active();
                Deactive();
                UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Select_Mode);
                UIManager.Instance.OnHomeUI();
                return;
            case ModeLessonEnum.SelfExamination:
                LevelManager.INSTANCE.DestroyCurrentLevel();
                SelectModeLessonPanel.Instance.Active();
                SelectSelfExaminationPanel.Instance.ActiveSelectLevel();
                Deactive();
                UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Select_Mode);
                UIManager.Instance.OnHomeUI();
                ShowProgressNofication();
                break;
            default:
                break;
        }
    }
    private void ShowProgressNofication()
    {
        //string message = LevelManager.INSTANCE.GetProgressSelfExamination();
        //MessageCallBackPopupPanel.INSTACNE.Active(message);
    }
}
