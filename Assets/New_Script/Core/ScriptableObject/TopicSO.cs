using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearningGame
{
    [CreateAssetMenu(menuName = "Scriptable Objects/LearningGame/Topic")]
    public class TopicSO : ScriptableObject
    {
        public string topicId;
        public string subjectId;
        public Sprite topicImage;
        public string topicName;

        public string referenceId => subjectId + "_" + topicId; 

        private void OnValidate()
        {
#if UNITY_EDITOR
            topicId = this.name;
#endif
        }
    }
}


