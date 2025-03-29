using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    public static GameplayUIController instance;
    public GameObject panel;
    public TMP_Text timerText;
    public Button completeBtn;

    private float startTime = 0;
    private float time = 0;
    private bool isStarted = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(isStarted)
        {
            time = Time.time - startTime;
        }
    }

    public void UpdateTimerText(string timer)
    {

        timerText.text = timer;
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isStarted = true;
    }

    public void OpenGameplayPanel()
    {
        panel.SetActive(true);

    }
}
