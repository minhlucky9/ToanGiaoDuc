using LearningGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LearningGameType { KhamPha, LuyenTap, TuKiemTra };

public class LearningGameManager : MonoBehaviour
{
    public static LearningGameManager instance;
    public string currentTopicName = "";
    public LearningGameSO gameData;
    public string currentAdditiveSceneName = "";

    //subject list data
    SubjectSO[] allSubjects;
    Dictionary<string, SubjectSO> subjectDict;

    //topic list data
    TopicSO[] allTopics;
    Dictionary<string, TopicSO> topicDict;

    //game list data
    LearningGameSO[] allGames;
    Dictionary<string, LearningGameSO> gameDict;

    private void Awake()
    {
        instance = this;
        GetSubjectData();
        GetTopicData();
        GetGameData();
    }

    void Start()
    {
        LoadSceneAdditive("IntroScene");
        SelectLessonUIController.instance.gameObject.SetActive(true);
    }

    public void LoadGame(LearningGameSO gameSO)
    {
        gameData = gameSO;
        //LoadSceneAdditive(gameData.gameType.ToString());
        LoadSceneAdditive("LuyenTap");
        SelectLessonUIController.instance.gameObject.SetActive(false);
    }

    public void LoadSceneAdditive(string targetScene)
    {
        if(currentAdditiveSceneName != "")
        {
            SceneManager.UnloadSceneAsync(currentAdditiveSceneName);
        }

        SceneManager.LoadScene(targetScene, LoadSceneMode.Additive);
        currentAdditiveSceneName = targetScene;
    }

    public void GetSubjectData()
    {
        allSubjects = Resources.LoadAll<SubjectSO>("");
        subjectDict = new Dictionary<string, SubjectSO>();
        for (int i = 0; i < allSubjects.Length; i++)
        {
            subjectDict.Add(allSubjects[i].subjectId, allSubjects[i]);
        }
    }

    public SubjectSO[] GetListSubjects()
    {
        return allSubjects;
    }

    public void GetTopicData()
    {
        allTopics = Resources.LoadAll<TopicSO>("");
        topicDict = new Dictionary<string, TopicSO>();
        for (int i = 0; i < allTopics.Length; i++)
        {
            topicDict.Add(allTopics[i].referenceId, allTopics[i]);
        }
    }

    public TopicSO[] GetListTopicsFromSubjectId(string subjectId)
    {
        List<TopicSO> topics = new List<TopicSO>();
        for(int i = 0; i < allTopics.Length; i ++)
        {
            if(allTopics[i].subjectId == subjectId)
            {
                topics.Add(allTopics[i]);
            }
        }
        return topics.ToArray();
    }

    public void GetGameData()
    {
        allGames = Resources.LoadAll<LearningGameSO>("");
        gameDict = new Dictionary<string, LearningGameSO>();
        for (int i = 0; i < allGames.Length; i++)
        {
            gameDict.Add(allGames[i].referenceId, allGames[i]);
        }
    }

    public LearningGameSO[] GetListGamesFromTopicId(string topicId)
    {
        List<LearningGameSO> games = new List<LearningGameSO>();
        for (int i = 0; i < allGames.Length; i++)
        {
            if (allGames[i].topicId == topicId)
            {
                games.Add(allGames[i]);
            }
        }
        return games.ToArray();
    }
}
