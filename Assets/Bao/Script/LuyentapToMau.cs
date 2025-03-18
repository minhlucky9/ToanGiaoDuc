using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LuyentapToMau : MonoBehaviour, LevelMap
{
    public RequestColor requestColor;
    public GameObject allItem;
    public GameObject pen;
    public bool isRandom;
    public List<GameObject> allItemForRandom;
    public string nameLevel;
    public string levelType;
    [SerializeField]
    private string hardness;
    public bool canReaction;
    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = requestColor.CountAllRight();
        int wrongCount = requestColor.CountAllWrong();
        int wrongChoiceCount = requestColor.CountAllWrongChoice();

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
        pen.SetActive(false);
        Debug.Log("Call Stop");
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.5f);
        TimeLineManage.INSTANCE.ToMauTutPlay();

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

    public void OnStartLevel()
    {
        Setup();
        StartCoroutine(StartIE());
    }

    public void Setup()
    {
        if (isRandom && allItemForRandom.Count > 0)
        {
            allItem = allItemForRandom[Random.Range(0, allItemForRandom.Count)];
        }
        allItem.SetActive(true);
        requestColor.Request(allItem);
    }
}
