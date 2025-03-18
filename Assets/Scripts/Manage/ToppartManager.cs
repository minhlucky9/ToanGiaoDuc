using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppartManager : MonoBehaviour
{
    public static ToppartManager INSTANCE;
    public Sprite homeButtomSprite, closeButtonSprite;
    public Image ButtonCloseImage;
    private void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        UpdateImageButtonClose();
    }
    public void UpdateImageButtonClose()
    {
        switch (UIManager.Instance.currentPanel)
        {
            case UIManager.EnabledPanel.Playing:
                ButtonCloseImage.sprite = homeButtomSprite;
                ButtonCloseImage.SetNativeSize();
                break;
            default:
                ButtonCloseImage.sprite = closeButtonSprite;
                ButtonCloseImage.SetNativeSize();
                break;
        }
    }
    public void ButtonSetingClick()
    {
        switch (UIManager.Instance.currentPanel)
        {
            case UIManager.EnabledPanel.Login:
                if (SettingPanel.INSTANCE.IsActive())
                {
                    SettingPanel.INSTANCE.Deactive1();
                }
                else
                {
                    SettingPanel.INSTANCE.Active1();
                }
                break;
            case UIManager.EnabledPanel.Playing:

                break;
            default:
                if (SettingPanel.INSTANCE.IsActive())
                {
                    SettingPanel.INSTANCE.Deactive();
                }
                else
                {
                    SettingPanel.INSTANCE.Active();
                }
                break;
        }
    }
    public void ButtonCloseClick()
    {
        //MessageCallBackPopupPanel.INSTACNE.Active("Bạn muốn thoát trò chơi?", AskQuitGame);
        switch (UIManager.Instance.currentPanel)
        {
            case UIManager.EnabledPanel.Playing:
                GameManager.Instance.PauseGame();
                MessageCallBackPopupPanel.INSTACNE.Active("Bạn muốn về trang chủ?", AskBackHome);
                break;
            default:
                MessageCallBackPopupPanel.INSTACNE.Active("Bạn muốn đăng xuất?", AskLogOut);
                break;
        }
    }
    public void ButtonLogOutClick()
    {
        MessageCallBackPopupPanel.INSTACNE.Active("Bạn muốn đăng xuất?", AskLogOut);
    }

    public void deletePlayerPref()
    {
        PlayerPrefs.DeleteAll();
    }

    private void AskLogOut(bool check)
    {
        if (check)
        {
            SceneLoader.INSTANCE.LoadScene(0);
        }
        else
        {

        }
    }
    private void AskQuitGame(bool check)
    {
        if (check)
        {
            Debug.Log("Quit game");
        }
        else
        {
            Debug.Log("Not quit game");
        }
    }
    private void AskBackHome(bool check)
    {
        if (check)
        {
            switch (UIManager.Instance.currentPanel)
            {
                case UIManager.EnabledPanel.Playing:
                    GameManager.Instance.FinishGame();
                    LevelManager.INSTANCE.QuitLevel();
                    GameUIPanel.Instance.Deactive();
                    break;
                case UIManager.EnabledPanel.End:
                    GameManager.Instance.FinishGame();
                    EndGamePane.Instance.Deactive();
                    break;
            }
            LevelManager.INSTANCE.DestroyCurrentLevel();
            TimeLineManage.INSTANCE.StopCurrentDirector();
            HomePanel.Instance.Active();
            UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Home);
            UIManager.Instance.OnHomeUI();
        }
        else
        {
            switch (UIManager.Instance.currentPanel)
            {
                case UIManager.EnabledPanel.Playing:
                    GameManager.Instance.ConitnueGame();
                    break;
                case UIManager.EnabledPanel.End:
                    break;
            }
            Debug.Log("Not quit game");
        }
    }
}
