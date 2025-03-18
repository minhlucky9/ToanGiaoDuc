using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet5_KP_lvlmap : MonoBehaviour, LevelMap
{

    public string nameLevel;

    public void OnFinishLevel()
    {
        Debug.Log("Finish khampha 5");
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongItemCount = 0;

        //calc

        //Achievement
        Achivement achievement = new Achivement
        {
            GameMode = GameManager.Instance.GetCurrentMode()
        };
        //misc
        achievement.LevelName = nameLevel;
        achievement.SetRightAnswerCount(rightCount);
        achievement.SetTimeCount((int)timeCount);
        achievement.SetWrongAnswerCount(wrongCount);
        achievement.SetWrongSelectTimeCount(wrongItemCount);
        AchivementManager.INSTANCE.AddAchiveMent(achievement);
        GameManager.Instance.SetLastAchievement(achievement);

        GameManager.Instance.FinishGame();
        GameUIPanel.Instance.Deactive();
        EndGamePane.Instance.Active(RunPhanTich);
        UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.End);
    }

    public void RunPhanTich()
    {
        MessageCallBackPopupPanel.INSTACNE.Active("Chúc mừng bạn đã hoàn thành phần khám phá!", CallEnd, true);
    }
    private void CallEnd(bool notMatter)
    {
        LevelManager.INSTANCE.DestroyCurrentLevel();
        UIManager.Instance.BackFromExplore();
    }

    public void OnQuitLevel()
    {
        StopAllCoroutines();
        TimeLineManage.INSTANCE.StopCurrentDirector();
        resetCharPos.INSTANCE.resetPos();
    }

    public void OnStartLevel()
    {
        StartCoroutine(StartIE());
    }
    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.25f);

        Tiet5_KP kp = GetComponentInChildren<Tiet5_KP>();
        StartCoroutine(kp.startTimeline(1));
        for (int i = 0; i < kp.listAnimPhase1.Count; i++)
        {
            StartCoroutine(kp.playAnim(kp.listAnimPhase1[i]));
        }

        GameManager.Instance.StartGame();

        UIManager.Instance.OnLevelPrepareComplete();

    }

    public void Setup()
    {

    }
}
