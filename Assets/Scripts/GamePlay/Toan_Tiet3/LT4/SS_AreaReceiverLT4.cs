using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_AreaReceiverLT4 : MonoBehaviour
{
    private BoxCollider boxCollider;

    private Vector3 topLeft, topRight, botLeft, botRight;

    [SerializeField] Transform endPos;
    public List<SS_ItemMoveLT4> listItem;
    [SerializeField] SoSanh_LT4_lvlMap levelMap;
    private int m_wrongChoiceCount = 0;
    public int wrongChoiceCount { get { return m_wrongChoiceCount; } }
    private SS_ItemMoveLT4 itemCheckTemplate;
    private bool isItemOver = false;
    private void Start()
    {
        InitVars();
    }
    private void InitVars()
    {
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
        listItem = new List<SS_ItemMoveLT4>();
        levelMap = transform.GetComponentInParent<SoSanh_LT4_lvlMap>();
    }

    public void CheckPositionOfItem(SS_ItemMoveLT4 Item)
    {
        if (!GameManager.Instance.canPlay) return;
        if (IsIn(Item.getBounds()))
        {
            Item.transform.position = endPos.position;
            if (!listItem.Contains(Item))
            {
                Item.SetAreaDad(this);

                if (listItem.Count > 0)
                {
                    SS_ItemMoveLT4_Restriction A = listItem[0].GetComponent<SS_ItemMoveLT4_Restriction>();
                    SS_ItemMoveLT4_Restriction B = Item.GetComponent<SS_ItemMoveLT4_Restriction>();
                    //doi vi tri a sang b
                    A.transform.position = B.startPos;
                    B.changeStartPos(A.startPos);
                    A.changeStartPos(A.transform.position);
                    listItem.Remove(listItem[0]);
                }
                listItem.Add(Item);
                OnDone();
            }
        }
    }
    public void ReleaseItem(SS_ItemMoveLT4 item)
    {
        if (listItem.Contains(item))
        {
            item.transform.position = item.GetComponent<SS_ItemMoveLT4_Restriction>().startPos;
            listItem.Remove(item);
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

    public bool IsIn(Vector3 point)
    {
        if (boxCollider == null) return false;
        Debug.DrawLine(point, topLeft, Color.green, 3f);
        if (point.x >= topLeft.x && point.x <= topRight.x && point.y >= botLeft.y && point.y <= topRight.y) return true;
        return false;
    }

    private void Update()
    {
        if (levelMap.itemManager.GetCurrentMovingItem() == null)
        {
            itemCheckTemplate = null;
        }
        else
        {
            itemCheckTemplate = levelMap.itemManager.GetCurrentMovingItem().GetComponent<SS_ItemMoveLT4>();
        }

        if (itemCheckTemplate == null)
        {
            isItemOver = false;
            return;
        }

        if (IsIn(itemCheckTemplate.getBounds()))
        {
            if (!isItemOver)
            {
                isItemOver = true;
            }
        }
        else
        {
            if (isItemOver)
            {
                isItemOver = false;
            }
        }

    }

    public void OnDone()
    {
        bool isRight = true;
        //Xử lý wrongChoiceCount here
        if (listItem[0].itemValue != GetComponentInParent<SS_RandomLT4>().NumberNeeded[0])
        {
            m_wrongChoiceCount++;
            isRight = false;
        }
        if (GameManager.Instance.canShowReaction)
        {
            if (isRight)
            {
                TimeLineManage.INSTANCE.RightPlay();
            }
            else
            {
                TimeLineManage.INSTANCE.WrongPlay();
            }
        }
    }
}
