using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace BlockNumber
{
    public class AnswerBox : MonoBehaviour
    {
        [SerializeField] Button answerButton;
        [SerializeField] TextMeshProUGUI answerText;
        [SerializeField] int rightAnswer;

        public void SetButtonAction(UnityAction action)
        {
            answerButton.onClick.AddListener(action);
        }
        public bool IsAnswerRight() { return int.Parse(answerText.text) == rightAnswer; }
        public void SetAnswerText(string answer) { answerText.text = answer; }
        public string GetAnswerText() { return answerText.text; }

    }

}
