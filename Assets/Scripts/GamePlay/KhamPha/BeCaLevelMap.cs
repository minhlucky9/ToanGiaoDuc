using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeCaLevelMap : MonoBehaviour, LevelMap
{
    public ItemLuyenTap1[] listItems;
    public Animator num0WriteAnimator;
    public void OnFinishLevel()
    {
        OnQuitLevel();
        Achivement achievementOfLevel = new Achivement();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightItemCount = 0;
        int wrongItemCount = 0;
        for (int i = 0; i < listItems.Length; i++)
        {
            if (listItems[i].IsRight())
            {
                rightItemCount++;
            }
            else
            {
                wrongItemCount++;
            }
        }
        achievementOfLevel.GameMode = GameManager.Instance.GetCurrentMode();
        achievementOfLevel.SetRightAnswerCount(rightItemCount);
        achievementOfLevel.SetTimeCount((int)timeCount);
        achievementOfLevel.SetWrongAnswerCount(wrongItemCount);
        achievementOfLevel.SetWrongSelectTimeCount(0);
        //AchivementManager.INSTANCE.AddAchiveMent(achievementOfLevel);
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
        TimeLineManage.INSTANCE.PhanTichBeCaPlay();
        float time = (float)TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds(6.67f);
        num0WriteAnimator.Play("sheet0");
        time -= 6.67f;
        yield return new WaitForSeconds(2.15f);
        time -= 2.15f;
        listItems[listItems.Length - 1].UpdateValue(listItems[listItems.Length - 1].GetRequiredValue());
        yield return new WaitForSeconds(time+2f);
        TimeLineManage.INSTANCE.StopCurrentDirector();
        MessageCallBackPopupPanel.INSTACNE.Active("Chúc mừng bạn đã hoàn thành phần khám phá!", CallEnd, true);
    }
    private void CallEnd(bool notMatter)
    {
        LevelManager.INSTANCE.SetCurrentLevelMap(transform.parent.gameObject);
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
        GameUIPanel.Instance.Active();
        UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Playing);
        StartCoroutine(StartIE());
    }
    private IEnumerator StartIE()
    {
        GameUIPanel.Instance.ResetTimeText() ;
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2f);
        TimeLineManage.INSTANCE.GiaoNhiemVuBeCaPlay();
        float time = (float)TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds(time);
        TimeLineManage.INSTANCE.CountDownPlay();
        time = (float)TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds(time);
        GameManager.Instance.StartGame();
        UIManager.Instance.OnLevelPrepareComplete();
    }

    public void Setup()
    {

    }

}
