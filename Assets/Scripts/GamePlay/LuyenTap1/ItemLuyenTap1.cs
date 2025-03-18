using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemLuyenTap1: MonoBehaviour
{
    [SerializeField]
    private int id;
    public string nameLevel;
    [SerializeField]
    private int valueRequire;
    public bool isLV5Check = false;
    [SerializeField]
    private int value = -1;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private Color colorTextDefault;
    [SerializeField]
    private Color colorTextValue;
    public bool canEdit = true;
    public bool isDefaultTrue = false;
    [SerializeField]
    private bool canZoom = false;
    public Transform defaultPos;
    public Vector3 localPositionToZoomIn;
    public float zoomTime = 0.15f;
    public float quitZoomTime = 0.1f;
    public int defaultOrder= 21;
    public int selectedOrder=23;
    private int m_wrongChoiceCount = 0;
    public int wrongChoiceCount { get { return m_wrongChoiceCount; } }
    [SerializeField]
    private SpriteRenderer sprd;
    [SerializeField]
    private SpriteRenderer sprd_child;
    private Vector3 defaultScale;
    public Vector3 zoomInScale;
    private void Start()
    {
        if(text == null)
        {
            text = transform.GetComponentInChildren<TextMeshPro>();
            text.color = colorTextDefault;
            text.text = "";
        }
        defaultScale = transform.localScale;
        if (isDefaultTrue)
        {
            UpdateValue(valueRequire);
        }
        if(sprd == null)
        {
            sprd = GetComponent<SpriteRenderer>();
            sprd_child = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    }
    public void SetRequiredValue(int val)
    {
        valueRequire = val;
    }
    public int GetRequiredValue()
    {
        return valueRequire;
    }
    public int GetValue()
    {
        return value;
    }
    public void UpdateValue(int val)
    {
        Debug.Log(name + " update value: " + val);
        value = val;
        text.color = colorTextValue;
        text.text = string.Empty + val;
        if (GameManager.Instance.canShowReaction)
        {
            if (IsRight())
            {
                TimeLineManage.INSTANCE.RightPlay();
            }
            else
            {
                TimeLineManage.INSTANCE.WrongPlay();
            }
        }
        if (!IsRight())
        {
            m_wrongChoiceCount++;
        }
        if (canZoom)
        {
            DoCompleteZoom();
        }
    }
    public void SetDefaultTrue()
    {
        value = valueRequire;
        if(text == null)
        {
            text = transform.GetComponentInChildren<TextMeshPro>();
        }
        text.color = colorTextValue;
        text.text = string.Empty + valueRequire;
    }
    public void SetDefaultFalse()
    {
        value = 0;
        if (text == null)
        {
            text = transform.GetComponentInChildren<TextMeshPro>();
        }
        text.color = colorTextDefault;
        text.text = "";
    }
    public bool IsRight()
    {
        return valueRequire.Equals(value);
    }
    private void DoZoom()
    {
        if (!canZoom) return;
        sprd.sortingOrder = selectedOrder;
        sprd_child.sortingOrder = selectedOrder + 1;
        BlackWindow.Instance.On();
        StartCoroutine(UltilityCustom.MoveAndScaleLocalOverSecond(transform, localPositionToZoomIn, zoomInScale, zoomTime));
        //StartCoroutine(UltilityCustom.ScaleOverSecond(transform, zoomInScale, zoomTime));
    }
    private void DoCompleteZoom()
    {
        if (!canZoom) return;
        BlackWindow.Instance.Off();
        Debug.Log(defaultPos.localPosition);
        //StartCoroutine(UltilityCustom.MoveOverSecondIE(transform, defaultPos.position, quitZoomTime));
        //StartCoroutine(UltilityCustom.ScaleOverSecond(transform, defaultScale, zoomTime));
        StartCoroutine(UltilityCustom.MoveAndScaleOverSecond(transform, defaultPos.position, defaultScale, zoomTime));
        sprd.sortingOrder = defaultOrder;
        sprd_child.sortingOrder = defaultOrder + 1;
    }
    private void OnMouseDown()
    {
        //if (InputCallBackPopupPanel.INSTANCE.IsActive() || !GameManager.Instance.canPlay || !canEdit) return;
        Debug.Log("Call from item button");
        if (canZoom)
        {
            DoZoom();
        }
        InputCallBackPopupPanel.INSTANCE.Active(value, UpdateValue, DoCompleteZoom);
    }
    
}
