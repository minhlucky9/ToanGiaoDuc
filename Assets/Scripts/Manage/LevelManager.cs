using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager INSTANCE;
    private const string LEVEL = "level";
    [SerializeField]
    private string TitlePlayerPref;
    [SerializeField]
    private GameObject[] listKhamPha;
    [SerializeField]
    private GameObject[] listLuyenTap;
    [SerializeField]
    private GameObject[] listTuKiemTra1;
    [SerializeField]
    private GameObject[] listTuKiemTra2;
    [SerializeField]
    private GameObject[] listTuKiemTra3;
    [SerializeField]
    private GameObject currentLevelMap;
    public bool IsTestLevel = false;

    [SerializeField]
    private Sprite buttonLevelActiveButton;
    public Sprite ButtonLevelActiveSprite { get { return buttonLevelActiveButton; } set { buttonLevelActiveButton = value; } }
    [SerializeField]
    private Sprite buttonLevelDeactiveButton;
    public Sprite ButtonLevelDeactiveButton { get { return buttonLevelDeactiveButton; } set { buttonLevelDeactiveButton = value; } }
    public int levelTuKT;
    public int levelTuKT_Child;
    //private void Awake()
    //{
    //    if (INSTANCE == null)
    //    {
    //        INSTANCE = this;
    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}
    private void Start()
    {
        if (FirstOpenController.Instance.open == 123)
        {
            FirstInit();
            FirstInitOnStart();
        }
        if (IsTestLevel)
        {
            InitForTest();
        }
    }
    private void InitForTest()
    {
        for (int i = 1; i <= 5; i++)
        {
            PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "LT" + i, 1);
        }
        for (int i = 1; i <= levelTuKT; i++)
        {
            for (int j = 1; j <= levelTuKT_Child; j++)
            {
                PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "TKT" + i + "LV" + j, 1);
            }
        }
    }
    public void FirstInit()
    {
        //LuyenTap
        for (int i = 1; i <= 5; i++)
        {
            PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "LT" + i, 0);
        }
        PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "LT" + 1, 1);
        PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "KP" + 1, 1);
    }
    public void FirstInitOnStart()
    {
        for (int i = 1; i <= levelTuKT; i++)
        {
            for (int j = 1; j <= levelTuKT_Child; j++)
            {
                PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "TKT" + i + "LV" + j, 0);
            }
            int z = i;
            Debug.Log("Setup  lv: " + TitlePlayerPref + LEVEL + "TKT" + z + "LV" + 1);
            PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "TKT" + z + "LV" + 1, 1);
        }

    }
    public bool IsLevelOpen(ModeLessonEnum mode, int level)
    {
        if (mode == ModeLessonEnum.Practice)
        {

            return 1 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "LT" + level, 0) || 2 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "LT" + level, 0);
        }
        else if (mode == ModeLessonEnum.SelfExamination)
        {

        }

        return false;
    }
    public bool IsLevelOpen(ModeLessonEnum mode, int level, int index)
    {
        Debug.Log("Check is open mode " + mode + " lv" + level + " index" + index);
        if (mode == ModeLessonEnum.Practice)
        {
            return 1 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "LT" + level, 0) || 2 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "LT" + level, 0);
        }
        else if (mode == ModeLessonEnum.SelfExamination)
        {
            return 1 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "TKT" + level + "LV" + index, 0) || 2 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "TKT" + level + "LV" + index, 0);
        }
        return false;
    }
    public void OpenLevel(int level)
    {
        if (level < 0) return;
        PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "LT" + level, 1);
    }
    public void PassLevel(ModeLessonEnum mode, int level, int index)
    {
        if (level < 0 || index < 0) return;
        if (mode == ModeLessonEnum.Practice)
        {

            PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "LT" + level, 2);
        }
        else if (mode == ModeLessonEnum.SelfExamination)
        {
            PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "TKT" + level + "LV" + index, 2);
        }
    }
    public bool IsLevelPassed(ModeLessonEnum mode, int level, int index)
    {
        if (mode == ModeLessonEnum.Practice)
        {

            return 2 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "LT" + level, 0);
        }
        else if (mode == ModeLessonEnum.SelfExamination)
        {

            return 2 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "TKT" + level + "LV" + index, 0);
        }

        return false;
    }
    public void OpenLevel(ModeLessonEnum mode, int level, int index)
    {
        if (level < 0 || index < 0) return;
        if (IsLevelOpen(mode, level, index)) return;
        if (mode == ModeLessonEnum.Practice)
        {
            PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "LT" + level, 1);
        }
        else if (mode == ModeLessonEnum.SelfExamination)
        {
            PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "TKT" + level + "LV" + index, 1);
        }
    }

    public int GetOpenLevelCount(ModeLessonEnum mode, int level)
    {
        int count = 0;
        if (mode == ModeLessonEnum.Practice)
        {

            for (int i = 1; i <= 5; i++)
            {
                if (IsLevelOpen(mode, level))
                {
                    count++;
                }
            }
        }
        else if (mode == ModeLessonEnum.SelfExamination)
        {
            int levelMax = 0;
            int levelz = GameManager.Instance.GetCurrentHardMode();
            switch (levelz)
            {
                case 1:
                    levelMax = listTuKiemTra1.Length;
                    break;
                case 2:
                    levelMax = listTuKiemTra2.Length;
                    break;
                case 3:
                    levelMax = listTuKiemTra3.Length;
                    break;

            }
            for (int i = 1; i <= levelMax; i++)
            {
                if (IsLevelOpen(mode, level, i))
                {
                    count++;
                }
            }
        }
        return count;
    }
    public int GetPassedLevelCount(ModeLessonEnum mode, int level)
    {
        int count = 0;
        if (mode == ModeLessonEnum.Practice)
        {

            for (int i = 1; i <= 5; i++)
            {
                if (IsLevelPassed(mode, level, 0))
                {
                    count++;
                }
            }
        }
        else if (mode == ModeLessonEnum.SelfExamination)
        {
            int levelMax = 0;
            int levelz = GameManager.Instance.GetCurrentHardMode();
            switch (levelz)
            {
                case 1:
                    levelMax = listTuKiemTra1.Length;
                    break;
                case 2:
                    levelMax = listTuKiemTra2.Length;
                    break;
                case 3:
                    levelMax = listTuKiemTra3.Length;
                    break;

            }
            for (int i = 1; i <= levelMax; i++)
            {
                if (IsLevelPassed(mode, level, i))
                {
                    count++;
                }
            }
        }
        return count;
    }
    public bool IsDoneAllPracticeLevel()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (!IsLevelPassed(ModeLessonEnum.Practice, i, 0))
            {
                return false;
            }
        }
        return true;
    }
    public bool IsDoneOneSelfExamination()
    {
        for (int i = 1; i <= levelTuKT; i++)
        {
            bool check = true;
            int size = 0;
            if (i == 1)
            {
                size = listTuKiemTra1.Length;
            }
            else if (i == 2)
            {
                size = listTuKiemTra2.Length;
            }
            else
            {
                size = listTuKiemTra3.Length;
            }
            for (int j = 1; j <= size; j++)
            {
                if (!IsLevelPassed(ModeLessonEnum.SelfExamination, i, j))
                {
                    check = false;
                }
            }
            if (check) return true;
        }
        return false;
    }
    public void PassExplore()
    {
        PlayerPrefs.SetInt(TitlePlayerPref + LEVEL + "KP" + 1, 2);
    }
    public bool IsDoneExplore()
    {
        return 2 == PlayerPrefs.GetInt(TitlePlayerPref + LEVEL + "KP" + 1, 1);
    }
    public string GetProgressSelfExamination()
    {
        int levelCount = GetPassedLevelCount(GameManager.Instance.GetCurrentMode(), GameManager.Instance.GetCurrentHardMode());
        int levelMax = 0;
        int level = GameManager.Instance.GetCurrentHardMode();
        switch (level)
        {
            case 1:
                levelMax = listTuKiemTra1.Length;
                break;
            case 2:
                levelMax = listTuKiemTra2.Length;
                break;
            case 3:
                levelMax = listTuKiemTra3.Length;
                break;

        }
        string ret = "Bạn đã hoàn thành " + levelCount + "/" + levelMax + " bài của phần tự kiểm tra mức độ " + level;
        return ret;
    }

    public void LoadLevel(int level)
    {
        ModeLessonEnum mode = GameManager.Instance.GetCurrentMode();
        switch (mode)
        {
            case ModeLessonEnum.Explore:

                break;
            case ModeLessonEnum.Practice:
                break;
            case ModeLessonEnum.SelfExamination:
                break;
            default:
                break;
        }
    }
    public void LoadCurrentLevel()
    {
        if (currentLevelMap != null)
        {
            Destroy(currentLevelMap);
        }
        ModeLessonEnum mode = GameManager.Instance.GetCurrentMode();
        int levelIndex = GameManager.Instance.GetCurrentLevel();
        int hardMode = GameManager.Instance.GetCurrentHardMode();
        switch (mode)
        {
            case ModeLessonEnum.Explore:
                Debug.Log("Load explore level");
                levelIndex = GameManager.Instance.GetCurrentTietHoc().GetTietHocId();
                //if (levelIndex < 0 || levelIndex > listKhamPha.Length)
                //{
                //    Debug.Log("Loi load level: level truyen vao sai");
                //    return;
                //}
                //SelectModeLessonPanel.Instance.Deactive();
                //UIManager.Instance.UIPlayGame();
                //currentLevelMap = listKhamPha[levelIndex].gameObject;
                //currentLevelMap.transform.GetChild(0).gameObject.SetActive(true);
                currentLevelMap = Instantiate(listKhamPha[0].gameObject, transform, true);
                currentLevelMap.transform.GetChild(0).gameObject.SetActive(true);
                currentLevelMap.GetComponent<LevelMap>().OnStartLevel();
                break;
            case ModeLessonEnum.Practice:
                //SelectModeLessonPanel.Instance.Deactive();
                //SelectPracticeExcercisePanel.Instance.Deactive();
                //UIManager.Instance.UIPlayGame();
                levelIndex = Mathf.Clamp(levelIndex, 1, 1000);
                currentLevelMap = Instantiate(listLuyenTap[levelIndex - 1].gameObject, transform, true);
                currentLevelMap.transform.GetChild(0).gameObject.SetActive(true);
                currentLevelMap.GetComponent<LevelMap>().OnStartLevel();
                break;
            case ModeLessonEnum.SelfExamination:
                //SelectModeLessonPanel.Instance.Deactive();
                //SelectSelfExaminationPanel.Instance.Deactive();
                //UIManager.Instance.UIPlayGame();
                switch (hardMode)
                {
                    case 1:
                        currentLevelMap = Instantiate(listTuKiemTra1[levelIndex - 1].gameObject, transform, true);
                        break;
                    case 2:
                        currentLevelMap = Instantiate(listTuKiemTra2[levelIndex - 1].gameObject, transform, true);
                        break;
                    case 3:
                        currentLevelMap = Instantiate(listTuKiemTra3[levelIndex - 1].gameObject, transform, true);
                        break;
                }
                currentLevelMap.transform.GetChild(0).gameObject.SetActive(true);
                currentLevelMap.GetComponent<LevelMap>().OnStartLevel();
                break;
            default:
                break;
        }
    }
    public void DestroyCurrentLevel()
    {
        if (currentLevelMap != null)
        {
            Destroy(currentLevelMap);
        }
    }
    public int GetLevelNumberCount(ModeLessonEnum mode, int index)
    {
        switch (mode)
        {
            case ModeLessonEnum.Explore:
                return listKhamPha.Length;
                break;
            case ModeLessonEnum.Practice:
                return listLuyenTap.Length;
                break;
            case ModeLessonEnum.Print:
                return 0;
                break;
            case ModeLessonEnum.SelfExamination:
                switch (index)
                {
                    case 1:
                        return listTuKiemTra1.Length;
                    case 2:
                        return listTuKiemTra2.Length;
                    case 3:
                        return listTuKiemTra3.Length;
                }
                break;
        }
        return 0;
    }
    public void SetCurrentLevelMap(GameObject level)
    {
        currentLevelMap = level;
    }
    public void QuitLevel()
    {
        currentLevelMap.GetComponent<LevelMap>().OnQuitLevel();
    }
    public int GetNumberLevel(ModeLessonEnum mode, int hardMode = 0)
    {
        switch (mode)
        {
            case ModeLessonEnum.Explore:
                return listKhamPha.Length;
            case ModeLessonEnum.Practice:
                return listLuyenTap.Length;
            case ModeLessonEnum.SelfExamination:

                if(hardMode == 1)
                {
                    return listTuKiemTra1.Length;
                }
                else if(hardMode == 2)
                {
                    return listTuKiemTra2.Length;
                }
                else if(hardMode == 3)
                {
                    return listTuKiemTra3.Length;
                }
                else
                {
                    return 0;
                }
            default:
                return 0;
        }
    }
    public void FinishCurrentLevel()
    {
        Debug.Log("Call stop from Level manager");
        if (GameManager.Instance.GetCurrentMode() == ModeLessonEnum.Explore)
        {
            PassExplore();
        }
        currentLevelMap.GetComponent<LevelMap>().OnFinishLevel();
    }
    //public void ContinueExplore()
    //{
    //    currentLevelMap.GetComponent<KhamPhaLevelMap>().RunPhanTich();
    //}
}
