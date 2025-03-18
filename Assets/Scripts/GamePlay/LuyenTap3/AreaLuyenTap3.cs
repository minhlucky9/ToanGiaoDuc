using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaLuyenTap3 : MonoBehaviour
{
    private BoxCollider boxCollider;

    private Vector3 topLeft, topRight, botLeft, botRight, v3Tmp, defaultScale;

    [SerializeField]
    [Range(0, 100)]
    private int itemCountRequire;
    [SerializeField]
    private List<ItemMoveLuyenTap3> listItem;
    private Color normalColor;
    [SerializeField]
    private LuyenTap3LevelMap levelMap;
    [SerializeField]
    private Color onOverColor;
    private SpriteRenderer sprd;
    private ItemMoveLuyenTap3 itemCheckTemplate;
    private bool isItemOver = false;
    private Transform _transform;
    private bool isDone = false;
    private AreaLuyenTap3_DoneButton doneButton;
    private int m_wrongChoiceCount =0;
    public int wrongChoiceCount { get { return m_wrongChoiceCount; } }
    [SerializeField]
    private Animator reactionAnimator;
    private void Start()
    {
        InitVars();
    }
    private void InitVars()
    {
        isDone = false;
        _transform = transform;
        defaultScale = _transform.localScale;
        sprd = GetComponent<SpriteRenderer>();
        reactionAnimator = GetComponentInChildren<Animator>();
        normalColor = sprd.color;
        m_wrongChoiceCount = 0;
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
        listItem = new List<ItemMoveLuyenTap3>();
        levelMap = transform.parent.parent.parent.GetComponent<LuyenTap3LevelMap>();
        doneButton = GetComponentInChildren<AreaLuyenTap3_DoneButton>();
        try
        {
            transform.GetChild(1).GetComponent<TextMeshPro>().text = string.Empty + itemCountRequire;
        }
        catch { }
    }

    public void ReactionDirect(bool t)
    {
        if (t)
        {
            reactionAnimator?.Play("right");
        }
        else
        {
            reactionAnimator?.Play("wrong");
        }
    }

    public bool IsWinThisArea()
    {
        return listItem.Count == itemCountRequire;
    }
    public int GetTotalWrongItem()
    {
        return Mathf.Abs(itemCountRequire - listItem.Count);
    }
    public void CheckPositionOfItem(ItemMoveLuyenTap3 Item)
    {
        if (!GameManager.Instance.canPlay) return;
        if (IsIn(Item.getBounds()))
        {
            if (!listItem.Contains(Item))
            {
                if (isDone)
                {
                    OnStartAgain();
                }
                listItem.Add(Item);
                Item.SetAreaDad(this);
            }
        }
    }
    public void ReleaseItem(ItemMoveLuyenTap3 item)
    {
        if (listItem.Contains(item))
        {
            listItem.Remove(item);
            if (isDone)
            {
                OnStartAgain();
            }
        }
    }
    public bool IsDone()
    {
        return isDone;
    }

    public bool IsIn(Vector3[] listCheckPoint)
    {
        for (int i = 0; i < listCheckPoint.Length; i++)
        {
            if (!IsIn(listCheckPoint[i]))
            {
                //Debug.Log("false " + i);
                return false;
            }
            //Debug.Log("true " + i);
        }
        //Debug.Log("true all");
        return true;
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
        if(levelMap.itemManager.GetCurrentMovingItem() == null)
        {
            itemCheckTemplate = null;
        }
        else
        {
            itemCheckTemplate = levelMap.itemManager.GetCurrentMovingItem().GetComponent<ItemMoveLuyenTap3>();
        }
        if (itemCheckTemplate == null)
        {
            sprd.color = normalColor;
            isItemOver = false;
            //_transform.localScale = defaultScale;
            return;
        }
        else { }
        if (IsIn(itemCheckTemplate.getBounds()))
        {
            if (!isItemOver)
            {
                isItemOver = true;
                //v3Tmp = _transform.localScale;
                //v3Tmp = v3Tmp * 1.1f;
                //_transform.localScale = v3Tmp;
            }
            sprd.color = onOverColor;
        }
        else
        {
            if (isItemOver)
            {
                isItemOver = false;
                //_transform.localScale = defaultScale;
            }
            sprd.color = normalColor;
        }
    }
    public void OnDone()
    {
        isDone = true;
        if (!listItem.Count.Equals(itemCountRequire))
        {
            m_wrongChoiceCount++;
        }
        if (!GameManager.Instance.canShowReaction) return;
        if (listItem.Count.Equals(itemCountRequire))
        {
            TimeLineManage.INSTANCE.RightPlay();
            ReactionDirect(true);
        }
        else
        {
            TimeLineManage.INSTANCE.WrongPlay();
            ReactionDirect(false);
        }
    }
    public void OnStartAgain()
    {
        isDone = false;
        doneButton.OnStartAgain();
    }
    private void DoBlink()
    {

    }
}
