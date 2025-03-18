using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaLuyenTap3_DoneButton : MonoBehaviour
{
    [SerializeField]
    private AreaLuyenTap3 area;
    [SerializeField]
    private Sprite playSprite;
    [SerializeField]
    private Sprite doneSprite;
    private SpriteRenderer sprd;
    [SerializeField]
    private TextMeshPro text;
    private void Start()
    {
        area = GetComponentInParent<AreaLuyenTap3>();
        sprd = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        sprd.sprite = playSprite;
        text.gameObject.SetActive(true);
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.canPlay)
        {
            if (!area.IsDone())
            {
                area.OnDone();
                sprd.sprite = doneSprite;
                text.gameObject.SetActive(false);

            }
        }
    }
    public void OnStartAgain()
    {
        sprd.sprite = playSprite;
        text.text = "Xong";
        text.gameObject.SetActive(true);
    }
}
