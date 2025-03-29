using System.Collections;
using System.Collections.Generic;
using BlockNumber;
using UnityEngine;

namespace PairObject
{
    public class Lesson : MonoBehaviour
    {
        [SerializeField] ChoiceSection choiceSection;

        //  Hai nhóm đối tượng
        List<DraggableObject> draggedObjects = new List<DraggableObject>();
        List<SnapPoint> snapPoints = new List<SnapPoint>();

        int mistakeCount;
        bool isLessonPaused;
        float lessonTimer;

        private void Awake()
        {
            draggedObjects.AddRange(FindObjectsOfType<DraggableObject>());
            snapPoints.AddRange(FindObjectsOfType<SnapPoint>());
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

        public void ToPhaseTwo()
        {
            foreach(DraggableObject draggedObject in draggedObjects)
            {
                draggedObject.StopInteraction();
            }
        }

        public void EndLesson()
        {
            isLessonPaused = true;

            int totalQuestion = snapPoints.Count;
            int answerRight = 0;

            if (snapPoints.Count > draggedObjects.Count) totalQuestion = draggedObjects.Count;
            for (int i = 0; i < snapPoints.Count; i++)
            {
                if (snapPoints[i].IsSnapped())
                {
                    answerRight++;
                    if(answerRight == totalQuestion) break;
                }
            }

            totalQuestion++;
            if (choiceSection.IsChoiceCorrect()) answerRight++;

            print($"LESSON RESULT: " +
                $"\nRight Answer: {answerRight}" +
                $"\nWrong Answer: {totalQuestion - answerRight}" +
                $"\nMistake Count: {mistakeCount}" +
                $"\nTotal Time: {GetLessonTime()}");
        }

    }

}
