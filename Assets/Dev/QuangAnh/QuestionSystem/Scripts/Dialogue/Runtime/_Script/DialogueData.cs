using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using MathCounting;
using QuestionSystem;

namespace DialogueCustomSystem {
    [System.Serializable]
    public class DialogueData
    {
        public string dialogueText;
        public Transform character;
        private DialogueCharacterCtrl dialogueCtrl;
        private Animator animator;
        public AudioClip audioClip;
        public AnimationName animationClip;
        public List<GameObject> objectsQuestion = new List<GameObject>();

        protected void LoadCharacterAttibutes() { 

            this.animator = character.GetComponentInChildren<Animator>();
            this.dialogueCtrl = character.GetComponentInChildren<DialogueCharacterCtrl>();

        }


        public IEnumerator StartDialogue() {

            this.LoadCharacterAttibutes();

            if (animator == null ) {
                Debug.LogWarning("Missing Animator !");
                yield break;
            }

            if (dialogueCtrl != null) {
                dialogueCtrl.Show();
                if(dialogueCtrl.dialogueTextUI != null) dialogueCtrl.dialogueTextUI.text = dialogueText;
            }
            animator.CrossFade(animationClip.GetDescription(), 0.2f);


            // Play audio if available
            AudioSource audioSource = character.GetComponentInChildren<AudioSource>();
            if (audioSource && audioClip) {
                audioSource.clip = audioClip;
                audioSource.Play();

                // Wait until the audio finishes playing
                yield return new WaitForSeconds(audioClip.length);
            } else {
                // Wait for animation duration if no audio is available
                Debug.Log("Not have audioSource");
                AnimationClip clip = GetAnimationClip(animationClip.GetDescription());
                if (clip != null) {
                    yield return new WaitForSeconds(clip.length);
                } else {
                    Debug.LogWarning("Animation clip not found, using default wait time.");
                    yield return new WaitForSeconds(2f); 
                }

            }
           

            foreach (var item in objectsQuestion) {
                item.SetActive(true);
            }

            if (dialogueCtrl != null) {

                dialogueCtrl.Hide();
                if (dialogueCtrl.dialogueTextUI != null) dialogueCtrl.dialogueTextUI.text = "";
            }

            animator.CrossFade(ConstAnimator.IDLE, 0.2f);

            yield return new WaitForSeconds(0.2f);

        }

        private AnimationClip GetAnimationClip( string animationName ) {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips) {
                if (clip.name == animationName) {
                    return clip;
                }
            }
            return null;
        }

        public virtual void ResetState() {
            foreach (var item in objectsQuestion) {
                item.SetActive(false);
                if(item.GetComponent<QuestionPrefab>() != null) item.GetComponent<QuestionPrefab>().ResetStatus();
            }
        }
    }

}
