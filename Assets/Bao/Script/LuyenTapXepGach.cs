using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuyenTapXepGach : MonoBehaviour, LevelMap
{
    public List<GameObject> allGroup;
    public GameObject group;
    public string nameLevel;
    public string levelType;
    public bool canReaction;
    [SerializeField]
    private string hardness;
    [SerializeField]
    private LevelType type;
    public enum LevelType
    {
        LT5, LT9
    }
    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        GroupXepGach gr = group.GetComponent<GroupXepGach>();
        gr.Caculate();
        int rightCount = gr.rightCount;
        int wrongCount = gr.wrongCount;
        int wrongChoiceCount = gr.wrongChoiceCount;

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

        switch (type)
        {
            case LevelType.LT5:
                TimeLineManage.INSTANCE.ChonHinhTutPlay();
                break;
            case LevelType.LT9:
                TimeLineManage.INSTANCE.XepGachTutPlay();
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
        if (allGroup.Count > 0)
        {
            group = allGroup[Random.Range(0, allGroup.Count)];
            group.SetActive(true);
        }
    }
}
