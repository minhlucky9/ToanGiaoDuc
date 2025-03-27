using MathCounting;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestionSystem {
    public class QuestionCtrl : Singleton<QuestionCtrl> {

        [Header("QuestionCtrl - Initialize")]

        [SerializeField] protected Transform questionHolder;
        [SerializeField] protected List<StudentCharacter> characters = new();
        [SerializeField] protected List<QuestionPrefab> questionPrefabList = new();


        [Header("Edit Questions - Restart for sync prefabs")]
        [SerializeField] private List<bool> CorrectQuestionCustom = new();
        [SerializeField] private List<bool> ActiveQuestionCustom = new(); 

        protected bool isCheckingAnswer = false;

        public bool IsCheckingAnswer => isCheckingAnswer;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadQuestionHolder();
            this.LoadQuestionPrefab();
            this.LoadStudents();
          
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
                this.ApplyQuestionActive();
            }
        }


        protected override void Start() {
            base.Start();
            this.ApplyQuestionStatus();
            this.ApplyQuestionActive();
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
            if (this.questionPrefabList.Count > 0 && !this.questionPrefabList.Any(x => x == null)) return;
            this.questionPrefabList = GetComponentsInChildren<QuestionPrefab>().ToList();
            this.SyncQuestionStatus();
            this.SyncQuestionActive();
        }

        protected virtual void SyncQuestionStatus() {
            CorrectQuestionCustom = questionPrefabList.Select(q => q.GetCorrect()).ToList();
        }

        protected virtual void SyncQuestionActive() {
            
            if (ActiveQuestionCustom.Count != questionPrefabList.Count) {
                ActiveQuestionCustom = questionPrefabList.Select(q => q.gameObject.activeSelf).ToList();
            }
        }

        public virtual void ApplyQuestionStatus() {
            for (int i = 0; i < questionPrefabList.Count; i++) {
                if (i < CorrectQuestionCustom.Count) {
                    questionPrefabList[i].SetCorrect(CorrectQuestionCustom[i]);
                }
            }
        }

        public virtual void ApplyQuestionActive() {
            for (int i = 0; i < questionPrefabList.Count; i++) {
                if (i < ActiveQuestionCustom.Count) {
                    questionPrefabList[i].gameObject.SetActive(ActiveQuestionCustom[i]);
                }
            }
        }

        public virtual void CheckingAnswer() {
            if (this.isCheckingAnswer) return;

            foreach (var question in this.questionPrefabList) {
                question.SetStatus();
            }

            this.isCheckingAnswer = true;
        }

        public virtual void Animation(bool isCorrect) {
            if (isCorrect)
                foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.RIGHT);
            else foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.WRONG);
        }

        
    }
}
