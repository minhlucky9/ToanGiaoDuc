using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toan2_T2_KhamPhaLevelmap : MonoBehaviour, LevelMap
{
    public string nameLevel;
    public ItemManage itemManager;
    public AreaManage areaManager;
    public Animator item1Anim, item2Anim;
    public Animator[] listNumberAnimator;
    public GameObject[] listNumber;
    public ItemContainBoxcollider[] listItem;
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
        TimeLineManage.INSTANCE.PhanTich6_10Play();

        yield return new WaitForSeconds(6.4f);
        listNumberAnimator[0].Play("sheet6");
        yield return new WaitForSeconds(3f);
        listNumber[0].SetActive(true);

        yield return new WaitForSeconds(7.4f);
        listNumberAnimator[1].Play("sheet7");
        yield return new WaitForSeconds(3f);
        listNumber[1].SetActive(true);

        yield return new WaitForSeconds(7.1f);
        listNumberAnimator[2].Play("sheet8");
        yield return new WaitForSeconds(3f);
        listNumber[2].SetActive(true);

        yield return new WaitForSeconds(7.4f);
        listNumberAnimator[3].Play("sheet9");
        yield return new WaitForSeconds(3f);
        listNumber[3].SetActive(true);

        yield return new WaitForSeconds(7.28f);
        listNumberAnimator[4].Play("sheet10");
        yield return new WaitForSeconds(3f);
        listNumber[4].SetActive(true);

        yield return new WaitForSeconds(1.3f);
        TimeLineManage.INSTANCE.StopCurrentDirector();
        MessageCallBackPopupPanel.INSTACNE.Active("Chúc mừng bạn đã hoàn thành phần khám phá!", CallEnd, true);
    }
    private void CallEnd(bool notMatter)
    {
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
        yield return new WaitForSeconds(2.5f);
        TimeLineManage.INSTANCE.HuongDanPlay();
        yield return new WaitForSeconds(11.167f);
        Debug.Log("abcabc");
        item1Anim.Play("in");
        yield return new WaitForSeconds(4.283f);
        Debug.Log("cde cde");
        item2Anim.Play("in");
        yield return new WaitForSeconds(9.717f);
        //float time = (float)TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        //yield return new WaitForSeconds(time);
        TimeLineManage.INSTANCE.CountDownPlay();
        float time = (float)TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds(time);
        GameManager.Instance.StartGame();
        item1Anim.enabled = false;
        item2Anim.enabled = false;
        UIManager.Instance.OnLevelPrepareComplete();
    }

    public void Setup()
    {

    }
    public void Skip()
    {

    }
}
