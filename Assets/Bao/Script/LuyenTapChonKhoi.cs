using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuyenTapChonKhoi : MonoBehaviour,LevelMap
{
    public string nameLevel;
    public string levelType;
    [SerializeField]
    private string hardness;
    public bool canReaction;
    public List<GameObject> allPos;
    public List<GameObject> wrong;
    public List<GameObject> right;
    public int quantity = 0;
    [SerializeField]
    private LevelType type;
    public enum LevelType
    {
        LP, CN
    }
    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongChoiceCount = 0;
        for(int i = 0; i < allPos.Count; i++)
        {
            wrongChoiceCount += allPos[i].GetComponent<PosChoice>().wrongCount;
            if (allPos[i].GetComponent<PosChoice>().isRight)
            {
                rightCount++;
            }
        }
        wrongCount = quantity - rightCount;
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
            case LevelType.LP:
                TimeLineManage.INSTANCE.ChonLPTutPlay();
                break;
            case LevelType.CN:
                TimeLineManage.INSTANCE.ChonCNTutPlay();
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
        int x = Random.Range(1, 4);
        quantity = x;
        List<GameObject> allBlockShow = new List<GameObject>();
        List<int> shapeRequest = new List<int>();
        for (int j = 0; j < x; j++)
        {
            int r = Random.Range(0, right.Count);
            int tryTime = 0;
            do
            {
                r = Random.Range(0, right.Count);
                tryTime++;
                if (tryTime > 8)
                {
                    break;
                }
            }
            while (shapeRequest.Contains(r));
            shapeRequest.Add(r);
        }
        for(int i = 0; i < shapeRequest.Count; i++)
        {
            allBlockShow.Add(right[shapeRequest[i]]);
        }
        shapeRequest = new List<int>();
        for (int j = 0; j < 7- x; j++)
        {
            int r = Random.Range(0, wrong.Count);
            int tryTime = 0;
            do
            {
                r = Random.Range(0, wrong.Count);
                tryTime++;
                if (tryTime > 8)
                {
                    break;
                }
            }
            while (shapeRequest.Contains(r));
            shapeRequest.Add(r);
        }
        for (int i = 0; i < shapeRequest.Count; i++)
        {
            allBlockShow.Add(wrong[shapeRequest[i]]);
        }
        List<int> shapePos = new List<int>();
        for(int i = 0; i < 7; i++)
        {
            int r = Random.Range(0, 7);
            int tryTime = 0;
            do
            {
                r = Random.Range(0, 7);
                tryTime++;
                if (tryTime > 100)
                {
                    break;
                }
            }
            while (shapePos.Contains(r));
            shapePos.Add(r);
        }
        for(int i = 0; i < allBlockShow.Count; i++)
        {
            Vector3 pos = new Vector3(allPos[shapePos[i]].transform.position.x, allBlockShow[i].transform.position.y, allBlockShow[i].transform.position.z);
            allBlockShow[i].transform.position = pos;
            allBlockShow[i].SetActive(true);
        }
        for(int i = 0; i < x; i++)
        {
            allPos[shapePos[i]].GetComponent<PosChoice>().isRightChoice = true;
        }
    }
}
