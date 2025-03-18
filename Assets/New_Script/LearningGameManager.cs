using LearningGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LearningGameType { KhamPha, LuyenTap, TuKiemTra };

public class LearningGameManager : MonoBehaviour
{
    public static LearningGameManager instance;
    public LearningGameSO gameData;
    public string currentAdditiveSceneName = "";
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadSceneAdditive("IntroScene");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            LoadSceneAdditive(gameData.gameType.ToString());
        }
    }

    public void LoadSceneAdditive(string targetScene)
    {
        if(currentAdditiveSceneName != "")
        {
            SceneManager.UnloadSceneAsync(currentAdditiveSceneName);
        }

        SceneManager.LoadScene(targetScene, LoadSceneMode.Additive);
        currentAdditiveSceneName = targetScene;
    }
}
