using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiet6_inputPanel : MonoBehaviour
{
    [HideInInspector] public Tiet6_Input areaT6;
    public bool isNumPanel;
    public Text inputedText;
    int num = 0;
    AudioSource voiceSource;

    private void Start()
    {
        voiceSource=GetComponent<AudioSource>();
    }

    public void Input(string input)
    {
        num++;
        if(num <= 3)
        {
            if (isNumPanel)
            {
                inputedText.text += input;
            }
            else
            {
                inputedText.text += " " + input;
            }
        }
    }

    public void onDone()
    {
        areaT6.inputText.text = inputedText.text;
        //check right
        areaT6.CheckAnswer(inputedText.text);
        turnOff();
    }
    

    public void delete()
    {
        inputedText.text = "";
        num = 0;
    }
    public void turnOff()
    {
        delete();
        gameObject.SetActive(false);
    }

    public void turnOn(Vector3 Position)
    {
        gameObject.SetActive(true);
        GetComponent<RectTransform>().localPosition = Position;
    }

    public void wordSound(AudioClip voice)
    {
        voiceSource.clip = voice;
        voiceSource.Play();
    }
}
