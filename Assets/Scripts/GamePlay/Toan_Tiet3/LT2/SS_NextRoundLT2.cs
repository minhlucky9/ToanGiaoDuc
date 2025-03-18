using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_NextRoundLT2 : MonoBehaviour
{
    [HideInInspector] public int TotalRoundsIndex;
    [HideInInspector] public int curRoundIndex;
    public GameObject nextRoundButton;
    public List<GameObject> Rounds;
    public List<GameObject> IndivSprite;
    public Text curLvl;

    //connect with SS_ItemMoveLT1: returnToStartingPos, SS_AreaReceiver: OnDone, OnStartAgain

    //private void Start()
    //{
    //    curRoundIndex = 0;
    //    TotalRoundsIndex = Rounds.Count - 1;

    //    curLvl.text = (curRoundIndex + 1).ToString() + " / " + (TotalRoundsIndex + 1).ToString();
    //}

    //public void nextRound()
    //{
    //    if(curRoundIndex < TotalRoundsIndex)
    //    {
    //        curRoundIndex++;
    //        initNextRound();
    //    }

    //    curLvl.text = (curRoundIndex + 1).ToString() + " / " + (TotalRoundsIndex + 1).ToString();
    //}

    //private void initNextRound()
    //{
    //    nextRoundButton.SetActive(false);
    //    Rounds[curRoundIndex - 1].SetActive(false);
    //    Rounds[curRoundIndex].SetActive(true);

    //    foreach (GameObject sprite in IndivSprite)
    //    {
    //        sprite.GetComponent<SS_ItemMoveLT1>().returnToStartingPos();
    //    }

    //    GetComponent<SS_RandomLT1>().Run();
    //}
}
