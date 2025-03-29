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

        protected override void OnValidate() {   
            if(this.descriptionTMP == null) return;
            this.descriptionTMP.text = Description;
        }


        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImageSwap();
            this.LoadQuestionCtrl();
            this.LoadTextMeshPro();
            this.SettingButton();
            this.LoadIcon();
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
            this.questionCtrl = GameObject.FindAnyObjectByType<QuestionCtrl>();
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
            if(questionCtrl.IsCheckingAnswer) return;
            questionCtrl.Animation(isCorrect);
            questionCtrl.CheckingAnswer();
        }

        public virtual void SetStatus() {
            if (isCorrect) {
                image.CorrectImage();
                this.CorrectIcon.gameObject.SetActive(true);
            } else {
                image.WrongImage();
                this.WrongIcon.gameObject.SetActive(true);
            }
            return;
        }

        public bool GetCorrect() { 
            return this.isCorrect;
        }

        public void SetCorrect(bool isCorrect) {
            this.isCorrect = isCorrect;
        }



    }

}