
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPart : MonoBehaviour
{
    [SerializeField]
    private List<ConversationScriptableObject> listConversation;
    public List<ConversationScriptableObject> GetListConversation()
    {
        return listConversation;
    }
    public void SetListConversation(List<ConversationScriptableObject> list)
    {
        listConversation = list;
    }
}
