using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader INSTANCE;
    public GameObject LoadingObject;
    public TextMeshPro text;
    private bool isLoading;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //private void Start()
    //{
    //    //StartCoroutine(LoadAsynchronously(1));
    //}
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        //yield return new WaitForSeconds(.95f);
        //Debug.Log("Here");
        //fillImage.fillAmount = 0;
        //LoadBar.SetActive(true);
        isLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //float fill = 0f;
        if(text != null)
        {
            StartCoroutine(Interface());
        }
        while (!operation.isDone)
        {
            //fill = operation.progress / 0.9f;
            //fillImage.fillAmount = fill;
            yield return new WaitForEndOfFrame();
        }
        isLoading = false;
        Debug.Log("yay");
    }
    IEnumerator Interface()
    {
        LoadingObject?.SetActive(true);
        while (isLoading)
        {
            text.text = ".";
            yield return new WaitForSeconds(0.15f);
            text.text = "..";
            yield return new WaitForSeconds(0.15f);
            text.text = "...";
            yield return new WaitForSeconds(0.15f);
            text.text = "....";
            yield return new WaitForSeconds(0.15f);
            text.text = ".....";
            yield return new WaitForSeconds(0.15f);
        }
        LoadingObject?.SetActive(false);
    }
}
