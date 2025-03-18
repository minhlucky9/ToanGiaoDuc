using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion
    [SerializeField]
    private GameObject TopPartObject;
    [SerializeField]
    private GameObject DialogHome;
    [SerializeField]
    private GameObject DialogGame;
    [SerializeField]
    private GameObject HomeCharacters;
    [SerializeField]
    private GameObject PlayCharacters;
    [SerializeField]
    private Cinemachine.CinemachineBrain brainCam;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamDefaultHome;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamDefaultPlay;
    public EnabledPanel currentPanel;
    [SerializeField]
    GameObject UIObject;
    private void Start()
    {
        HomePanel.Instance.Active();
        currentPanel = EnabledPanel.Home;
        HomeCharacters.SetActive(true);
        PlayCharacters.SetActive(false);
        DialogHome.SetActive(true);
        DialogGame.SetActive(false);
        virtualCamDefaultPlay.Priority = 14;
        EnableTopPart();
    }
    public void EnableTopPart()
    {
        TopPartObject.SetActive(true);
    }
    public void DisableTopPart()
    {
        TopPartObject.SetActive(false);
    }
    public void SetCurrentPanel(EnabledPanel curr)
    {
        currentPanel = curr;
        switch (curr)
        {
            case EnabledPanel.End:
            case EnabledPanel.Playing:
                ToppartManager.INSTANCE.UpdateImageButtonClose();
                DialogHome.SetActive(false);
                DialogGame.SetActive(true);
                break;
            default:
                ToppartManager.INSTANCE.UpdateImageButtonClose();
                DialogHome.SetActive(true);
                DialogGame.SetActive(false);
                break;

        }
    }
    public void UIPlayGame()
    {
        SetCurrentPanel(EnabledPanel.Playing);
        virtualCamDefaultPlay.Priority = 16;
        HomeCharacters.SetActive(false);
        PlayCharacters.SetActive(true);
        GameUIPanel.Instance.Active();
        ToppartManager.INSTANCE.UpdateImageButtonClose();
    }
    public void OnHomeUI()
    {
        virtualCamDefaultPlay.Priority = 14;
        HomeCharacters.SetActive(true);
        PlayCharacters.SetActive(false);
    }
    public enum EnabledPanel {
        Login, Home, Select_Chapter, Select_Lesson, Select_Mode, Playing, End
    }
    public void BackFromExplore()
    {
        SelectModeLessonPanel.Instance.Active();
        SetCurrentPanel(EnabledPanel.Select_Mode);
        OnHomeUI();
    }
    public void OnLevelPrepare()
    {
        GameUIPanel.Instance.HideFinishButton();
        DisableTopPart();
    }
    public void OnLevelPrepareComplete()
    {
        GameUIPanel.Instance.Active();
        GameUIPanel.Instance.ShowFinishButton();
        EnableTopPart();
    }
    public void ButtonRate()
    {
        int check = CheckCanRate.Check();
        switch (check)
        {
            case -3:
                MessageCallBackPopupPanel.INSTACNE.Active("Bạn cần phải hoàn thành một mức độ ở phần tự kiểm tra để hiện phần đánh giá!");
                break;
            case -2:
                MessageCallBackPopupPanel.INSTACNE.Active("Bạn cần phải hoàn thành các bài luyện tập để hiện phần đánh giá!");
                break;
            case -1:
                MessageCallBackPopupPanel.INSTACNE.Active("Bạn cần phải hoàn thành bài khám phá để hiện phần đánh giá!");
                break;
            case 1:
                RatePanel.Instance.Active();
                break;
            default:
                MessageCallBackPopupPanel.INSTACNE.Active("Chưa hiện được phần đánh giá!");
                break;



        }
    }
    public void HideUI()
    {
        UIObject?.SetActive(false);
    }
    public void ShowUI()
    {
        UIObject?.SetActive(true);
    }
}
