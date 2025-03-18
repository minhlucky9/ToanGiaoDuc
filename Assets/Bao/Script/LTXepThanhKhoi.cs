using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LTXepThanhKhoi : MonoBehaviour,LevelMap
{
    [SerializeField]
    private PlayableDirector tut;
    public GameObject itemContainer;
    public GameObject groupContainer;
    public GameObject slotContainer;
    public List<GameObject> allGroup;
    public GameObject currentGroup;
    public string nameLevel;
    public string levelType;
    public bool canReaction;
    [SerializeField]
    private string hardness;
    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongChoiceCount = 0;
        for(int i = 0; i < slotContainer.transform.childCount; i++)
        {
            if (!slotContainer.transform.GetChild(i).GetComponent<SlotShape>().isRight)
            {
                wrongCount++;
            }
        }
        for(int i=0;i< itemContainer.transform.childCount; i++)
        {
            wrongChoiceCount += itemContainer.transform.GetChild(i).GetComponent<ToShapeSlotItem>().wrongChoiceCount;
            if (itemContainer.transform.GetChild(i).GetComponent<ToShapeSlotItem>().isRight)
            {
                rightCount++;
            }
        }
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

        TimeLineManage.INSTANCE.SetAndPlayCurrentPayableDirector(tut);

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
        Debug.Log(groupContainer.transform.childCount);
        for(int i = 0; i < groupContainer.transform.childCount; i++)
        {
            allGroup.Add(groupContainer.transform.GetChild(i).gameObject);
        }
        if (allGroup.Count > 0)
        {
            currentGroup = allGroup[Random.Range(0, allGroup.Count)];
            currentGroup.SetActive(true);
            for (int i = 0; i < itemContainer.transform.childCount; i++)
            {
                //allItems.Add(itemContainer.transform.GetChild(i).gameObject);
                slotContainer = currentGroup.transform.GetChild(0).gameObject;
                itemContainer.transform.GetChild(i).GetComponent<ToShapeSlotItem>().slotContainer = slotContainer;
                itemContainer.transform.GetChild(i).GetComponent<ToShapeSlotItem>().OnStart();
            }
            
        }
    }
}
