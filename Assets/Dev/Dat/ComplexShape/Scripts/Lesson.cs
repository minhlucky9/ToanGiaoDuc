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

        public void PlusMistake()
        {
            mistakeCount++;
        }
    }
}
