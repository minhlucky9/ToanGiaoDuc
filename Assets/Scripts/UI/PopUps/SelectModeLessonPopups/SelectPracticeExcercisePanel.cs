using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPracticeExcercisePanel : MonoBehaviour, IPanel
{
    public static SelectPracticeExcercisePanel Instance;
    private GameObject panelChild;
    [SerializeField]
    private Transform ButtonsDad;
    [SerializeField]
    private SelectLevelButton[] listButtons;
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
        InitButton();
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

    public bool IsActive()
    {
        if (panelChild != null)
            return panelChild.activeSelf;
        return false;
    }
    public void ButtonBack()
    {
        Deactive();
    }
    bool isFirstInit = true;
    [SerializeField]
    SelectLevelButton selectLevelButtonPrefab;
    private void InitButton()
    {
        Debug.Log(LevelManager.INSTANCE);
        int t = LevelManager.INSTANCE.GetLevelNumberCount(ModeLessonEnum.Practice, 0);
     
        if (isFirstInit)
        {
            for (int i = 0; i < t; i++)
            {
                SelectLevelButton buttonz = Instantiate(selectLevelButtonPrefab, ButtonsDad);
            }
            isFirstInit = true;
        }
        if (t > listButtons.Length)
        {
            int z = listButtons.Length - t;
            for (int i = 0; i < z; i++)
            {
                SelectLevelButton buttonz = Instantiate(selectLevelButtonPrefab, ButtonsDad);
            }
        }
        listButtons = ButtonsDad.GetComponentsInChildren<SelectLevelButton>();
        for (int i = 0; i < listButtons.Length; i++)
        {
            if (i < t)
            {
                SelectLevelButton button = listButtons[i];
                int c = i + 1;
                button.SetId(c);
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.GetComponent<Button>().onClick.AddListener(() => SelectLevel(button.GetID()));
                if (LevelManager.INSTANCE.IsLevelOpen(ModeLessonEnum.Practice, button.GetID()))
                {

                    button.SetImage(null);
                    button.SetImage(LevelManager.INSTANCE.ButtonLevelActiveSprite);
                }
                else
                {
                    button.SetImage(null);
                    button.SetImage(LevelManager.INSTANCE.ButtonLevelDeactiveButton);
                }
            }
            else
            {
                listButtons[i].gameObject.SetActive(false);
            }
        }
    }
    //private void UpdateButtons()
    //{
    //    int t = LevelManager.INSTANCE.GetLevelNumberCount(ModeLessonEnum.Practice, 0);
    //    for (int i = 0; i < listButtons.Length; i++)
    //    {
    //        if (i < t)
    //        {
    //            SelectLevelButton button = listButtons[i];
    //            if (LevelManager.INSTANCE.IsLevelOpen(ModeLessonEnum.Practice, button.GetID()))
    //            {
    //                button.SetImage(null);
    //                button.SetImage(LevelManager.INSTANCE.ButtonLevelActiveSprite);
    //            }
    //            else
    //            {
    //                button.SetImage(null);
    //                button.SetImage(LevelManager.INSTANCE.ButtonLevelDeactiveButton);
    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //}
    private void SelectLevel(int level)
    {
        if (level < 0) return;
        if (!LevelManager.INSTANCE.IsLevelOpen(ModeLessonEnum.Practice, level))
        {
            return;
        }
        GameManager.Instance.SetCurrentLevel(level);
        LevelManager.INSTANCE.LoadCurrentLevel();
        SelectModeLessonPanel.Instance.Deactive();
        Deactive();
        TimeLineManage.INSTANCE.StopCurrentDirector();
        UIManager.Instance.UIPlayGame();
    }
}
