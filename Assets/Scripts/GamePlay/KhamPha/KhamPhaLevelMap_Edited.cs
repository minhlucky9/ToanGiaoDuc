using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KhamPhaLevelMap_Edited : MonoBehaviour, LevelMap
{
    [SerializeField]
    DirectCustom[] StartDirector;
    [SerializeField]
    DirectCustom[] FinishDirector;
    public string nameLevel;
    public ItemManage itemManager;
    public AreaManage areaManager;
    public GameObject nextLevel_BeCa;
    public ItemContainBoxcollider[] listItem;
    [SerializeField]
    private bool hasBeCa = false;
    public void OnFinishLevel()
    {
        OnQuitLevel();
        Achivement achievementOfLevel = new Achivement();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightItemCount = 0;
        int wrongItemCount = 0;
        int wrongSelectCount = 0;
        List<AreaPlay> listArea = areaManager.GetList();
        for (int i = 0; i < listItem.Length; i++)
        {
            if (listItem[i].gameObject.activeSelf)
            {
                if (listItem[i].IsRight())
                {
                    rightItemCount++;
                }
                else
                {
                    wrongItemCount++;
                }
                wrongSelectCount += listItem[i].wrongSelectCount;
            }
        }
        achievementOfLevel.LevelName = nameLevel;
        achievementOfLevel.GameMode = GameManager.Instance.GetCurrentMode();
        achievementOfLevel.SetRightAnswerCount(rightItemCount);
        achievementOfLevel.SetTimeCount((int)timeCount);
        achievementOfLevel.SetWrongAnswerCount(wrongItemCount);
        achievementOfLevel.SetWrongSelectTimeCount(wrongSelectCount);
        achievementOfLevel.SetWrongSelectTimeCount(0);
        AchivementManager.INSTANCE.AddAchiveMent(achievementOfLevel);
        GameManager.Instance.SetLastAchievement(achievementOfLevel);
        OnFinishFirstAction();
    }
    private void OnFinishFirstAction()
    {
        GameManager.Instance.FinishGame();
        GameUIPanel.Instance.Deactive();
        EndGamePane.Instance.Active(RunPhanTich);
        UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.End);
    }
    public void RunPhanTich()
    {
        StartCoroutine(FinishIE());
    }
    private IEnumerator FinishIE()
    {
        for (int i = 0; i < FinishDirector.Length; i++)
        {
            if (FinishDirector[i].type == DirectType.Wait)
            {
                yield return new WaitForSeconds(FinishDirector[i].timeWait);
            }
            else if (FinishDirector[i].type == DirectType.Event)
            {
                FinishDirector[i].events.Invoke();
            }
            else if (FinishDirector[i].type == DirectType.Both_EventAndWait)
            {
                FinishDirector[i].events.Invoke();
                yield return new WaitForSeconds(FinishDirector[i].timeWait);
            }
        }
        if (hasBeCa)
        {
            MessageCallBackPopupPanel.INSTACNE.Active("Chúng ta hãy cùng tiếp tục đến với phần tiếp theo nhé!", CallBeCa, true);
        }
        else
        {
            MessageCallBackPopupPanel.INSTACNE.Active("Chúc mừng bạn đã hoàn thành phần khám phá!", CallEnd, true);
        }
    }
    private void CallBeCa(bool checkk)
    {
        LevelManager.INSTANCE.SetCurrentLevelMap(nextLevel_BeCa);
        transform.GetChild(0).gameObject.SetActive(false);
        nextLevel_BeCa.SetActive(true);
        nextLevel_BeCa.GetComponent<LevelMap>().OnStartLevel();

    }
    private void CallEnd(bool notMatter)
    {
        LevelManager.INSTANCE.DestroyCurrentLevel();
        UIManager.Instance.BackFromExplore();
    }
    public void OnQuitLevel()
    {
        Debug.Log("Call Stop");
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
    }

    public void OnStartLevel()
    {
        Debug.Log("Kham pha On start");
        StartCoroutine(StartIE());
    }
    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        for (int i = 0; i < StartDirector.Length; i++)
        {
            if (StartDirector[i].type == DirectType.Wait)
            {
                yield return new WaitForSeconds(StartDirector[i].timeWait);
            }
            else if (StartDirector[i].type == DirectType.Event)
            {
                StartDirector[i].events.Invoke();
            }
            else if (StartDirector[i].type == DirectType.Both_EventAndWait)
            {
                StartDirector[i].events.Invoke();
                yield return new WaitForSeconds(StartDirector[i].timeWait);
            }
        }
        TimeLineManage.INSTANCE.CountDownPlay();
        float time = (float)TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds(time);
        GameManager.Instance.StartGame();
        UIManager.Instance.OnLevelPrepareComplete();
    }

    public void Setup()
    {

    }
}
public enum DirectType
{
    Wait, Event, Both_EventAndWait
}
[System.Serializable]
public class DirectCustom
{
    public DirectType type;
    public float timeWait;
    public UnityEvent events;
}