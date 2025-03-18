using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovableItem : MonoBehaviour
{
    private bool isBeingHeld;
    private Vector2 v2Tmp;
    private Vector3 v3Tmp;
    private Transform thisTransform;
    [SerializeField]
    private bool setDefaultPositionOnMouseDown;
    [SerializeField]
    private bool stayDefault;
    private Transform DP;
    [SerializeField]
    private bool isCallEvent;
    [SerializeField]
    private UnityEvent myEvent;
    [SerializeField]
    private bool isCallOnMouseDownEvent;
    [SerializeField]
    private UnityEvent onMouseDownEvent;
    private float mZCoord;
    private Vector3 offSet, defaultPos;
    Camera mainCam;
    private ItemContainBoxcollider itemCollider;
    [SerializeField]
    private bool hasContrain;
    [SerializeField]
    private Transform TopRightContrain;
    [SerializeField]
    private Transform BotLeftContrain;

    private bool canMoveItem = true;
    private void Start()
    {
        mainCam = Camera.main;
        thisTransform = transform;
        try
        {
            itemCollider = GetComponent<ItemContainBoxcollider>();
        }
        catch { }
        try
        {
            if(DP == null)
            { 
                DP = thisTransform.parent;
            }
        }
        catch { }
    }
    private void OnMouseDrag()
    {
        if (isBeingHeld && GameManager.Instance.canPlay && canMoveItem)
        {
            if (!hasContrain)
            {
                thisTransform.position = GetMouseWorldPosition() + offSet;
            }
            else
            {
                v3Tmp = GetMouseWorldPosition() + offSet;
                if (IsInContrain(v3Tmp))
                {
                    thisTransform.position = v3Tmp;
                }
            }
        }

    }
    private bool IsInContrain(Vector3 position)
    {
        if(position.x > BotLeftContrain.position.x && position.x < TopRightContrain.position.x 
            && position.z > BotLeftContrain.position.z && position.z < TopRightContrain.position.z)
        {
            return true;
        }
        return false;
    }
    private void OnMouseDown()
    {
        
        if (GameManager.Instance.canPlay && canMoveItem)
        {
            if (isCallOnMouseDownEvent)
            {
                Debug.Log("Call On mouse down event");
                onMouseDownEvent.Invoke();
            }
            if (setDefaultPositionOnMouseDown)
            {
                defaultPos = transform.position;
            }
            mZCoord = mainCam.WorldToScreenPoint(thisTransform.position).z;
            offSet = thisTransform.position - GetMouseWorldPosition();
            isBeingHeld = true;
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        v3Tmp = Input.mousePosition;
        v3Tmp.z = mZCoord;
        //Debug.Log(mainCam.ScreenToWorldPoint(v3Tmp));
        v3Tmp = mainCam.ScreenToWorldPoint(v3Tmp);
        v3Tmp.z = thisTransform.position.z;
        return v3Tmp;
    }
    private void OnMouseUp()
    {
        isBeingHeld = false;
        if (GameManager.Instance.canPlay && canMoveItem)
        {
            if (stayDefault)
            {
                thisTransform.position = DP.position;
            }
            else
            {

            }
            if (isCallEvent)
            {
                Debug.Log("Call my event");
                myEvent.Invoke();
            }
        }
    }
    public void SetPositionToDefault()
    {
        thisTransform.position = defaultPos;
    }

    public void SetCanMoveItem(bool _moveItem)
    {
        canMoveItem = _moveItem;
    }
}
