using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateManage : MonoBehaviour
{
    public static RateManage Instance;
    public RateLesson currentRateLesson { get; private set; }
    public void SetCurrentRateLesson(RateLesson rateLesson)
    {
        currentRateLesson = rateLesson;
    }
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //public string getOutPutTime(Achivement achievement)
    //{
    //    float time = achievement.GetTimeCount();
    //}
}