using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultUIController : MonoBehaviour
{
    public static GameResultUIController instance;
    public GameObject resultPanel;
    public Text titleText;
    public Text rightCountText;
    public Text wrongCountText;
    public Text errorCountText;
    public Text timeCountText;
    public Button continueBtn;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResultPanel(string title, GameResult result)
    {
        titleText.text = title;
        //
        rightCountText.text = result.nbCorrect.ToString();
        wrongCountText.text = result.nbWrong.ToString();
        errorCountText.text = result.nbError.ToString();
        timeCountText.text = result.completeTime.ToString();
        //
        resultPanel.SetActive(true);
    }
}
