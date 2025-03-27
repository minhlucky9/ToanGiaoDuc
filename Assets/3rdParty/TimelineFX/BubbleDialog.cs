using TMPro;
using UnityEngine;

public class BubbleDialog : MonoBehaviour
{
    public TMP_Text textUI;
    public AudioSource audioSource;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        textUI = GetComponentInChildren<TMP_Text>();

        if (textUI == null)
        {
            GameObject go = new GameObject("Dialog Text");
            go.transform.parent = transform;
            textUI = go.AddComponent<TextMeshProUGUI>();
        }

        if (audioSource == null)
        {
            GameObject go = new GameObject("Audio Source");
            go.transform.parent = transform;
            audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }
}