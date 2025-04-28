using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComplexShape
{
    public class Lesson : MonoBehaviour
    {
        List<SnapShape> snapShapes = new List<SnapShape>();

        int mistakeCount;
        bool isLessonPaused;
        float lessonTimer;

        void Start()
        {
            snapShapes.AddRange(FindObjectsOfType<SnapShape>());
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

        public void EndLesson()
        {
            isLessonPaused = true;
            int rightAnswer = 0;

            foreach (SnapShape shape in snapShapes)
            {
                if (shape.IsFullyCombined()) rightAnswer++;
            }

            print($"LESSON RESULT: " +
                $"\nRight Answer: {rightAnswer}" +
                $"\nWrong Answer: {snapShapes.Count - rightAnswer}" +
                $"\nMistake Count: {mistakeCount}" +
                $"\nTotal Time: {GetLessonTime()}");
        }

        public void Correct()
        {
            StartCoroutine(CorrectCoroutine(0.1f));
        }

        IEnumerator CorrectCoroutine(float delayBetween)
        {
            foreach (SnapShape shape in snapShapes)
            {
                if(shape.IsFullyCombined()) continue;
                shape.SelfCorrect();
                yield return new WaitForSeconds(delayBetween);
            }
        }

        public void PlusMistake() => mistakeCount++;
    }
}
