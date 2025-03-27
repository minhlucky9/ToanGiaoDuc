using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearningGame
{
    [CreateAssetMenu(menuName = "Scriptable Objects/LearningGame/Subject")]
    public class SubjectSO : ScriptableObject
    {
        public string subjectId;
        public Sprite subjectImage;
        public string subjectName;

        private void OnValidate()
        {
#if UNITY_EDITOR
            subjectId = this.name.ToLower();
#endif
        }
    }

}
