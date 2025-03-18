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


    private void Awake()
    {
        instance = this;
    }

    public void LoadCurrentGameStep()
    {
        MainGameStep[] steps = LearningGameManager.instance.gameData.gameSteps;
        SceneManager.LoadSceneAsync(steps[currentGameStep].sceneName, LoadSceneMode.Additive);
        onStepStart?.Invoke(steps[currentGameStep]);
    }
}
