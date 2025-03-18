using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ConversationPartEditorTool : MonoBehaviour
{
    [SerializeField]
    private float duration=0f;
#if UNITY_EDITOR
    [ContextMenu("SetAllDuration")]
    private void SetAllDuration()
    {
        Transform trans = transform;
        List<ConversationScriptableObject> listEdit = trans.GetComponentInChildren<ConversationPart>().GetListConversation();

        for(int i=0; i<listEdit.Count; i++)
        {
            listEdit[i].SetDuration(duration);
        }
        trans.GetComponentInChildren<ConversationPart>().SetListConversation(listEdit);
    }
    
#endif

}
