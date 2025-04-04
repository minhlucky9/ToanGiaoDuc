using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PairObject
{
    public class ChoiceSection : MonoBehaviour
    {
        [SerializeField] Lesson lesson;

        [SerializeField] Button rightChoiceButton;
        [SerializeField] Button[] allChoiceButtons;
        [SerializeField] Sprite greenSprite;
        [SerializeField] Sprite redSprite;

        Color chosenColor = new Color(0.5882f, 0.7843f, 0.6353f);
        bool isChoiceCorrect;

        void Awake()
        {
            foreach (Button choiceButton in allChoiceButtons)
            {
                choiceButton.onClick.AddListener(() => Choose(choiceButton));
            }
            gameObject.SetActive(false);
        }

        void Choose(Button button)
        {
            isChoiceCorrect = button == rightChoiceButton;

            foreach (Button choiceButton in allChoiceButtons)
            {
                Image buttonImage = choiceButton.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = choiceButton == button ? chosenColor : Color.white;
                }

                Image childImage = choiceButton.transform.Find("Check").GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.enabled = true;
                    childImage.sprite = choiceButton == rightChoiceButton ? greenSprite : redSprite;
                }
            }

            lesson.CheckAnswerResult(isChoiceCorrect);
        }

        public bool IsChoiceCorrect() { return isChoiceCorrect; }

        public void ChooseCorrectChoice()
        {
            Choose(rightChoiceButton);
        }
    }
}
