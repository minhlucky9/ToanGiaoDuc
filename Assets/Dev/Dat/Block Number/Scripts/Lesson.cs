using System.Collections.Generic;
using UnityEngine;

namespace BlockNumber
{
    public class Lesson : MonoBehaviour
    {
        [SerializeField] InputNumber inputNumber;
        [SerializeField] ModelController modelController;
        private List<AnswerBox> answerBoxList;

        int mistakeCount;
        bool isLessonPaused;
        float lessonTimer;

        void Start()
        {
            answerBoxList = new List<AnswerBox>(FindObjectsOfType<AnswerBox>());

            foreach (AnswerBox box in answerBoxList)
            {
                box.SetButtonAction(() => {
                    inputNumber.gameObject.SetActive(true);
                    inputNumber.SetCurrentAnswerBox(box);
                });
            }
        }

        void Update()
        {
            if (isLessonPaused) return;
            lessonTimer += Time.deltaTime;
        }

        string GetLessonTime()
        {
            int minute = Mathf.FloorToInt(lessonTimer / 60);
            int second = Mathf.FloorToInt(lessonTimer - minute * 60);

            string res = minute.ToString() + "'";
            if (second < 10) res += "0";
            res += second.ToString() + "''";

            return res;
        }

        public void CheckAnswerResult(bool check) { if (!check) mistakeCount++; }

        public void EndLesson()
        {
            isLessonPaused = true;

            int answerRight = 0;
            foreach (AnswerBox box in answerBoxList)
            {
                if (box.IsAnswerRight()) answerRight++;
            }
            print($"LESSON RESULT: " +
                $"\nRight Answer: {answerRight}" +
                $"\nWrong Answer: {answerBoxList.Count - answerRight}" +
                $"\nMistake Count: {mistakeCount}" +
                $"\nTotal Time: {GetLessonTime()}");
        }
    }
}