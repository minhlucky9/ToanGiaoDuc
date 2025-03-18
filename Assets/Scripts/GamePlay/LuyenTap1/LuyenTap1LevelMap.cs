using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuyenTap1LevelMap : MonoBehaviour, LevelMap
{
    [SerializeField]
    private List<ItemLuyenTap1> listItem;
    [SerializeField]
    private LevelType type;
    public string nameLevel;
    public string levelType;
    [SerializeField]
    private string hardness;
    [SerializeField]
    private bool isFixedOutputAbility = false;
    [SerializeField]
    private string outputAbility;
    [SerializeField]
    private bool canReaction = false;
    public GameObject block;
    public enum LevelType
    {
        LT1, LT4, LT5,LTHH,LTDHLP,LTDHCN
    }


    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongChoiceCount = 0;
        for(int i=0;i<listItem.Count; i++)
        {
            if (listItem[i].gameObject.activeSelf)
            {
                if (listItem[i].IsRight())
                {
                    if (type.Equals(LevelType.LT5))
                    {
                        if(listItem[i].isLV5Check)
                            rightCount++;
                    }
                    else
                    {
                        rightCount++;
                    }
                }
                else
                {
                    wrongCount++;
                }
                wrongChoiceCount += listItem[i].wrongChoiceCount;
            }
        }
        Achivement achievement = new Achivement();
        achievement.GameMode = GameManager.Instance.GetCurrentMode();
        if (!isFixedOutputAbility)
        {
            float rightRate = 0;
            try
            {
                rightRate = rightCount / (rightCount + wrongCount);
            }
            catch { }
            if (rightRate >= 0.9f)
            {
                switch (type)
                {
                    case LevelType.LT1:
                        achievement.OutputAbility = "snd1; gqvd1";
                        break;
                    case LevelType.LT4:
                        achievement.OutputAbility = "snd3; gqvd2";
                        break;
                    case LevelType.LT5:
                        achievement.OutputAbility = "TTLLTH3; gqvd3";
                        break;
                }
            }
            else if (rightRate >= .7f)
            {
                switch (type)
                {
                    case LevelType.LT1:
                        achievement.OutputAbility = "snd1; gqvd1";
                        break;
                    case LevelType.LT4:
                        achievement.OutputAbility = "snd2; gqvd1";
                        break;
                    case LevelType.LT5:
                        achievement.OutputAbility = "TTLLTH2; gqvd2";
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case LevelType.LT1:
                        achievement.OutputAbility = "snd1; gqvd1";
                        break;
                    case LevelType.LT4:
                        achievement.OutputAbility = "snd3; gqvd2";
                        break;
                    case LevelType.LT5:
                        achievement.OutputAbility = "TTLLTH1; gqvd1";
                        break;
                }
            }
        }
        else
        {
            achievement.OutputAbility = outputAbility;
        }
        achievement.LevelName = nameLevel;
        achievement.HardMode = GameManager.Instance.GetCurrentHardMode();
        achievement.LevelHardess = hardness;
        achievement.levelType = levelType;
        achievement.SetTimeCount((int)timeCount);
        achievement.SetRightAnswerCount(rightCount);
        achievement.SetWrongAnswerCount(wrongCount);
        achievement.SetWrongSelectTimeCount(wrongChoiceCount);
        AchivementManager.INSTANCE.AddAchiveMent(achievement);
        GameManager.Instance.SetLastAchievement(achievement);
    }
    
    public void OnStartLevel()
    {
        StartCoroutine(StartIE());
    }
    private IEnumerator StartIE()
    {
        Setup();
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.5f);
        switch (type)
        {
            case LevelType.LT1:
                TimeLineManage.INSTANCE.LuyenTap1TutPlay();
                break;
            case LevelType.LT4:
                TimeLineManage.INSTANCE.LuyenTap1TutPlay();
                break;
            case LevelType.LT5:
                TimeLineManage.INSTANCE.LuyenTap5TutPlay();
                break;
            case LevelType.LTHH:
                TimeLineManage.INSTANCE.DemHinhTutPlay();
                break;
            case LevelType.LTDHLP:
                TimeLineManage.INSTANCE.DemLPTutPlay();
                break;
            case LevelType.LTDHCN:
                TimeLineManage.INSTANCE.DemCNTutPlay();
                break;
        }
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
        if (type == LevelType.LTDHLP|| type == LevelType.LTDHCN)
        {
            int tmp = Random.Range(0, block.transform.childCount);
            GameObject currentBlock = block.transform.GetChild(tmp).gameObject;
            currentBlock.SetActive(true);
            int quantity = currentBlock.GetComponent<ItemValue>().GetRequireValue();
            listItem[0].GetComponent<ItemLuyenTap1>().SetRequiredValue(quantity);
        }
    }

    public void OnQuitLevel()
    {
        Debug.Log("Call Stop");
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
}
