using LearningGame;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectGameUIController : MonoBehaviour
{
    public SelectGameBtnController buttonPrefab;

    [Header("Explore Selection UI Settings")]
    public Button selectExploreBtn;
    public LearningGameSO exploreGameSO;

    [Header("Practice Selection UI Settings")]
    public Button selectPracticeBtn;
    public GameObject selectPracticePanel;
    public Transform practiceButtonContainer;
    List<LearningGameSO> practiceGames;

    [Header("Self Examination Selection UI Settings")]
    public Button selectExaminationBtn;
    public GameObject selectExaminationPanel;
    public List<Button> examinationLevelBtns;
    public Transform examinationButtonContainer;
    Dictionary<int, List<LearningGameSO>> examinationGames;

    // Start is called before the first frame update
    void Start()
    {
        selectExploreBtn.onClick.AddListener(delegate
        {
            if (exploreGameSO.gameSteps.Count > 0)
            {
                LearningGameManager.instance.LoadGame(exploreGameSO);
            }
        });

        selectPracticeBtn.onClick.AddListener(delegate
        {
            selectPracticePanel.SetActive(true);
        });

        selectExaminationBtn.onClick.AddListener(delegate
        {
            selectExaminationPanel.SetActive(true);
        });

        for(int i = 0; i < examinationLevelBtns.Count; i++)
        {
            int difficult = i + 1;
            examinationLevelBtns[i].onClick.AddListener(delegate
            {
                SetupExaminationButton(difficult);
            });
        }
    }

    

    public void Init(string topicId)
    {
        SetupGameData(topicId);
        InitSelectGameBtn(practiceGames, practiceButtonContainer, "Bài luyện tập");
    }

    public void SetupGameData(string topicId)
    {
        LearningGameSO[] games = LearningGameManager.instance.GetListGamesFromTopicId(topicId);
        //
        practiceGames = new List<LearningGameSO>();
        examinationGames = new Dictionary<int, List<LearningGameSO>>();
        exploreGameSO = new LearningGameSO();
        exploreGameSO.topicId = topicId;
        exploreGameSO.gameType = LearningGameType.KhamPha;

        for (int i = 0; i < games.Length; i++)
        {
            LearningGameSO currentGame = games[i];
            //create practice game data 
            if (currentGame.gameType == LearningGameType.LuyenTap)
            {
                practiceGames.Add(currentGame);
            }

            //create examination data
            if (currentGame.gameType == LearningGameType.TuKiemTra)
            {
                if (!examinationGames.ContainsKey(currentGame.difficulty))
                {
                    examinationGames.Add(currentGame.difficulty, new List<LearningGameSO>());
                }

                examinationGames[currentGame.difficulty].Add(currentGame);
            }

            //create explore data
            if(currentGame.gameType == LearningGameType.KhamPha)
            {
                exploreGameSO.gameSteps.AddRange(currentGame.gameSteps);
            }
        }
    }

    public void SetupExaminationButton(int difficulty)
    {
        if (!examinationGames.ContainsKey(difficulty)) return;
        ClearExaminationGameBtn();
        List<LearningGameSO> learningGames = examinationGames[difficulty];
        InitSelectGameBtn(learningGames, examinationButtonContainer, "Bài kiểm tra");
    }

    public void ClearExaminationGameBtn()
    {
        for (int i = 0; i < examinationButtonContainer.childCount; i++)
        {
            Destroy(examinationButtonContainer.GetChild(i).gameObject);
        }
    }

    public void InitSelectGameBtn(List<LearningGameSO> gameDatas, Transform contaner, string description)
    {
        for (int i = 0; i < gameDatas.Count; i ++)
        {
            LearningGameSO currentGame = gameDatas[i];
            SelectGameBtnController btn = Instantiate(buttonPrefab, contaner);
            btn.Init(description + " " + (i + 1), delegate
            {
                if(currentGame.gameSteps.Count > 0)
                {
                    LearningGameManager.instance.LoadGame(currentGame);
                }
            });
        }
    }
}
