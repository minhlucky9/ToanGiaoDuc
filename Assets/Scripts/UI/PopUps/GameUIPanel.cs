using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIPanel : MonoBehaviour
{
    public static GameUIPanel Instance;
    private GameObject panelChild;
    public Text timeCountText;
    public GameObject FinishButton;
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
        timeCountText.text = UltilityCustom.FloatToStringTime(0);
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
    public void HideFinishButton()
    {
        FinishButton.SetActive(false);
    }
    public void ShowFinishButton()
    {
        FinishButton.SetActive(true);
    }
    public void ButtonFinish()
    {
        GameManager.Instance.PauseGame();
        MessageCallBackPopupPanel.INSTACNE.Active("Bạn muốn kết thúc bài làm?", FinishLevel);
    }
    private void FinishLevel(bool check)
    {
        if (check)
        {
            switch (GameManager.Instance.GetCurrentMode())
            {
                case ModeLessonEnum.Explore:
                    GameManager.Instance.FinishGame();
                    LevelManager.INSTANCE.FinishCurrentLevel();
                    //EndGamePane.Instance.Active();
                    //UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.End);
                    break;
                case ModeLessonEnum.SelfExamination:
                    Deactive();
                    GameManager.Instance.FinishGame();
                    LevelManager.INSTANCE.PassLevel(ModeLessonEnum.SelfExamination, GameManager.Instance.GetCurrentHardMode() , GameManager.Instance.GetCurrentLevel());
                    LevelManager.INSTANCE.OpenLevel(ModeLessonEnum.SelfExamination, GameManager.Instance.GetCurrentHardMode(), GameManager.Instance.GetCurrentLevel() + 1);
                    LevelManager.INSTANCE.FinishCurrentLevel();
                    EndGamePane.Instance.Active();
                    UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.End);
                    break;
                default:

                    Deactive();
                    GameManager.Instance.FinishGame();
                    LevelManager.INSTANCE.FinishCurrentLevel();
                    LevelManager.INSTANCE.PassLevel(ModeLessonEnum.Practice, GameManager.Instance.GetCurrentLevel(), 0);
                    LevelManager.INSTANCE.OpenLevel(ModeLessonEnum.Practice,GameManager.Instance.GetCurrentLevel() + 1, 1);
                    EndGamePane.Instance.Active();
                    UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.End);
                    break;
            }
        }
        else
        {
            GameManager.Instance.ConitnueGame();
        }
    }
    private void Update()
    {
        if (GameManager.Instance.canPlay)
        {
            timeCountText.text = UltilityCustom.FloatToStringTime(GameManager.Instance.GetTimeCount());
        }
    }
    public void ResetTimeText()
    {
        timeCountText.text = "00\':00\"";
    }
}
