using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoSanh_LT1_lvlMap : MonoBehaviour, LevelMap
{
    public SS_InputAreaLT1 InputNumberLeft;
    public SS_InputAreaLT1 InputNumberRight;
    public SS_InputAreaLT1 InputOperator;
    [Header("Stats")]
    public string nameLevel;
    public string levelType;
    public string hardness;
    [SerializeField] bool isFixedOutputAbility = false;
    [SerializeField] string outputAbility;
    [SerializeField] bool canReact;

    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongItemCount = 0;

        //calculate input number
        if(InputNumberLeft.AreaInputedNumber == InputNumberLeft.leftNum)  { rightCount++; }
        else  { wrongCount++; }
        if (InputNumberRight.AreaInputedNumber == InputNumberRight.rightNum) { rightCount++; }
        else  { wrongCount++; }
        //calculate input operator
        if(InputOperator.AreaInputedOperator == InputOperator.operatorNeeded) { rightCount++; }
        else { wrongCount++; }

        SS_RandomLT1 rand = GetComponentInChildren<SS_RandomLT1>();
        if(rand.rightCount != 0 || rand.wrongCount != 0)
        {
            rightCount += rand.rightCount;
            wrongCount += rand.wrongCount;
        }
        else
        {
            wrongCount++;
        }
        wrongItemCount += rand.wrongChoiceCount;

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
        yield return new WaitForSeconds(2.25f);

        // TimeLine play
        TimeLineManage.INSTANCE.Tiet3("LT1", 0);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        //Play countdown timeline
        TimeLineManage.INSTANCE.CountDownPlay();
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

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
        Tiet3AudioManager.instance.Stop();
        GetComponentInChildren<SS_RandomLT1>().StopAllCoroutines();
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
}
