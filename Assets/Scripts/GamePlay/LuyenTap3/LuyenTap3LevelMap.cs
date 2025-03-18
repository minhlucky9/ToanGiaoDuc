using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LuyenTap3LevelMap : MonoBehaviour, LevelMap
{
    public ItemManageLuyenTap3 itemManager;

    public AreaManageLuyenTap3 areaManage;

    public string nameLevel;

    [SerializeField]
    private string hardness;

    public string levelType;

    [SerializeField]
    private bool isFixedOutputAbility = false;

    [SerializeField]
    private string outputAbility;

    [SerializeField]
    private bool canReaction = false;

    public void OnFinishLevel()
    {
        OnQuitLevel();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongItemCount = 0;
        List<AreaLuyenTap3> listAreaToCheck = areaManage.GetList();
        for (int i = 0; i < listAreaToCheck.Count; i++)
        {
            if (listAreaToCheck[i].IsWinThisArea() && listAreaToCheck[i].IsDone())
            {
                rightCount++;
            }
            else
            {
                wrongCount++;
            }
            wrongItemCount += listAreaToCheck[i].wrongChoiceCount;
        }
        Achivement achievement = new Achivement();
        float rightRate = 0;
        try
        {
            rightRate =  rightCount / (rightCount + wrongCount);
        }
        catch { }
        if (!isFixedOutputAbility)
        {
            if (rightRate >= 0.9f)
            {
                achievement.OutputAbility = "snd2; gqvd3";
            }
            else
            {
                achievement.OutputAbility = "snd1; gqvd2";
            }
        }
        else
        {
            achievement.OutputAbility = outputAbility;
        }
        achievement.LevelName = nameLevel;
        achievement.LevelHardess = hardness;
        achievement.HardMode = GameManager.Instance.GetCurrentHardMode();
        achievement.GameMode = GameManager.Instance.GetCurrentMode();
        achievement.levelType = levelType;
        achievement.SetTimeCount((int)GameManager.Instance.GetTimeCount());
        achievement.SetRightAnswerCount(rightCount);
        achievement.SetWrongAnswerCount(wrongCount);
        achievement.SetWrongSelectTimeCount(wrongItemCount);
        AchivementManager.INSTANCE.AddAchiveMent(achievement);
        GameManager.Instance.SetLastAchievement(achievement);
    }

    public void OnStartLevel()
    {
        StartCoroutine(StartIE());
    }

    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.5f);
        TimeLineManage.INSTANCE.LuyenTap3TutPlay();
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        Debug.Log((float)timeW1);
        yield return new WaitForSeconds((float)timeW1);
        TimeLineManage.INSTANCE.CountDownPlay();
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        Debug.Log((float)timeW1);
        yield return new WaitForSeconds((float)timeW1);
        GameManager.Instance.StartGame();
        UIManager.Instance.OnLevelPrepareComplete();
        if (canReaction)
        {
            GameManager.Instance.TurnOnReaction();
        }
        else
        {
            GameManager.Instance.TurnOffReaction();
        }
    }

    public void Setup()
    {
    }

    public void OnQuitLevel()
    {
        Debug.Log("Call Stop");
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
}
