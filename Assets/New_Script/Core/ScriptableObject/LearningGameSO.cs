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
        [Range(1, 3)]
        public int difficulty = 1;
        public List<MainGameStep> gameSteps = new List<MainGameStep>();

        public string referenceId => topicId + "_" + gameId;

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
