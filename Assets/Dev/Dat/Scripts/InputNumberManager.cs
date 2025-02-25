using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DatDev
{
    public class InputNumberManager : MonoBehaviour
    {
        [SerializeField] GameObject numberInputUI;

        [SerializeField] Button[] allNumberButtons;
        [SerializeField] TMP_InputField choiceDisplay;
        [SerializeField] TextMeshProUGUI finalNumberDisplay;

        [SerializeField] Button finishButton;

        void Start()
        {
            InitNumberButtons();
            finishButton.onClick.AddListener(Finish);
        }

        void Finish()
        {
            finalNumberDisplay.text = choiceDisplay.text;
            numberInputUI.SetActive(false);
        }

        void InitNumberButtons()
        {
            foreach (Button button in allNumberButtons)
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    button.onClick.AddListener(() => setInputNumber(buttonText.text));
                }
            }
        }

        public void setInputNumber(string inputNumber)
        {
            choiceDisplay.text = inputNumber;
        }
    }

}


