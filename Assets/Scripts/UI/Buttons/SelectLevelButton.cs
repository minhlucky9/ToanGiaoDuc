
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    [SerializeField]
    private int id;
    private Image image;
    [SerializeField]
    Text description;
    [SerializeField]
    string prefixButton;
    [SerializeField]
    bool isUseCustomPrefix;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public int GetID()
    {
        return id;
    }
    public void SetId(int idz)
    {
        id = idz;
        if (isUseCustomPrefix)
        {
            description.text = prefixButton + " " + idz;
        }
    }
    public void SetImage(Sprite sprite)
    {
        if(image == null)
        {
            image = GetComponent<Image>(); 
        }
        image.sprite = sprite;
    }
}
