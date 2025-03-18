using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AreaPlay : MonoBehaviour
{
    private BoxCollider boxCollider;

    private Vector3 topLeft, topRight, botLeft, botRight, v3Tmp, defaultScale;

    [SerializeField]
    [Range(0, 100)]
    private int id;
    [SerializeField]
    private List<ItemContainBoxcollider> listItem;
    private TextMeshPro[] textMeshPros;
    private Color normalColor;
    [SerializeField]
    private Color onOverColor;
    [SerializeField]
    ItemManage itemManager;
    private SpriteRenderer sprd;
    private ItemContainBoxcollider itemCheckTemplate;
    private bool isItemOver = false;
    private Transform _transform;
    private void Start()
    {
        itemManager = transform.parent.parent.GetChild(0).GetComponent<ItemManage>();
        _transform = transform;
        defaultScale = _transform.localScale;
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
        if (textMeshPros == null)
        {
            textMeshPros = GetComponentsInChildren<TextMeshPro>();
        }
        listItem = new List<ItemContainBoxcollider>();
    }

    public void CheckPositionOfItem(ItemContainBoxcollider Item)
    {
        //if (!GameManager.Instance.canPlay) return;
        if (IsIn(Item.getBounds()))
        {
            if (!listItem.Contains(Item))
            {
                listItem.Add(Item);
                if (!Item.GetAreaId().Equals(id))
                {
                    if(GameManager.Instance.canPlay)
                        TimeLineManage.INSTANCE.WrongPlay();
                    Item.AddFalseTime();
                }
                else
                {
                    if (GameManager.Instance.canPlay)
                        TimeLineManage.INSTANCE.RightPlay();
                }
            }
            Item.SetAreaIdSelected(id);
        }
        else
        {
            if (listItem.Contains(Item))
            {
                listItem.Remove(Item);
                Item.SetAreaIdSelected(-1);
            }
        }

        ShowInformation();
    }

    private void ShowInformation()
    {
        if (textMeshPros == null) return;
        int trueCount = 0, falseCount = 0;
        for(int i=0; i<listItem.Count; i++)
        {
            if (listItem[i].GetAreaId().Equals(id))
            {
                trueCount++;
            }
            else
            {
                falseCount++;
            }
        }
        //textMeshPros[0].text = "" + trueCount;
        //textMeshPros[1].text = "" + falseCount;
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
    public int GetTrueItemCount()
    {
        int trueCount = 0;
        for (int i = 0; i < listItem.Count; i++)
        {
            if (listItem[i].GetAreaId().Equals(id))
            {
                trueCount++;
            }
        }
        Debug.Log("true: " + trueCount);
        return trueCount;
    }
    public int GetWrongItemCount()
    {
        int falseCount = 0;
        for (int i = 0; i < listItem.Count; i++)
        {
            if (!listItem[i].GetAreaId().Equals(id))
            {
                falseCount++;
            }
        }
        Debug.Log("wrong: " + falseCount);
        return falseCount;
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
        if (itemManager.GetCurrentMovingItem() == null)
        {
            itemCheckTemplate = null;
        }
        else
        {
            itemCheckTemplate = itemManager.GetCurrentMovingItem().GetComponent<ItemContainBoxcollider>();
        }
        if (itemCheckTemplate == null)
        {
            //itemCheckTemplate = levelMap.itemManager.GetCurrentMovingItem().GetComponent<ItemContainBoxcollider>();
            sprd.color = normalColor;
            isItemOver = false;
            //_transform.localScale = defaultScale;
            return;
        }
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
    private void DoBlink()
    {

    }
}
