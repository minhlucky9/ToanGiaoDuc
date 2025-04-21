using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace LinkPair
{
    public class MultiQuestionsManager : MonoBehaviour
    {
        // Prefab để sinh câu trả lời
        public GameObject answerOption;
        // List câu trả lời
        public List<GameObject> answerOptions = new List<GameObject>();

        public Button btnShowQuestionPanel, btnChangePhase;
        GameObject questionPanel;

        LinkPairManager linkPairManager;

        // Danh sách các phép so sánh được phép (được chuyển từ LinkPairManager)
        LinkPairManager.Comparators[] allowedComparators;

        // Biến để log kết quả
        public int correctAnswer = 0, wrongAnswer = 0;

        // Start is called before the first frame update
        void Start()
        {
            linkPairManager = GetComponent<LinkPairManager>();
            questionPanel = GameObject.FindGameObjectWithTag("QuestionPanel");
            btnShowQuestionPanel = GameObject.FindGameObjectWithTag("BtnQuestionPanel").GetComponent<Button>();
            // answerOptions = GameObject.FindGameObjectsWithTag("AnswerOption");
            btnChangePhase = GameObject.FindGameObjectWithTag("BtnChangePhase").GetComponent<Button>();

            btnChangePhase.onClick.AddListener(ChangePhase);
            btnShowQuestionPanel.onClick.AddListener(ShowQuestionPanel);

            allowedComparators = linkPairManager.phases[linkPairManager.currentPhase].allowedComparators.GetFlags();

            // Tắt question panel đến khi nút được bấm
            questionPanel.SetActive(false);
        }

       

        // Hiện tất cả đáp án đúng, sai
        public void ShowAllAnswers()
        {
            foreach (GameObject answer in answerOptions)
            {
                AnswerOption answerScript = answer.GetComponent<AnswerOption>();
                answerScript.UpdateBtn();
            }
        }

        // Cập nhật tất cả đáp án theo pha tiếp theo
        public void UpdateAllAnswers()
        {
            foreach (GameObject answer in answerOptions)
            {
                Destroy(answer);
            }
            answerOptions.Clear();

            // Tạo mới các answer option
            for (int i = 0; i < allowedComparators.Length; i++)
            {
                GameObject newAnswer = Instantiate(answerOption, questionPanel.transform);
                AnswerOption answerScript = newAnswer.GetComponent<AnswerOption>();
                answerScript.SetComparator(allowedComparators[i]);
                answerScript.UpdateAnswer();

                answerOptions.Add(newAnswer);
            }
        }

        // Hiện bảng câu hỏi
        public void ShowQuestionPanel()
        {
            questionPanel.SetActive(true);

            // Delay UpdateAllAnswers để start() của các answer option chạy trước
            Invoke(nameof(UpdateAllAnswers), 0.01f);
        }

        public void ChangePhase()   
        {
            linkPairManager.ChangePhase();
            allowedComparators = linkPairManager.phases[linkPairManager.currentPhase].allowedComparators.GetFlags();
            linkPairManager.UpdateObjects();

            /*// Cập nhật câu hỏi theo pha mới
            if (questionPanel.activeInHierarchy)
            {
                UpdateAllAnswers();
            }*/
        }

        public void UpdateAnswerAmount(bool answer)
        {
            if (answer)
            {
                correctAnswer++;
            }
            else
            {
                wrongAnswer++;
            }
        }

        public void Submit()
        {
            // Đảm bảo số câu sai đủ
            if (correctAnswer + wrongAnswer != linkPairManager.phases.Length) {
                wrongAnswer = linkPairManager.phases.Length - correctAnswer;
            }
            Debug.Log("Multiple questions: correct: " + correctAnswer + ", wrong: " + wrongAnswer);
        }
    }
}
