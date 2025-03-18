using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneResult : MonoBehaviour
{
    

    public void ButtonResult()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        GeneralController.instance.Active();
    }
}
