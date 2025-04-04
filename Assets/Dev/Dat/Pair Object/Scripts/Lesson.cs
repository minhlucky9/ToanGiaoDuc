using System.Collections;
using System.Collections.Generic;
using BlockNumber;
using UnityEngine;
using UnityEngine.UI;

namespace PairObject
{
    public class Lesson : MonoBehaviour
    {
        [SerializeField] ChoiceSection choiceSection;
        [SerializeField] Button continueButton;

        List<DraggableObject> draggableObjects = new List<DraggableObject>();
        List<SnapPoint> snapPoints = new List<SnapPoint>();

        int mistakeCount;
        bool isLessonPaused;
        bool isPhaseTwo;
        float lessonTimer;

        private void Awake()
        {
            draggableObjects.AddRange(FindObjectsOfType<DraggableObject>());
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

        #region Button OnClick Events
        public void ToPhaseTwo()
        {
            choiceSection.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
            foreach (DraggableObject draggedObject in draggableObjects)
            {
                draggedObject.StopInteraction();
            }
            isPhaseTwo = true;
        }

        public void CorrectLesson()
        {
            if (!isPhaseTwo)
            {
                foreach (DraggableObject draggedObject in draggableObjects)
                {
                    draggedObject.AutoSnap();
                }
                continueButton.onClick.Invoke();
            }
            else choiceSection.ChooseCorrectChoice();
        }

        public void EndLesson()
        {
            isLessonPaused = true;

            int answerRight = 0;
            int totalQuestion = snapPoints.Count > draggableObjects.Count ?
                snapPoints.Count : draggableObjects.Count;

            for (int i = 0; i < snapPoints.Count; i++)
            {
                if (!snapPoints[i].IsCurrentDraggableObjectNull())
                {
                    answerRight++;
                    if (answerRight == totalQuestion) break;
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
        #endregion
    }

}
