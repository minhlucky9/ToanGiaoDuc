using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoSanh_LT7_lvlMap : MonoBehaviour, LevelMap
{
    public string nameLevel;
    public string levelType;
    public string hardness;
    [SerializeField] bool isFixedOutputAbility = false;
    [SerializeField] string outputAbility;
    [SerializeField] bool canReact = true;

    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongItemCount = 0;

        //calculate
        SS_RandomLT7 Lt7 = GetComponentInChildren<SS_RandomLT7>();
        List<Transform> question = Lt7.Spawnpoints;

        foreach (Transform quest in question)
        {
            if (quest.gameObject.activeInHierarchy)
            {
                SS_RandomLT7.oper need = quest.GetComponentInChildren<SS_InputOperatorLT7>().neededOper;
                SS_RandomLT7.oper cur = quest.GetComponentInChildren<SS_InputOperatorLT7>().curOper;

                if (need == cur) { rightCount++; }
                else { wrongCount++; }
            }
        }

        wrongItemCount += Lt7.WrongCount;

        //Achievement
        Achivement achievement = new Achivement
        {
            GameMode = GameManager.Instance.GetCurrentMode()
        };
        float rightRate = 0;
        try
        {
            rightRate = rightCount / (rightCount + wrongCount);
        }
        catch { }
        Debug.Log("Chua co outputAbiblity");
        if (!isFixedOutputAbility)
        {
            if (rightRate >= 0.9f)
            {
                achievement.OutputAbility = " ";
            }
            else
            {
                achievement.OutputAbility = " ";
            }
        }
        else
        {
            achievement.OutputAbility = outputAbility;
        }
        //misc
        achievement.LevelName = nameLevel;
        achievement.HardMode = GameManager.Instance.GetCurrentHardMode();
        achievement.LevelHardess = hardness;
        achievement.levelType = levelType;
        achievement.SetTimeCount((int)timeCount);
        achievement.SetRightAnswerCount(rightCount);
        achievement.SetWrongAnswerCount(wrongCount);
        achievement.SetWrongSelectTimeCount(wrongItemCount);
        AchivementManager.INSTANCE.AddAchiveMent(achievement);
        GameManager.Instance.SetLastAchievement(achievement);
    }

    public void OnStartLevel()
    {
        StartCoroutine(StartTietHoc());
    }

    IEnumerator StartTietHoc()
    {
        // Prepare
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.5f);

        // TimeLine play
        TimeLineManage.INSTANCE.Tiet3("LT1", 3);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        ////Play countdown timeline
        TimeLineManage.INSTANCE.CountDownPlay();
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        GetComponentInChildren<SS_RandomLT7>().canAnswer = true;
        // Start
        GameManager.Instance.StartGame();
        if (canReact)
        {
            GameManager.Instance.TurnOnReaction();
        }
        else
        {
            GameManager.Instance.TurnOffReaction();
        }
        // Prepare done
        UIManager.Instance.OnLevelPrepareComplete();
    }

    public void Setup()
    {

    }
    public void OnQuitLevel()
    {
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
}
