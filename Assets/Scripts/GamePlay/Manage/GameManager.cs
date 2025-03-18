using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private int idBaiHoc;
    [SerializeField]
    private BaiHoc currentBaiHoc;
    [SerializeField]
    private TietHoc currentTietHoc;
    [SerializeField]
    private float timeCount;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int currentHardMode;
    [SerializeField]
    private ModeLessonEnum currentMode;
    [SerializeField]
    private Text titleLessonText;
    private Achivement lastAchievement;
    private bool canCountTime = false;
    public bool canPlay = false;
    public bool isDoneAll = false;
    public bool canShowReaction = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    public void SelectBaiHoc(BaiHoc lesson)
    {
        currentBaiHoc = lesson; 
    }
    public void SelectTietHoc(TietHoc mode)
    {
        currentTietHoc = mode;
        if (titleLessonText == null) return;
        titleLessonText.text = mode.GetTenTietHoc();
    }
    public BaiHoc GetCurrentBaiHoc()
    {
        return currentBaiHoc;
    }
    public TietHoc GetCurrentTietHoc()
    {
        return currentTietHoc;
    }
    public void SetCurrentMode(ModeLessonEnum mode)
    {
        currentMode = mode;
    }
    public void SetCurrentLevel(int level)
    {
        if (level < 0) return;
        currentLevel = level;
    }
    public int GetCurrentHardMode()
    {
        return currentHardMode;
    }
    public void SetCurrentHardMode(int mode)
    {
        if (mode < 0) return;
        currentHardMode = mode;
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public ModeLessonEnum GetCurrentMode()
    {
        return currentMode;
    }
    public void SetLastAchievement(Achivement a)
    {
        lastAchievement = a;
    }
    public Achivement GetLastAchievement()
    {
        return lastAchievement;
    }
    public void StartGame()
    {
        timeCount = 0;
        canPlay = true;
        isDoneAll = false;
        StartCountTime();

    }
    public void TurnOnReaction()
    {
        canShowReaction = true;
    }
    public void TurnOffReaction()
    {
        canShowReaction = false;
    }
    private void StartCountTime()
    {
        canCountTime = true;
    }
    private void StopCountTime()
    {
        canCountTime = false;
    }
    public void PauseGame()
    {
        canPlay = false;
        canCountTime = false;
    }
    public void ConitnueGame()
    {
        canPlay = true;
        StartCountTime();
    }
    public void FinishGame()
    {
        canCountTime = false;
        canPlay = false;
    }
    public float GetTimeCount()
    {
        return timeCount;
    }
    private void Update()
    {
        if (canCountTime)
        {
            timeCount += Time.deltaTime;
        }
    }

}