using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearningGame
{
    [CreateAssetMenu(menuName = "Scriptable Objects/LearningGame/Game")]
    public class LearningGameSO : ScriptableObject
    {
        public string gameId;
        public string topicId;
        public LearningGameType gameType;
        public MainGameStep[] gameSteps;

        private void OnValidate()
        {
#if UNITY_EDITOR
            gameId = this.name.ToLower();
#endif
        }
    }

    [Serializable]
    public class MainGameStep
    {
        public string sceneName;
        public List<DialogData> guideDialogs;
    }
}
