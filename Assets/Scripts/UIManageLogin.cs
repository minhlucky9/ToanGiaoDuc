using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManageLogin : MonoBehaviour
{
    public GameObject panelPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonQuitGame()
    {
        panelPopup.SetActive(true);
        panelPopup.GetComponentInChildren<Text>().text = "Bạn có muốn thoát?";
    }

    public void ClosePopup()
    {
        panelPopup.SetActive(false);    
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
