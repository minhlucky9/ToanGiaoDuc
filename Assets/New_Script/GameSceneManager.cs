using Cinemachine;
using LearningGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    public int currentGameStep = 0;
    public UnityAction<MainGameStep> onStepStart;
    public UnityAction<GameResult> onStepEnd;

    //game mechanic
    GameResult result;
    public UnityEvent onAnswerCorrect;
    public UnityEvent onAnswerWrong;
    

    private void Awake()
    {
        instance = this;
        //
        onStepEnd += (result) =>
        {
            GameResultUIController.instance.ShowResultPanel(LearningGameManager.instance.currentTopicName, result);
        };
    }

    public void FinishGameStep(GameResult _result)
    {
        result = _result;
        onStepEnd?.Invoke(result);
    }

    public void LoadCurrentGameStep()
    {
        MainGameStep step = GetGameCurrentStep();
        if (step != null)
        {
            SceneManager.LoadSceneAsync(step.sceneName, LoadSceneMode.Additive);
            onStepStart?.Invoke(step);
        } else
        {
            Debug.LogError("Game steps list is empty!");
        }
            
    }

    public MainGameStep GetGameCurrentStep()
    {
        List<MainGameStep> steps = LearningGameManager.instance.gameData.gameSteps;
        
        if(steps.Count > 0)
        {
            return steps[currentGameStep];
        } else
        {
            return null;
        }
    }
}

public class GameResult
{
    public int nbCorrect;
    public int nbWrong;
    public int nbError;
    public string completeTime;

    public GameResult(int _nbCorrect, int _nbWrong, int _nbError, string _completeTime)
    {
        nbCorrect = _nbCorrect;
        nbWrong = _nbWrong;
        nbError = _nbError;
        completeTime = _completeTime;
    }

    public GameResult(int _nbCorrect, int _nbWrong, int _nbError, int _completeTime)
    {
        nbCorrect = _nbCorrect;
        nbWrong = _nbWrong;
        nbError = _nbError;
        completeTime = ConvertTime(_completeTime);
    }

    public string ConvertTime(int time)
    {
        int min = 0, sec = 0;
        string minString, secString;

        min = time / 60;
        sec = time % 60;

        if (sec < 10)
        {
            secString = "0" + sec + "''";
        }
        else
        {
            secString = sec + "''";
        }

        if (min < 10)
        {
            minString = "0" + min + "'";
        }
        else
        {
            minString = min + "'";
        }

        return minString + secString;
    }
}