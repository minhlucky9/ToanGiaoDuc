using LearningGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionLevel { SUBJECT, TOPIC, GAME_MODE };

public class SelectLessonUIController : MonoBehaviour
{
    public static SelectLessonUIController instance;
    [Header("Scroll View Settings")]
    public RectTransform scrollView;
    public int minScrollViewWidth = 500;
    public int maxScrollViewWidth = 1725;

    [Header("Selection Button Settings")]
    public RectTransform btnContainer;
    public SelectionBtnController btnPrefab;
    LayoutGroup containerLayout;

    SelectionLevel level = SelectionLevel.SUBJECT;
    int totalLevel = Enum.GetNames(typeof(SelectionLevel)).Length;
    string selectedSubjectId;
    string selectedTopicId;

    [Header("Game Selection Button Settings")]
    public GameObject gameSelectionGroup;
    public SelectGameUIController selectGameUIController;

    [Header("Other buttons")]
    public Button backBtn;
    public Button resultBtn;
    public Button rateBtn;

    private void Awake()
    {
        instance = this;
        containerLayout = btnContainer.GetComponent<LayoutGroup>();
    }

    private void Start()
    {
        level = SelectionLevel.SUBJECT;
        selectedSubjectId = "";
        selectedTopicId = "";
        InitButtonSelection();
        InitOtherButtonsEvent();
    }

    // Update is called once per frame
    void Update()
    {
        float containerWidth = btnContainer.sizeDelta.x - containerLayout.padding.left - containerLayout.padding.right;
        scrollView.sizeDelta = new Vector2( Mathf.Clamp(containerWidth, minScrollViewWidth, maxScrollViewWidth) , scrollView.sizeDelta.y);
    }



    #region Other Buttons Event Handle

    public void HideAllButtons()
    {
        backBtn.gameObject.SetActive(false);
        resultBtn.gameObject.SetActive(false);
        rateBtn.gameObject.SetActive(false);

    }

    public void ShowAllButtons()
    {
        backBtn.gameObject.SetActive(true);
        resultBtn.gameObject.SetActive(true);
        rateBtn.gameObject.SetActive(true);
    }

    public void InitOtherButtonsEvent()
    {
        backBtn.onClick.AddListener(delegate
        {
            HandleBackBtn();
        });
    }

    public void HandleBackBtn()
    {
        if(level > 0) level--;
        InitButtonSelection();
    }

    #endregion

    #region Button Selection Handle

    public void InitButtonSelection()
    {
        gameSelectionGroup.SetActive(false);
        ClearBtnContainer();
        HideAllButtons();

        switch (level)
        {
            case SelectionLevel.SUBJECT:
                InitButtonSelectionSubject();
                break;
            case SelectionLevel.TOPIC:
                InitButtonSelectionTopic();
                break;
            case SelectionLevel.GAME_MODE:
                InitButtonSelectionGameMode();
                break;
        }
    }

    public void InitButtonSelectionSubject()
    {
        //init btn
        SubjectSO[] allSubjects = LearningGameManager.instance.GetListSubjects();
        for (int i = 0; i < allSubjects.Length; i++)
        {
            SubjectSO currentSubject = allSubjects[i];
            SelectionBtnController btn = Instantiate(btnPrefab, btnContainer);
            btn.Init(currentSubject.subjectImage, currentSubject.subjectName, delegate {
                SelectionBtnEvent(ref selectedSubjectId, currentSubject.subjectId);
            });
        }
    }

    public void InitButtonSelectionTopic()
    {
        backBtn.gameObject.SetActive(true);
        //init btn
        TopicSO[] allTopics = LearningGameManager.instance.GetListTopicsFromSubjectId(selectedSubjectId);
        for (int i = 0; i < allTopics.Length; i++)
        {
            TopicSO currentTopic = allTopics[i];
            SelectionBtnController btn = Instantiate(btnPrefab, btnContainer);
            btn.Init(currentTopic.topicImage, currentTopic.topicName, delegate
            {
                SelectionBtnEvent(ref selectedTopicId, currentTopic.topicId);
                LearningGameManager.instance.currentTopicName = currentTopic.topicName;
            });
        }
    }

    public void InitButtonSelectionGameMode()
    {
        ShowAllButtons();
        gameSelectionGroup.SetActive(true);
        selectGameUIController.Init(selectedTopicId);
    }

    public void SelectionBtnEvent(ref string selectedId, string objectId)
    {
        selectedId = objectId;
        //
        if ((int)level < totalLevel - 1)
        {
            level++;
        }
        InitButtonSelection();
    }

    public void ClearBtnContainer()
    {
        for(int i = 0; i < btnContainer.childCount; i ++)
        {
            Destroy(btnContainer.GetChild(i).gameObject);
        }
    }

    #endregion
}
