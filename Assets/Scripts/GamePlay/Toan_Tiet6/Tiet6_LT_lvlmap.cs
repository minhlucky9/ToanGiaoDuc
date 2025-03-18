using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet6_LT_lvlmap : MonoBehaviour, LevelMap
{
    public int idLT = 1;
    public List<Tiet6_Input> inputList;
    public List<Tiet6_AreaReceiver> areaList;
    public Tiet6_ItemManage itemManager;
    public bool IsInputList = true;
    [Header("Stats")]
    public string nameLevel;
    public string levelType;
    public string hardness;
    [SerializeField] bool canReact;

    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongItemCount = 0;

        //calculate input number
        if (IsInputList)
        {
            foreach (Tiet6_Input item in inputList)
            {
                wrongItemCount += item.getWrongChoiceCount();

                if (item.neededInput != item.getInput())
                {
                    wrongCount++;
                }
                else
                {
                    rightCount++;
                }
            }
        }
        else
        {
            foreach (Tiet6_AreaReceiver area in areaList)
            {
                int itemInputedCount = 0;
                foreach (Tiet6_ItemMove item in area.getListItem())
                {
                    itemInputedCount += item.ItemCount;
                }
                if(itemInputedCount == area.itemCountRequire)
                {
                    rightCount++;
                }
                else
                {
                    wrongCount++;
                }
            }
        }

        //Achievement
        Achivement achievement = new Achivement();
        achievement.GameMode = GameManager.Instance.GetCurrentMode();
        achievement.OutputAbility = hardness;
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
        TimeLineManage.INSTANCE.Tiet6("LT", idLT);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        GetComponentInChildren<PlayAnimList>().playAnimList();
        yield return new WaitForSeconds((float)timeW1);

        // Play countdown timeline
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
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
}
