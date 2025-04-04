using System.Collections.Generic;
using UnityEngine;

namespace BlockNumber
{
    public class Lesson : MonoBehaviour
    {
        [SerializeField] ModelController modelController;

        bool isLessonPaused;
        float lessonTimer;

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

        public void EndLesson()
        {
            isLessonPaused = true;

            print($"LESSON RESULT: " +
                $"\nRight Answer: {MathCounting.ResultCtrl.Instance.GetCorrectCount()}" +
                $"\nRight Answer: {MathCounting.ResultCtrl.Instance.GetWrongCount()}" +
                $"\nMistake Count: {MathCounting.ResultCtrl.Instance.GetMistakeCount()}" +
                $"\nTotal Time: {GetLessonTime()}");
        }
    }
}