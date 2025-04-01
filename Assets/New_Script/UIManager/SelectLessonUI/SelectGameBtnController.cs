using LearningGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectGameBtnController : MonoBehaviour
{
    public Button selectGameButton;
    public Text descriptionText;

    public void Init(string description, UnityAction action)
    {
        descriptionText.text = description;
        selectGameButton.onClick.AddListener(action);
    }
}
