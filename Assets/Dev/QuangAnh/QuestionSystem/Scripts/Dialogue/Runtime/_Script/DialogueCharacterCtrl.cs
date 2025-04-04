using MathCounting;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DialogueCustomSystem {

    [RequireComponent(typeof(AudioSource))]
    public class DialogueCharacterCtrl : NewMonobehavior {
        public AudioSource audioSource;
        public TextMeshProUGUI dialogueTextUI;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadAudioScoure();
            this.LoadDialogueTextUI();
        }


        protected override void Start() {
            Hide();
        }


        protected virtual void LoadDialogueTextUI() {
            if (this.dialogueTextUI != null) return;
            this.dialogueTextUI = this.GetComponentInChildren<TextMeshProUGUI>(); 
           /* Debug.Log(transform.name + ": LoadDialogueTextUI " + gameObject);*/
        }


        protected virtual void LoadAudioScoure() {
            if (this.audioSource != null) return;
            this.audioSource = this.GetComponent<AudioSource>(); 
            Debug.Log(transform.name + ": LoadAudioScoure " + gameObject);
        }


        public void Show() {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
        }

        public void Hide() {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }



    }
}
