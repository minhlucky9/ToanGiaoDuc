using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_AreaReceiverLT2 : MonoBehaviour
{
    private BoxCollider boxCollider;

    private Vector3 topLeft, topRight, botLeft, botRight;
    public Text inputText;
    [SerializeField][Range(0, 100)] int itemCountRequire;
    [SerializeField] List<SS_ItemMoveLT2> listItem;
    [SerializeField] SoSanh_LT2_lvlMap levelMap;
    [SerializeField] Color onOverColor;
    private Color normalColor;
    private SpriteRenderer sprd;
    private int m_wrongChoiceCount = 0;
    public int wrongChoiceCount { get { return m_wrongChoiceCount; } }
    private SS_ItemMoveLT2 itemCheckTemplate;
    private bool isItemOver = false;
    private void Start()
    {
        InitVars();
    }
    private void InitVars()
    {
        sprd = GetComponent<SpriteRenderer>();
        normalColor = sprd.color;
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
            Vector2 size = Vector2.Scale(transform.lossyScale, boxCollider.size);
            Vector2 offset = Vector2.Scale(transform.lossyScale, boxCollider.center);
            topLeft = transform.position;
            topRight = topLeft;
            botLeft = topLeft;
            botRight = topLeft;
            topLeft.y = topLeft.y + size.y / 2 + offset.y;
            topRight.y = topRight.y + size.y / 2 + offset.y;
            topLeft.x = topLeft.x - size.x / 2 + offset.x; ;
            topRight.x = topRight.x + size.x / 2 + offset.x; ;
            botLeft.y = botLeft.y - size.y / 2 + offset.y;
            botRight.y = botRight.y - size.y / 2 + offset.y;
            botLeft.x = botLeft.x - size.x / 2 + offset.x;
            botRight.x = botRight.x + size.x / 2 + offset.x;
            Debug.Log("Init done!");
        }
        listItem = new List<SS_ItemMoveLT2>();
        levelMap = transform.GetComponentInParent<SoSanh_LT2_lvlMap>();
    }

    public void setItemCountRequire(int count)
    {
        itemCountRequire = count;
    }

    public void CheckPositionOfItem(SS_ItemMoveLT2 Item)
    {
        if (!GameManager.Instance.canPlay) return;
        if (IsIn(Item.getBounds()))
        {
            if (!listItem.Contains(Item))
            {
                listItem.Add(Item);
                inputText.text = listItem.Count.ToString();
                Item.SetAreaDad(this);
            }
        }
    }
    public void ReleaseItem(SS_ItemMoveLT2 item)
    {
        if (listItem.Contains(item))
        {
            listItem.Remove(item);
            inputText.text = listItem.Count.ToString();
        }
    }

    public bool IsIn(Vector3[] listCheckPoint)
    {
        for (int i = 0; i < listCheckPoint.Length; i++)
        {
            if (!IsIn(listCheckPoint[i]))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsWinThisArea()
    {
        bool isWin = false;
        switch (GetComponentInParent<SS_RandomLT2>().question)
        {
            case SS_RandomLT2.Question.Bigger:
                if(itemCountRequire < listItem.Count) { isWin = true; }
                break;
            case SS_RandomLT2.Question.Equal:
                if (itemCountRequire == listItem.Count) { isWin = true; }
                break;
            case SS_RandomLT2.Question.Smaller:
                if (itemCountRequire > listItem.Count) { isWin = true; }
                break;
        }
        return isWin;
    }

    public bool IsIn(Vector3 point)
    {
        if (boxCollider == null) return false;
        Debug.DrawLine(point, topLeft, Color.green, 3f);
        if (point.x >= topLeft.x && point.x <= topRight.x && point.y >= botLeft.y && point.y <= topRight.y) return true;
        return false;
    }
    private void OnMouseExit()
    {
        sprd.color = normalColor;
    }

    private void Update()
    {
        if (levelMap.itemManager.GetCurrentMovingItem() == null)
        {
            itemCheckTemplate = null;
        }
        else
        {
            itemCheckTemplate = levelMap.itemManager.GetCurrentMovingItem().GetComponent<SS_ItemMoveLT2>();
        }

        if (itemCheckTemplate == null)
        {
            sprd.color = normalColor;
            isItemOver = false;
            return;
        }

        if (IsIn(itemCheckTemplate.getBounds()))
        {
            if (!isItemOver)
            {
                isItemOver = true;
            }
            sprd.color = onOverColor;
        }
        else
        {
            if (isItemOver)
            {
                isItemOver = false;
            }
            sprd.color = normalColor;
        }
    }
}
