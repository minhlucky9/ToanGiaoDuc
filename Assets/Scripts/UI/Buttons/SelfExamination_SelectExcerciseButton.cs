using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfExamination_SelectExcerciseButton : BaseSelectButtonInt
{
    private Image buttonImage;
    private Text textButton;
    public string prefix = "Bài tập";
    private void Start()
    {
        buttonImage = GetComponent<Image>();
        textButton = GetComponentInChildren<Text>();
    }
    
    public void SetImage(Sprite sp)
    {
        if(buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }
        buttonImage.sprite = null;
        buttonImage.sprite = sp;
    }
    public void UpdateText()
    {
        if (textButton == null)
        {
            textButton = GetComponentInChildren<Text>();
        }
        textButton.text = prefix + " " + GetId();
    }
}
