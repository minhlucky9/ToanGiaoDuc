using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoSanh_KhamPha_lvlmap : MonoBehaviour, LevelMap
{
    public SS_ItemManager_KhamPha itemManager_1;
    public SS_AreaReceiverManager_KhamPha areaManage_1;
    public SS_ItemManager_KhamPha itemManager_2;
    public SS_AreaReceiverManager_KhamPha areaManage_2;
    public SS_InputArea_KhamPha operatorPhase3_1;
    public SS_InputArea_KhamPha operatorPhase3_2;
    public Animator napAm;
    public string nameLevel;

    public void OnFinishLevel()
    {
        Debug.Log("Finish khampha 3");
        OnQuitLevel();
        float timeCount = GameManager.Instance.GetTimeCount();
        int rightCount = 0;
        int wrongCount = 0;
        int wrongItemCount = 0;

        //calc
        List<SS_AreaReceiver_KhamPha> listAreaToCheck1 = areaManage_1.GetList();
        List<SS_AreaReceiver_KhamPha> listAreaToCheck2 = areaManage_2.GetList();
        for (int i = 0; i < listAreaToCheck1.Count; i++)
        {
            if (listAreaToCheck1[i].listItem.Count != 0) { rightCount++; }
            else { wrongCount++; }
        }
        for (int i = 0; i < listAreaToCheck2.Count; i++)
        {
            if (listAreaToCheck2[i].listItem.Count != 0) { rightCount++; }
            else { wrongCount++; }
        }
        if(operatorPhase3_1.neededState == operatorPhase3_1.InputedState) { rightCount++; }
        else { wrongCount++; }
        if (operatorPhase3_2.neededState == operatorPhase3_2.InputedState) { rightCount++; }
        else { wrongCount++; }

        SS_KhamPha khampha = GetComponentInChildren<SS_KhamPha>();
        rightCount += khampha.RightCount;
        wrongCount += khampha.WrongCount;
        wrongItemCount += khampha.WrongChoiceCount;
        switch (khampha.curPhase)
        {
            case 1:
            case 2:
                wrongCount += 8;
                break;
            case 3:
            case 4:
                wrongCount += 7;
                break;
            case 5:
                wrongCount += 6;
                break;
            case 6: 
                wrongCount += 4;
                break;
            case 7:
                wrongCount += 3;
                break;
            case 8:
            case 9:
            case 10:
                wrongCount += 1;
                break;
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
    }

    public void OnStartLevel()
    {
        StartCoroutine(StartIE());
    }
    private IEnumerator StartIE()
    {
        UIManager.Instance.OnLevelPrepare();
        yield return new WaitForSeconds(2.25f);

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Huongdan", 1);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();

        yield return new WaitForSeconds(13.5f);
        napAm.SetBool("Run", true);

        yield return new WaitForSeconds((float)timeW1 - 13.5f);

        //Play countdown timeline
        TimeLineManage.INSTANCE.CountDownPlay();
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        GameManager.Instance.StartGame();

        UIManager.Instance.OnLevelPrepareComplete();
    }

    public void Setup()
    {

    }
}
