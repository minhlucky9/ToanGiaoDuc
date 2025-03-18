using System.Collections;
using UnityEngine;
public class LuyenTap2LevelMap : MonoBehaviour, LevelMap
{
    [SerializeField]
    private ItemMoveMode2[] listItems;

    [SerializeField]
    private Transform ItemContainer;

    [SerializeField]
    private Transform DestinationContainer;

    [SerializeField]
    private int wrongItemCount = 0;

    [SerializeField]
    private int rightItemCount = 0;

    [SerializeField]
    private int totalWrongSelectCount = 0;

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

    public void OnFinishLevel()
    {
        OnQuitLevel();
        Achivement achievementOfLevel = new Achivement();
        float timeCount = GameManager.Instance.GetTimeCount();
        rightItemCount = 0;
        wrongItemCount = 0;
        for (int i = 0; i < listItems.Length; i++)
        {
            if (listItems[i].gameObject.activeSelf)
            {
                if (listItems[i].IsRightChoice())
                {
                    rightItemCount++;
                }
                else
                {
                    wrongItemCount++;
                }
                totalWrongSelectCount += listItems[i].wrongChoiceCount;
            }
        }
        float rightRate = 0;
        try
        {
            rightRate = rightItemCount / (rightItemCount + wrongItemCount);
        }
        catch { }
        if (!isFixedOutputAbility)
        {
            if (rightRate >= 0.9f)
            {
                achievementOfLevel.OutputAbility = "snd2; gqvd2";
            }
            else
            {
                achievementOfLevel.OutputAbility = "snd1; gqvd1";
            }

        }
        else
        {
            achievementOfLevel.OutputAbility = outputAbility;
        }
        achievementOfLevel.LevelName = nameLevel;
        achievementOfLevel.LevelHardess = hardness;
        achievementOfLevel.HardMode = GameManager.Instance.GetCurrentHardMode();
        achievementOfLevel.GameMode = GameManager.Instance.GetCurrentMode();
        achievementOfLevel.levelType = levelType;
        achievementOfLevel.SetRightAnswerCount(rightItemCount);
        achievementOfLevel.SetTimeCount((int)timeCount);
        achievementOfLevel.SetWrongAnswerCount(wrongItemCount);
        achievementOfLevel.SetWrongSelectTimeCount(totalWrongSelectCount);
        AchivementManager.INSTANCE.AddAchiveMent(achievementOfLevel);
        GameManager.Instance.SetLastAchievement(achievementOfLevel);
    }

    public void OnStartLevel()
    {
        StartCoroutine(StartIE());
    }

    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.5f);
        TimeLineManage.INSTANCE.LuyenTap2TutPlay();
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
    }

    public void OnQuitLevel()
    {
        Debug.Log("Call Stop");
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
}
