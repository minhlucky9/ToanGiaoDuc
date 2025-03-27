using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionBtnController : MonoBehaviour
{
    public Image btnImage;
    public Text btnText;
    Button selectionBtn;

    private void Awake()
    {
        selectionBtn = GetComponent<Button>();
    }

    public void Init(Sprite image, string text, UnityAction onClickEvent)
    {
        btnImage.sprite = image;
        btnText.text = text;
        selectionBtn.onClick.AddListener(onClickEvent);
    }
}
