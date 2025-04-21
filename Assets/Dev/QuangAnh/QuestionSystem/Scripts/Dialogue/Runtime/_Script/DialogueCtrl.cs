using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Playables;
using TMPro;
using System.Linq;
using System.Collections;
using QuestionSystem;


namespace DialogueCustomSystem {
    
    public class DialogueController : NewMonobehavior {

        [Header("Custom Dialogue Data")]
        [SerializeField] protected List<DialogueData> dialogueDataList = new();
        public bool isPlaying = false;

        protected override void Start() {
            // Only for testing
            
           /* StartDialogue();*/
        }


        //Start dialogue
        public virtual void StartDialogue() {
            isPlaying = true;
           /* this.RestartAllState();*/
            StartCoroutine(PlayDialogueSequence());
        }


        public IEnumerator PlayDialogueSequence() {
            if (dialogueDataList == null || dialogueDataList.Count == 0) {
                Debug.LogWarning("None dialogue data!");
                yield break;
            }
            for (int i = 0; i < dialogueDataList.Count; i++) {
                yield return StartCoroutine(dialogueDataList[i].StartDialogue());
            }

            isPlaying = false;
            Debug.Log("Done all Dialogue!");
        }

        public void RestartAllState() { 
            QuestionCtrl questionCtrl = GameObject.FindObjectOfType<QuestionCtrl>();
            if(questionCtrl != null) questionCtrl.ResetData();
        }


    }
}