using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlockNumber
{
    public class InputNumber : MonoBehaviour
    {
        [SerializeField] Lesson lesson;
        [SerializeField] TMP_InputField numberInputField;
        [SerializeField] Button[] allNumberButtons;

        AnswerBox currentAnswerBox;

        void Awake()
        {
            InitAllButtons();
            InputZero();
            gameObject.SetActive(false);
        }

        void InitAllButtons()
        {
            foreach (Button button in allNumberButtons)
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText == null) continue;

                string number = buttonText.text;
                button.onClick.AddListener(() => AddCharacter(number));
            }
        }
        void AddCharacter(string number)
        {
            if (numberInputField.text == "0") numberInputField.text = "";
            if(numberInputField.text.Length < 2) numberInputField.text += number;
        }

        public void SetCurrentAnswerBox(AnswerBox answerBox)
        {
            currentAnswerBox = answerBox;
            string input = answerBox.GetAnswerText();
            if(input == "") InputZero();
            else numberInputField.text = answerBox.GetAnswerText();

        }

        public void InputZero() { numberInputField.text = "0"; }

        public void SubmitInput()
        {
            if(currentAnswerBox == null) return;

            numberInputField.text = int.Parse(numberInputField.text).ToString();
            currentAnswerBox.SetAnswerText(numberInputField.text);
            
            bool isAnswerRight = currentAnswerBox.IsAnswerRight();
            lesson.CheckAnswerResult(isAnswerRight);

            gameObject.SetActive(false);
        }

    }

}
