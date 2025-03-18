using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet6_KP_lvlmap : MonoBehaviour, LevelMap
{
    public List<Tiet6_Input> inputs;
    public string nameLevel;
    [SerializeField] bool canReact;

    public void OnFinishLevel()
    {
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongItemCount = 0;

        //calc
        foreach (Tiet6_Input item in inputs)
        {
            wrongItemCount += item.getWrongChoiceCount();
            if(item.getInput() != item.neededInput) { wrongCount++; }
            else { rightCount++; }
        }
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

        Tiet6_KP kp = GetComponentInChildren<Tiet6_KP>();
        StartCoroutine(kp.startTimeline(1));
        for (int i = 0; i < kp.listAnimPhase1.Count; i++)
        {
            StartCoroutine(kp.playAnim(kp.listAnimPhase1[i]));
        }

        GameManager.Instance.StartGame();
        if (canReact)
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
}
