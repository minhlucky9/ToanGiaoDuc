using UnityEngine;
[System.Serializable]
public class ConversationScriptableObject
{
    [SerializeField]
    [Range(0, 2)]
    private int idCharacter = 0;

    [SerializeField]
    [Range(0, 20)]
    private float duration = 1f;
    [SerializeField]
    public bool isNextImediately = false;

    [TextArea]
    [SerializeField]
    private string conversationContent;

    public string GetContent()
    {
        return conversationContent;
    }

    public int GetIdCharacter()
    {
        return idCharacter;
    }
    public float GetDuration()
    {
        return duration;
    }
    public void SetContent(string content)
    {
        conversationContent = content;
    }

    public void SetIdCharacter(int id)
    {
        idCharacter = id;
    }
    public void SetDuration(float dur)
    {
        duration = dur;
    }
}
