    using DialogueCustomSystem;
using MathCounting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestionSystem {
    public class QuestionCtrl : Singleton<QuestionCtrl> {

        [Header("QuestionCtrl - Initialize")]

        [SerializeField] protected Transform questionHolder;
        [SerializeField] protected List<StudentCharacter> characters = new();
        [SerializeField] protected DialogueController dialogueController;
        [SerializeField] protected List<QuestionPrefab> questionPrefabList;

        public DialogueController DialogueController => dialogueController;

        [Header("Edit Questions - Restart for sync prefabs")]
        [SerializeField] private List<bool> CorrectQuestionCustom = new();


        public bool isCheckingAnswer = false;

        public bool IsCheckingAnswer => isCheckingAnswer;

        private int numberCorrectAnswers = 0;
        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadQuestionHolder();
            this.LoadQuestionPrefab();
            this.LoadStudents();
            this.LoadDialogueCtrl();
        }


        protected virtual void LoadDialogueCtrl() { 
            if (this.dialogueController != null) return;
            this.dialogueController = FindAnyObjectByType<DialogueController>();
            Debug.Log(transform.name + ": LoadDialogueCtrl: " + gameObject);
        } 

        protected virtual void LoadStudents() {
            if (this.characters.Count > 0 && !this.characters.Any(c => c == null)) return;
            this.characters = new List<StudentCharacter>(FindObjectsOfType<StudentCharacter>());
            Debug.Log(transform.name + ": LoadStudents " + gameObject);
        }

        protected override void OnValidate() {
            if (!Application.isPlaying) {
                if (questionPrefabList == null || questionPrefabList.Count == 0) return;
                this.ApplyQuestionStatus();
            }
        }

        protected override void Start() { 
            this.ApplyQuestionStatus();
        }



        protected virtual void LoadQuestionHolder() {
            if (this.questionHolder != null) return;
            this.questionHolder = transform.Find("QuestionsHolder");
            if (this.questionHolder == null) {
                this.questionHolder = new GameObject("QuestionsHolder").transform;
                this.questionHolder.SetParent(transform, false);
            }
        }

        protected virtual void LoadQuestionPrefab() {
            if (this.questionPrefabList.Count > 0) return;
            this.questionPrefabList = GetComponentsInChildren<QuestionPrefab>().ToList();
            SyncQuestionStatus();
        }

        public virtual void SyncQuestionStatus() {

            CorrectQuestionCustom = questionPrefabList.Select(q => q.GetCorrect()).ToList();

            numberCorrectAnswers = questionPrefabList.Count(q => q.GetCorrect());
        }


        public virtual void ApplyQuestionStatus() {
            for (int i = 0; i < questionPrefabList.Count; i++) {
                if (i < CorrectQuestionCustom.Count) {
                    questionPrefabList[i].SetCorrect(CorrectQuestionCustom[i]);
                }
            }
        }


        public virtual void CheckingAnswer(QuestionPrefab clickedPrefab) {
            
            if (this.isCheckingAnswer) return;
           /* Debug.Log(transform.name + ": CheckingAnswer: " + gameObject);*/

            foreach (var question in this.questionPrefabList) {
                if (question == clickedPrefab) continue;
                question.SetWrong();
            }

            
        }

        public IEnumerator Animation( bool isCorrect ) {
            string animationType = isCorrect ? ConstAnimator.RIGHT : ConstAnimator.WRONG;


            foreach (StudentCharacter character in this.characters) {
                character.ChangeAnimation(animationType);
            }

            yield return new WaitForSeconds(1.2f);

            foreach (StudentCharacter character in this.characters) {
                character.ChangeAnimation(ConstAnimator.IDLE);
            }


            yield return new WaitForSeconds(0.2f);
        }

        public virtual void ResetData() {
            this.isCheckingAnswer = false;
            foreach (var question in this.questionPrefabList) {
                question.ResetStatus();
            }
        }

        internal bool GetPlayerAnswer() {

            foreach (QuestionPrefab question in this.questionPrefabList) {
                if (question.PlayerAnswer == true && question.isCorrect) return true;
            }

            return false;
        }
    }
}
