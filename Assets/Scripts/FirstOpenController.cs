using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstOpenController : MonoBehaviour {
    public static FirstOpenController Instance;
    public int open;
    private const string DidRate = "Did player rate game";
    public void MakeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
        open = 111;
        //PlayerPrefs.DeleteAll();
        IsGameStartTheFirstTime();
    }
    private void IsGameStartTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
            open = 123;
            PlayerPrefs.SetInt(DidRate, 0);
            //GemManage.FirstOpenInit();
            SoundManager.FirstInit();
            //LevelManager.FirstInit();
        }
    }
}
