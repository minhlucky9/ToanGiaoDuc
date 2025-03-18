using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManger : MonoBehaviour, IPanel
{
    public static ConversationManger Instance;
    private bool isReady = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private TextMeshProUGUI conversationText;
    [SerializeField]
    private Image avatarImage;
    [SerializeField]
    private Sprite[] listSpriteAvatar;
    private GameObject panelChild;
    private void Start()
    {
        isReady = true;
        if(conversationText == null)
        {
            try
            {
                conversationText = transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            }
            catch { }
            
        }
        if(avatarImage == null)
        {
            try
            {
                avatarImage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
            }
            catch { }
        }
        try
        {
            panelChild = transform.GetChild(0).gameObject;
        }
        catch { }
    }
    public void Active()
    {
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Deactive()
    {
        try
        {
            GetComponent<Animator>().Play("out");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public bool IsActive()
    {
        if (panelChild != null)
            return panelChild.activeSelf;
        return false;
    }
    public void StartCoversation(ConversationPart partOfConversation)
    {
        if (isReady)
        {
            StartCoroutine(ConversationIE(partOfConversation));
        }
    }
    private IEnumerator ConversationIE(ConversationPart partOfConversation)
    {
        isReady = false;
        List<ConversationScriptableObject> listConversation = partOfConversation.GetListConversation();
        for(int i=0; i<listConversation.Count; i++)
        {
            ChangeAvatar(listConversation[i].GetIdCharacter());
            UpdateConversationText(listConversation[i].GetContent());
            yield return new WaitForSeconds(listConversation[i].GetDuration());
        }
        isReady = true;
        UpdateConversationText("");
    }
    private void ChangeAvatar(int id)
    {
        if (id < 0 || id >= listSpriteAvatar.Length) return;
        avatarImage.sprite = listSpriteAvatar[id];
        avatarImage.SetNativeSize();
    }
    private void UpdateConversationText(string textToUpdate)
    {
        if (textToUpdate == null) return;
        conversationText.text = textToUpdate;
    }
}
