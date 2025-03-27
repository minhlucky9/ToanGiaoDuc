using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogAnimation : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public GameObject canvas;

    Text dialogText;
    // Start is called before the first frame update
    private void Awake()
    {
        dialogText = canvas.GetComponentInChildren<Text>();
    }

    public IEnumerator GuideTalkAnimation(AudioClip audio, string text)
    {
        audioSource.clip = audio;
        audioSource.Play();
        animator.CrossFade("guide talk", 0.2f);

        if(dialogText)
        {
            dialogText.text = text;
        }
        
        canvas.SetActive(true);
        //
        while (audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        canvas.SetActive(false);
        animator.CrossFade("idle", 0.2f);

        yield return null;
    }

    private void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if(audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        } else
        {
            audioSource.playOnAwake = false;
        }
    }
}
