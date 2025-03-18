using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuyenTap4_Coloring : MonoBehaviour, LevelMap
{
    public int[] listQuantities;
    public int[] listSprites;
    public GameObject[] listItems;
    private bool[] markSprite;
    private bool[] markQuantity;
    public GameObject pen;
    public bool canReaction;
    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        for (int i = 0; i < listItems.Length; i++)
        {
            if(listItems[i].GetComponent<ItemValue>().IsRight())
            {
                rightCount++;
            }
            else
            {
                wrongCount++;
            }
        }
        Achivement achivement = new Achivement();
        achivement.GameMode = GameManager.Instance.GetCurrentMode();
        achivement.LevelName = "Coloring";
        achivement.HardMode = GameManager.Instance.GetCurrentHardMode();
        achivement.SetTimeCount((int)timeCount);
        achivement.SetRightAnswerCount(rightCount);
        achivement.SetWrongAnswerCount(wrongCount);
        AchivementManager.INSTANCE.AddAchiveMent(achivement);
        GameManager.Instance.SetLastAchievement(achivement);
    }

    public void OnQuitLevel()
    {
        pen.SetActive(false);
        Debug.Log("Call Stop");
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }
    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.5f);
        //tut
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

    public void OnStartLevel()
    {
        Setup();
        StartCoroutine(StartIE());
    }

    public void Setup()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        List<int> spriteList = new List<int>();
        pen.SetActive(true);
        if (InputCallBackPen.Instance != null)
        {
            InputCallBackPen.Instance.OnStart();
        }
        for (int i = 0; i < listItems.Length; i++)
        {
            int a = Random.Range(0, 9);
            int tryTime = 0;
            do
            {
                a = Random.Range(0, 9);
                tryTime++;
                if (tryTime > 9)
                {
                    break;
                }
            }
            while (spriteList.Contains(a));
            spriteList.Add(a);
            listItems[i].GetComponent<ItemColorPainting>().SetSprite(spriteList[i]);
            listItems[i].GetComponent<ItemValue>().SetRequireValue(Random.Range(6, 10));
        }
    }
}
