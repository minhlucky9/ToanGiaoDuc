using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet5_double_lvlmap : MonoBehaviour, LevelMap
{

    public int idLT = 5;
    public List<Tiet5_Inputs> inputList1;
    public List<Tiet5_Inputs> inputList2;
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
        foreach (Tiet5_Inputs item in inputList1)
        {
            if (!item.hasFixedInput)
            {
                wrongItemCount += item.getWrongChoiceCount();

                switch (item.inputType)
                {
                    case Tiet5_Inputs.neededType.String:
                        if (item.getInputedString() == item.neededInputString) { rightCount++; }
                        else { wrongCount++; }
                        break;
                    case Tiet5_Inputs.neededType.Int:
                        if (item.getInputInt() == item.neededInputInt) { rightCount++; }
                        else { wrongCount++; }
                        break;
                }
            }
        }
        foreach (Tiet5_Inputs item in inputList2)
        {
            if (!item.hasFixedInput)
            {
                wrongItemCount += item.getWrongChoiceCount();

                switch (item.inputType)
                {
                    case Tiet5_Inputs.neededType.String:
                        if (item.getInputedString() == item.neededInputString) { rightCount++; }
                        else { wrongCount++; }
                        break;
                    case Tiet5_Inputs.neededType.Int:
                        if (item.getInputInt() == item.neededInputInt) { rightCount++; }
                        else { wrongCount++; }
                        break;
                }
            }
        }

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
        TimeLineManage.INSTANCE.Tiet5("LT", idLT);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        GetComponentInChildren<Tiet5_double_gp>().playAnimList(1);
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
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
        resetCharPos.INSTANCE.resetPos();
    }
}
