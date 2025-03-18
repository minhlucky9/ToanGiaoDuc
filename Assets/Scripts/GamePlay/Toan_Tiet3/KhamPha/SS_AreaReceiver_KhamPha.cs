using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_AreaReceiver_KhamPha : MonoBehaviour
{
    private BoxCollider boxCollider;

    private Vector3 topLeft, topRight, botLeft, botRight;
    [SerializeField] Transform endPos;
    public List<SS_ItemMove_KhamPha> listItem;
    [SerializeField] SoSanh_KhamPha_lvlmap levelMap;
    private int m_wrongChoiceCount = 0;
    public int wrongChoiceCount { get { return m_wrongChoiceCount; } }
    private SS_ItemMoveLT5 itemCheckTemplate;
    private bool isItemOver = false;
    public bool firstItemMove = true;
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
        listItem = new List<SS_ItemMove_KhamPha>();
        levelMap = transform.GetComponentInParent<SoSanh_KhamPha_lvlmap>();
    }

    public void CheckPositionOfItem(SS_ItemMove_KhamPha Item)
    {
        if (!GameManager.Instance.canPlay) return;
        if (IsIn(Item.getBounds()))
        {
            Item.transform.position = endPos.position;
            if (!listItem.Contains(Item))
            {
                SS_AreaReceiver_KhamPha ItemOldDad = Item.GetAreaDad();
                Item.SetAreaDad(this);

                if (listItem.Count > 0)
                {
                    SS_ItemMove_KhamPha_restriction A = listItem[0].GetComponent<SS_ItemMove_KhamPha_restriction>();      //vi tri goc
                    SS_ItemMove_KhamPha_restriction B = Item.GetComponent<SS_ItemMove_KhamPha_restriction>();             //bi keo
                    //doi vi tri a sang b
                    A.transform.position = B.startPos;
                    B.changeStartPos(A.startPos);
                    A.changeStartPos(A.transform.position);
                    listItem.RemoveAt(0);

                    //neu vat bi keo co trong area
                    if (B.lT5 != null)
                    {
                        //add a vao area ma b thuoc vao
                        B.lT5.listItem.Add(A.GetComponent<SS_ItemMove_KhamPha>());
                        //doi area a thanh area b
                        A.lT5 = B.lT5;
                        //set lai areadad
                        A.GetComponent<SS_ItemMove_KhamPha>().SetAreaDad(ItemOldDad);

                        listItem.Add(Item);
                        Item.GetComponent<SS_ItemMove_KhamPha_restriction>().lT5 = this;
                        A.lT5.OnDone();
                        B.lT5.OnDone();
                    }
                    else    //neu vat bi keo k trong area
                    {
                        A.lT5 = null;
                        A.GetComponent<SS_ItemMove_KhamPha>().SetAreaDad(null);

                        listItem.Add(Item);
                        Item.GetComponent<SS_ItemMove_KhamPha_restriction>().lT5 = this;
                        B.lT5.OnDone();
                    }
                }
                else
                {
                    listItem.Add(Item);
                    Item.GetComponent<SS_ItemMove_KhamPha_restriction>().lT5 = this;
                    OnDone();
                }
            }
        }
    }
    public void ReleaseItem(SS_ItemMove_KhamPha item)
    {
        if (listItem.Contains(item))
        {
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
        GameObject item;
        if (firstItemMove) { item = levelMap.itemManager_1.GetCurrentMovingItem(); }
        else { item = levelMap.itemManager_2.GetCurrentMovingItem(); }

        if (item == null)
        {
            itemCheckTemplate = null;
        }
        else
        {
            itemCheckTemplate = item.GetComponent<SS_ItemMoveLT5>();
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
    //    bool isRight = true;
    //    if (GameManager.Instance.canShowReaction)
    //    {
    //        if (isRight)
    //        {
    //            TimeLineManage.INSTANCE.RightPlay();
    //        }
    //        else
    //        {
    //            TimeLineManage.INSTANCE.WrongPlay();
    //        }
    //    }
    }
}
