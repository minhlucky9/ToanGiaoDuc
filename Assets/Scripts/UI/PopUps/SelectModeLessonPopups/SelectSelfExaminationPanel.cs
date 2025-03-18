using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectSelfExaminationPanel : MonoBehaviour
{
    public static SelectSelfExaminationPanel Instance;

    private GameObject panelChild;

    private SelfExamination_SelectLevelButton[] listSelfExantinationSelectLevelButton;
    private SelfExamination_SelectExcerciseButton[] listSelfExantinationSelectExcerciseButton;
    [SerializeField]
    private Transform ButtonsLevelDadTransform;

    [SerializeField]
    private Transform ButtonsExerciseDadTransform;

    [SerializeField]
    private GameObject SelectLevelPanObject, SelectExcercisePanObject;

    [SerializeField]
    private int selectedLevel = -1;
    [SerializeField]
    private int selectedLevel_child = -1;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private SelfExamination_SelectExcerciseButton SelectExcerciseButton;
    [SerializeField]
    private Transform ButtonsSelectExcerciseContainer;
    public Sprite LevelOpenSprite, LevelLockSprite;
    public List<SelfExamination_SelectExcerciseButton> listSelectExcerciseButton;
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
        InitButtons();
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
        SetPanelObjectActiveStatus(1);
    }
    public void ActiveSelectLevel()
    {
        UpdateStatus();
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        SetPanelObjectActiveStatus(2);
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
    public void ButtonBack_SelectExcercisePanClick()
    {
        SetPanelObjectActiveStatus(1);
    }
    private void SetPanelObjectActiveStatus(int mode)
    {
        switch (mode)
        {
            case 1:
                SelectLevelPanObject.SetActive(true);
                SelectExcercisePanObject.SetActive(false);
                break;
            default:
                SelectLevelPanObject.SetActive(false);
                SelectExcercisePanObject.SetActive(true);
                break;
        }
    }

    private void InitButtons()
    {
        listSelfExantinationSelectLevelButton = ButtonsLevelDadTransform.GetComponentsInChildren<SelfExamination_SelectLevelButton>();
        for (int i = 0; i < listSelfExantinationSelectLevelButton.Length; i++)
        {
            SelfExamination_SelectLevelButton buttonTemplate = listSelfExantinationSelectLevelButton[i];
            int levelSelect = listSelfExantinationSelectLevelButton[i].GetId();
            buttonTemplate.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonTemplate.GetComponent<Button>().onClick.AddListener(() => SelectLevel(levelSelect));
        }

    }
    public void UpdateButtonState()
    {
        if (listSelectExcerciseButton.Count == 0)
        {
            listSelfExantinationSelectLevelButton = ButtonsLevelDadTransform.GetComponentsInChildren<SelfExamination_SelectLevelButton>();
        }
        int t = LevelManager.INSTANCE.GetLevelNumberCount(ModeLessonEnum.SelfExamination, selectedLevel);
        if (listSelectExcerciseButton.Count < t)
        {
            int z = t - listSelectExcerciseButton.Count;
            for (int i = 0; i < z; i++)
            {
                SelfExamination_SelectExcerciseButton newButton = Instantiate(SelectExcerciseButton, ButtonsSelectExcerciseContainer);
                listSelectExcerciseButton.Add(newButton);
            }
        }
    }
    private void SelectLevel(int level)
    {
        if (level < 0) return;
        selectedLevel = level;
        if (levelText == null) return;
        levelText.text = "Mức " + level;
        GameManager.Instance.SetCurrentHardMode(level);
        SetupSelectExcerciseButton();
        SetPanelObjectActiveStatus(2);
    }
    private void SelectLevelChild(int level)
    {
        if (level < 0) return;
        selectedLevel_child = level;
        if (!LevelManager.INSTANCE.IsLevelOpen(ModeLessonEnum.SelfExamination, selectedLevel, selectedLevel_child)) return;
        GameManager.Instance.SetCurrentLevel(level);
        LevelManager.INSTANCE.LoadCurrentLevel();
        SelectModeLessonPanel.Instance.Deactive();
        Deactive();
        TimeLineManage.INSTANCE.StopCurrentDirector();
        UIManager.Instance.UIPlayGame();
    }
    private void SetupSelectExcerciseButton()
    {
        int t = LevelManager.INSTANCE.GetLevelNumberCount(ModeLessonEnum.SelfExamination, selectedLevel);
        if (listSelectExcerciseButton.Count < t)
        {
            int z = t - listSelectExcerciseButton.Count;
            for (int i = 0; i < z; i++)
            {
                SelfExamination_SelectExcerciseButton newButton = Instantiate(SelectExcerciseButton, ButtonsSelectExcerciseContainer);
                listSelectExcerciseButton.Add(newButton);
            }
        }
        for (int i = 0; i < listSelectExcerciseButton.Count; i++)
        {
            SelfExamination_SelectExcerciseButton button = listSelectExcerciseButton[i];
            int c = i;
            if (c < t)
            {
                button.gameObject.SetActive(true);
                button.SetId(c + 1);
                if (LevelManager.INSTANCE.IsLevelOpen(ModeLessonEnum.SelfExamination, selectedLevel, c + 1))
                {
                    button.SetImage(LevelOpenSprite);
                }
                else
                {
                    button.SetImage(LevelLockSprite);
                }
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.GetComponent<Button>().onClick.AddListener(() => SelectLevelChild(c + 1));
                button.UpdateText();
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }

    }
    public void UpdateStatus()
    {
        for (int i = 0; i < listSelectExcerciseButton.Count; i++)
        {
            SelfExamination_SelectExcerciseButton button = listSelectExcerciseButton[i];
            int c = i;
            if (LevelManager.INSTANCE.IsLevelOpen(ModeLessonEnum.SelfExamination, selectedLevel, c + 1))
            {
                button.SetImage(LevelOpenSprite);
            }
            else
            {
                button.SetImage(LevelLockSprite);
            }
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() => SelectLevelChild(c + 1));
            button.UpdateText();
        }
    }
    private void SelectExcercise(int excerciseId)
    {
        Debug.Log("Level : " + selectedLevel + "; " + "Select excercise: " + excerciseId);
    }
}
