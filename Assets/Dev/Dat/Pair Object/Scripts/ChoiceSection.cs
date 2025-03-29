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
        [SerializeField] Sprite chosenButtonSprite;
        [SerializeField] Sprite unChosenButtonSprite;

        bool isChoiceCorrect;

        void Awake()
        {
            foreach (Button choiceButton in allChoiceButtons)
            {
                choiceButton.onClick.AddListener(() => Choose(choiceButton));
            }
        }

        void Choose(Button button)
        {
            foreach (Button choiceButton in allChoiceButtons)
            {
                Image buttonImage = choiceButton.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = choiceButton == button ? chosenButtonSprite : unChosenButtonSprite;
                }
            }

            isChoiceCorrect = button == rightChoiceButton;
            lesson.CheckAnswerResult(isChoiceCorrect);
        }

        public bool IsChoiceCorrect() { return isChoiceCorrect; }

    }

}
