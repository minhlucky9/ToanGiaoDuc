using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLessonButton : BaseSelectButton
{
    [SerializeField]
    private TietHoc tietHoc;
    [SerializeField]
    private Image imageIcon;
    [SerializeField]
    private Text titleText;

    public TietHoc GetTietHoc()
    {
        return tietHoc;
    }
    public void SetTietHoc(TietHoc bh)
    {
        tietHoc = bh;
    }
    public void SetImage(Sprite spr)
    {
        imageIcon.sprite = null;
        imageIcon.sprite = spr;
        imageIcon.SetNativeSize();
    }
    public void SetTitle(string text)
    {
        titleText.text = text;
    }
}
