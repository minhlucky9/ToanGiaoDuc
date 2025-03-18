using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiet6_AreaReceiver : MonoBehaviour
{
    private BoxCollider boxCollider;

    private Vector3 topLeft, topRight, botLeft, botRight;
    [Range(10, 100)] public int itemCountRequire;
    [SerializeField] List<Tiet6_ItemMove> listItem;
    [SerializeField] Tiet6_LT_lvlmap levelMap;
    [SerializeField] Color onOverColor;
    private Color normalColor;
    private SpriteRenderer sprd;
    private Tiet6_ItemMove itemCheckTemplate;
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
        listItem = new List<Tiet6_ItemMove>();
        levelMap = transform.GetComponentInParent<Tiet6_LT_lvlmap>();
    }

    public void setItemCountRequire(int count)
    {
        itemCountRequire = count;
    }

    public void CheckPositionOfItem(Tiet6_ItemMove Item)
    {
        if (!GameManager.Instance.canPlay) return;
        if (IsIn(Item.getBounds()))
        {
            if (!listItem.Contains(Item))
            {
                listItem.Add(Item);
                
                Item.SetAreaDad(this);
            }
        }
    }
    public void ReleaseItem(Tiet6_ItemMove item)
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
            itemCheckTemplate = levelMap.itemManager.GetCurrentMovingItem().GetComponent<Tiet6_ItemMove>();
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

    public List<Tiet6_ItemMove> getListItem() { return listItem; }
}
