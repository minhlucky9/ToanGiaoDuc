using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuyenTapChonMiengGhep : MonoBehaviour, LevelMap
{

    public List<GameObject> allGroup;
    public GameObject group;
    public string nameLevel;
    public string levelType;
    [SerializeField]
    private string hardness;
    public bool canReaction;
    public List<GameObject> allHand;
    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongChoiceCount = 0;
        for(int i = 0; i < allHand.Count; i++)
        {
            if (allHand[i].GetComponent<HandChoice>().isRight)
            {
                rightCount++;
            }
            wrongChoiceCount += allHand[i].GetComponent<HandChoice>().wrongCount;
        }
        wrongCount = 1 - rightCount;
        Achivement achievement = new Achivement();
        achievement.GameMode = GameManager.Instance.GetCurrentMode();
        achievement.LevelName = nameLevel;
        achievement.HardMode = GameManager.Instance.GetCurrentHardMode();
        achievement.OutputAbility = hardness;
        achievement.levelType = levelType;
        achievement.SetTimeCount((int)timeCount);
        achievement.SetRightAnswerCount(rightCount);
        achievement.SetWrongAnswerCount(wrongCount);
        achievement.SetWrongSelectTimeCount(wrongChoiceCount);
        AchivementManager.INSTANCE.AddAchiveMent(achievement);
        GameManager.Instance.SetLastAchievement(achievement);
    }

    public void OnQuitLevel()
    {
        Debug.Log("Call Stop");
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }

    public void OnStartLevel()
    {
        Setup();
        StartCoroutine(StartIE());
    }
    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.5f);

        TimeLineManage.INSTANCE.ChonMiengGhepTutPlay();

        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        Debug.Log((float)timeW1);
        yield return new WaitForSeconds((float)timeW1);
        TimeLineManage.INSTANCE.CountDownPlay();
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        Debug.Log((float)timeW1);
        yield return new WaitForSeconds((float)timeW1);
        GameManager.Instance.StartGame();
        if (canReaction)
        {
            GameManager.Instance.TurnOnReaction();
        }
        else
        {
            GameManager.Instance.TurnOffReaction();
        }
        UIManager.Instance.OnLevelPrepareComplete();

    }
    public void Setup()
    {
        if (allGroup.Count > 0)
        {
            group = allGroup[Random.Range(0, allGroup.Count)];
            group.SetActive(true);
        }
    }
    public void OnRightChoice()
    {
        group.GetComponent<GroupMiengGhep>().OnRightChoice();
        OnFinishLevel();
    }
}
