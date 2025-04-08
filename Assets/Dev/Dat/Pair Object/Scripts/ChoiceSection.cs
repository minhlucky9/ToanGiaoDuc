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
        [SerializeField] Sprite rightIcon;
        [SerializeField] Sprite wrongIcon;
        [SerializeField] Sprite chosenButtonSprite;
        [SerializeField] Sprite defaultButtonSprite;

        Button[] allChoiceButtons;
        bool isChoiceCorrect;

        void Awake()
        {
            allChoiceButtons = GetComponentsInChildren<Button>(true);
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
                    buttonImage.sprite = choiceButton == button ? chosenButtonSprite : defaultButtonSprite;
                }

                Image childImage = choiceButton.transform.Find("Check").GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.enabled = true;
                    childImage.sprite = choiceButton == rightChoiceButton ? rightIcon : wrongIcon;
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
