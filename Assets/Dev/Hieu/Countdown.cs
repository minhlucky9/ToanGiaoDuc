using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace MathConnection
{
    public class CountdownController : MonoBehaviour
    {
        public TMP_Text countdownText;
        public Image greenButton;
        public Image countdownPanel;
        public Image clockImage;
        public Image number3Image;
        public Image number2Image;
        public Image number1Image;
        public Image batdauImage;
        public float delayBetweenSteps = 1f;

        private void Start()
        {
            greenButton.gameObject.SetActive(false);
            countdownText.gameObject.SetActive(false);
            countdownPanel.gameObject.SetActive(false);
            clockImage.gameObject.SetActive(false);
            number3Image.gameObject.SetActive(false);
            number2Image.gameObject.SetActive(false);
            number1Image.gameObject.SetActive(false);
            batdauImage.gameObject.SetActive(false);

            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            greenButton.gameObject.SetActive(true);
            countdownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBetweenSteps);

            greenButton.gameObject.SetActive(false);
            countdownText.gameObject.SetActive(false);
            countdownPanel.gameObject.SetActive(true);
            clockImage.gameObject.SetActive(true);
            number3Image.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBetweenSteps);

            number3Image.gameObject.SetActive(false);
            number2Image.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBetweenSteps);

            number2Image.gameObject.SetActive(false);
            number1Image.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBetweenSteps);

            countdownPanel.gameObject.SetActive(false);
            clockImage.gameObject.SetActive(false);
            number1Image.gameObject.SetActive(false);
            batdauImage.gameObject.SetActive(true);
            greenButton.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBetweenSteps);

            batdauImage.gameObject.SetActive(false);
            greenButton.gameObject.SetActive(false);
        }
    }
}