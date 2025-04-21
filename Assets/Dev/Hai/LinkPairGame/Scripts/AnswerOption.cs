using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LinkPair
{
    public class AnswerOption : MonoBehaviour
    {
        // Dấu so sánh nhập vào
        public string inputComparator = "=";

        LinkPairManager linkPairManager;
        MultiQuestionsManager multiQuestionsManager;

        // Số lượng đối tượng đang xét
        [SerializeField]int countValidType1 = 0, countValidType2 = 0;
        // Tên đối tượng đang xét
        string nameValidType1 = "", nameValidType2 = "";

        // Text của đáp án
        TextMeshProUGUI text;

        // Ảnh của ô đáp án
        [SerializeField] Sprite originalImg, correctAnswerImg, wrongAnswerImg; 
        Image currentImg;

        Button btnComponent;

        // Start is called before the first frame update
        void Awake()
        {
            // Lấy các game manager
            linkPairManager = GameObject.FindGameObjectWithTag("LinkPairManager").GetComponent<LinkPairManager>();
            multiQuestionsManager = GameObject.FindGameObjectWithTag("LinkPairManager").GetComponent<MultiQuestionsManager>();

            text = GetComponentInChildren<TextMeshProUGUI>();

            // Gán onclick để check mọi đáp án
            btnComponent = GetComponent<Button>();
            btnComponent.onClick.AddListener(multiQuestionsManager.ShowAllAnswers);
            btnComponent.onClick.AddListener(CheckCorrectness);

            // Lấy img
            currentImg = GetComponent<Image>();
        }

        public void CheckCorrectness()
        {
            switch (inputComparator)
            {
                case ">":
                    multiQuestionsManager.UpdateAnswerAmount(countValidType1 > countValidType2);
                    break;
                case "<":
                    multiQuestionsManager.UpdateAnswerAmount(countValidType1 < countValidType2);
                    break;
                case "=":
                    multiQuestionsManager.UpdateAnswerAmount(countValidType1 == countValidType2);
                    break;
                default:
                    Debug.Log("Toán tử không hợp lệ!");
                    return;
            }

            btnComponent.interactable = false;
        }

        public void UpdateBtn()
        {
            switch (inputComparator)
            {
                case ">":
                    // Cập nhật background btn
                    if (countValidType1 > countValidType2)
                    {
                        currentImg.sprite = correctAnswerImg;
                        break;
                    }
                    else
                    {
                        currentImg.sprite = wrongAnswerImg;
                        break;
                    }
                case "<":
                    // Cập nhật background btn
                    if (countValidType1 < countValidType2)
                    {
                        currentImg.sprite = correctAnswerImg;
                        break;
                    }
                    else
                    {
                        currentImg.sprite = wrongAnswerImg;
                        break;
                    }
                case "=":
                    // Cập nhật background btn
                    if (countValidType1 == countValidType2)
                    {
                        currentImg.sprite = correctAnswerImg;
                        break;
                    }
                    else
                    {
                        currentImg.sprite = wrongAnswerImg;
                        break;
                    }
                default:
                    Debug.Log("Toán tử không hợp lệ!");
                    return;
            }

            btnComponent.interactable = false;
        }

        public void UpdateAnswer()
        {
            // Lấy số lượng đối tượng đang xét
            countValidType1 = linkPairManager.typeAmountsDictionary[linkPairManager.validType1];
            countValidType2 = linkPairManager.typeAmountsDictionary[linkPairManager.validType2];
            // Lấy tên đối tượng đang xét
            nameValidType1 = linkPairManager.typeNames[linkPairManager.validType1 - 1];
            nameValidType2 = linkPairManager.typeNames[linkPairManager.validType2 - 1];

            switch (inputComparator)
            {
                case ">":
                    text.text = "Số " + nameValidType1 + " NHIỀU HƠN số " + nameValidType2;
                    break;
                case "<":
                    text.text = "Số " + nameValidType1 + " ÍT HƠN số " + nameValidType2;
                    break;
                case "=":
                    text.text = "Số " + nameValidType1 + " BẰNG số " + nameValidType2;
                    break;
                default:
                    Debug.Log("Toán tử không hợp lệ!");
                    return;
            }
            currentImg.sprite = originalImg;

            btnComponent.interactable = true;
        }

        public void SetComparator(LinkPairManager.Comparators comp)
        {
            switch (comp)
            {
                case LinkPairManager.Comparators.Greater:
                    inputComparator = ">";
                    break;
                case LinkPairManager.Comparators.Less:
                    inputComparator = "<";
                    break;
                case LinkPairManager.Comparators.Equal:
                    inputComparator = "=";
                    break;
                default:
                    inputComparator = "=";
                    break;
            }
        }

    }

}
