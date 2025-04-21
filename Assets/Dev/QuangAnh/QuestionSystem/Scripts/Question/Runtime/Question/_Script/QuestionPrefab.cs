using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace QuestionSystem {
    public class QuestionPrefab : BaseBtn {
        [Header("Question Inzialize")]
        [SerializeField] protected QuestionCtrl questionCtrl;
        [SerializeField] private ImageSwap image;
        [SerializeField] protected TextMeshProUGUI descriptionTMP;
        [SerializeField] protected Transform CorrectIcon;
        [SerializeField] protected Transform WrongIcon;

        [Header("Question Prefab")]

        public bool isCorrect = false;
        [SerializeField] protected string Description;


        private bool playerAnswer = false;
        public bool PlayerAnswer => playerAnswer;
        protected override void OnValidate() {   
            if(this.descriptionTMP != null) this.descriptionTMP.text = Description;
            if (this.questionCtrl != null) {
                this.questionCtrl.SyncQuestionStatus();
            } 
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImageSwap();
            this.LoadQuestionCtrl();
            this.LoadTextMeshPro();
            this.SettingButton();
            this.LoadIcon();
            this.Hide();
        }

        protected virtual void LoadIcon() {
            if (this.CorrectIcon != null && this.WrongIcon != null) return;
            this.CorrectIcon = transform.Find("CheckIcon").Find("V");
            this.WrongIcon = transform.Find("CheckIcon").Find("X");
            CorrectIcon.gameObject.SetActive(false);
            WrongIcon.gameObject.SetActive(false);
            Debug.Log(transform.name + ": LoadIcon: " + gameObject);
            
        }

        protected virtual void SettingButton() { 
            if(this.button == null) return;
            this.button.targetGraphic = GetComponentInChildren<Image>();
        }

        protected virtual void LoadQuestionCtrl() { 
            if (this.questionCtrl != null) return;
            this.questionCtrl = FindAnyObjectByType<QuestionCtrl>();
            Debug.Log(transform.name + ": LoadQuestionCtrl: " + gameObject);
        }

        protected virtual void LoadImageSwap() {
            if (this.image != null) return;
            this.image = GetComponentInChildren<ImageSwap>(); 
            Debug.Log(transform.name + ": LoadImageSwap: " + gameObject);
        }

        protected virtual void LoadTextMeshPro() {
            if (this.descriptionTMP != null) return;
            this.descriptionTMP = transform.Find("Description").GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadTextMeshPro: " + gameObject);
        }


        protected override void OnClick() {
            if(questionCtrl.DialogueController.isPlaying) return;
            if (questionCtrl.IsCheckingAnswer) return;
            /*Debug.Log(transform.name + ": OnClick: " + gameObject);  */
            this.SetCorrect();
            if(!isCorrect) ResultQuestionCtrl.Instance.MistakeCount++;
            StartCoroutine(questionCtrl.Animation(isCorrect));
            questionCtrl.CheckingAnswer(this);
        }

        public virtual void SetWrong() {
           image.WrongImage();
           this.WrongIcon.gameObject.SetActive(true);
           this.CorrectIcon.gameObject.SetActive(false);
           playerAnswer = false;
        }

        public virtual void SetCorrect() {
            image.CorrectImage();
            this.CorrectIcon.gameObject.SetActive(true);
            this.WrongIcon.gameObject.SetActive(false);
            playerAnswer = true;
        }

        public bool GetCorrect() { 
            return this.isCorrect;
        }

        public void SetCorrect(bool isCorrect) {
            this.isCorrect = isCorrect;
        }

        public void Show() { this.transform.gameObject.SetActive(true); }
        public void Hide() { this.transform.gameObject.SetActive(false); }

        public virtual void ResetStatus() {
            this.Hide();
            this.image.DefaultImage();
            this.CorrectIcon.gameObject.SetActive(false);
            this.WrongIcon.gameObject.SetActive(false);
        }

    }

}