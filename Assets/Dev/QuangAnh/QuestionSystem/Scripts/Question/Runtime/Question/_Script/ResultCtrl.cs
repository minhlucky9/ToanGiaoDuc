using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestionSystem {

    public class ResultQuestionCtrl : Singleton<ResultQuestionCtrl> {
       // To-Do
        [SerializeField] protected List<QuestionCtrl> prefabList = new();

        public int MistakeCount = 0;

        public int QuestionCount = 0;

        private int CorrectCount = 0;
        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadQuestionPrefabs();
        }

        protected virtual void LoadQuestionPrefabs() { 
            if(prefabList.Count > 0 && !prefabList.Any(x => x == null)) return;
            this.prefabList = GameObject.FindObjectsOfType<QuestionCtrl>().ToList();
            this.QuestionCount = prefabList.Count;
            Debug.Log(transform.name + ": LoadQuestionPrefabs: " + gameObject);
        }

        public virtual int GetCorrectCount() {

            CorrectCount = 0;

            foreach (QuestionCtrl prefab in prefabList) { 
                if(prefab.GetPlayerAnswer())  CorrectCount++;
            }

            return CorrectCount;
        }

        public virtual int GetWrongCount() {
            return this.QuestionCount - this.CorrectCount;
        }

        public virtual int GetMistakeCount() {
            return this.MistakeCount;
        }

        public virtual void ResultLog() {
            Debug.Log("Mistake count " + this.MistakeCount);
            Debug.Log("Correct count " + this.GetCorrectCount());
            Debug.Log("Wrong count " + this.GetWrongCount());
        }

    }



}
