using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectModeLessonPanel : MonoBehaviour, IPanel
{
    public static SelectModeLessonPanel Instance;
    [SerializeField]
    private Transform ButtonsDadTransform;
    [SerializeField]
    private Text titleText;
    SelectModeLessonButton[] listSelectModeButtons;
    private GameObject panelChild;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        try
        {
            panelChild = transform.GetChild(0).gameObject;
        }
        catch { }

    }
    private void Start()
    {

        InitButtons();
    }
    public void Active()
    {
        titleText.text = GameManager.Instance.GetCurrentTietHoc().GetTenTietHoc();
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
        try
        {
            GetComponent<Animator>().Play("out");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void InitButtons()
    {
        listSelectModeButtons = ButtonsDadTransform.GetComponentsInChildren<SelectModeLessonButton>();
        for (int i = 0; i < listSelectModeButtons.Length; i++)
        {
            SelectModeLessonButton buttonTemplate = listSelectModeButtons[i];
            ModeLessonEnum mode = buttonTemplate.getMode();
            listSelectModeButtons[i].GetComponent<Button>().onClick.AddListener(() => SelectMode(mode));
        }
    }

    private void SelectMode(ModeLessonEnum modeSelected)
    {
        switch (modeSelected)
        {
            case ModeLessonEnum.Explore:
                GameManager.Instance.SetCurrentMode(ModeLessonEnum.Explore);
                TimeLineManage.INSTANCE.StopCurrentDirector();
                LevelManager.INSTANCE.LoadCurrentLevel();
                // test
                Deactive();
                UIManager.Instance.UIPlayGame();
                //
                break;
            case ModeLessonEnum.Practice:
                //Deactive();
                GameManager.Instance.SetCurrentMode(ModeLessonEnum.Practice);
                SelectPracticeExcercisePanel.Instance.Active();
                break;
            case ModeLessonEnum.SelfExamination:
                //Deactive();
                GameManager.Instance.SetCurrentMode(ModeLessonEnum.SelfExamination);
                SelectSelfExaminationPanel.Instance.Active();
                break;
            case ModeLessonEnum.Print:
                BookExportManage.Instance.currentBookExport.CreatePDF();
                break;
            default:
                break;
        }
        GameManager.Instance.SetCurrentMode(modeSelected);
    }
    public bool IsActive()
    {
        if (panelChild != null)
            return panelChild.activeSelf;
        return false;
    }
    public void ButtonBack()
    {
        Deactive();
        SelectLessonPanel.Instance.Active();
        UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Select_Lesson);
    }

    
}
