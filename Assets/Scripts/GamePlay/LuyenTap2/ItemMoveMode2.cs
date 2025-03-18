using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemMoveMode2 : MonoBehaviour
{
    private bool isBeingHeld;
    private Vector2 v2Tmp;
    private Vector3 v3Tmp, v3Tmp1;
    private Transform thisTransform;
    [SerializeField]
    private int id;
    [SerializeField]
    private LayerMask itemMask;
    [SerializeField]
    private bool isSelected;
    [SerializeField]
    private bool isCallEvent;
    [SerializeField]
    private UnityEvent myEvent;
    [SerializeField]
    private Transform pointToDraw;
    [SerializeField]
    private float scaleTo = 1.01f;
    [SerializeField]
    private DestinationMode2 desSelected;
    private int m_wrongChoiceCount;
    public int wrongChoiceCount { get { return m_wrongChoiceCount; } }
    private LineRenderer lr;
    private Vector3 localScaleDefault;
    Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;
        thisTransform = transform;
        if(pointToDraw == null && thisTransform.childCount > 0)
        {
            pointToDraw = thisTransform.GetChild(0);
        }
        lr = GetComponent<LineRenderer>();
        localScaleDefault = thisTransform.localScale;
        m_wrongChoiceCount = 0;
        lr.SetWidth(0.02f, 0.02f);
        lr.SetColors(Color.yellow, Color.yellow);
        if(thisTransform.localPosition.x >0)
        {
            if (pointToDraw.localPosition.x > 0)
            {
                Vector3 tmp = pointToDraw.localPosition;
                tmp.x *= -1;
                pointToDraw.localPosition = tmp;
            }
            
        }
        else
        {
            if (pointToDraw.localPosition.x < 0)
            {
                Vector3 tmp = pointToDraw.localPosition;
                tmp.x *= -1;
                pointToDraw.localPosition = tmp;
            }
        }
    }
    private void Update()
    {
        if (isBeingHeld)
        {
            DrawMouse();
        }
    }
    private void DrawMouse()
    {
        v3Tmp = Input.mousePosition;
        v3Tmp.z = mainCam.transform.position.z;
        v3Tmp = mainCam.ScreenToWorldPoint(v3Tmp);
        Vector3[] pos = { pointToDraw.transform.position, v3Tmp};
        lr.SetPositions(pos);
    }
    private void OnMouseDown()
    {

        if (GameManager.Instance.canPlay)
        {
            isBeingHeld = true;
            Vector3[] pos = { Vector3.zero, Vector3.zero };
            lr.SetPositions(pos);
            v3Tmp = localScaleDefault * scaleTo;
            thisTransform.localScale = v3Tmp;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
        if (GameManager.Instance.canPlay)
        {
            Vector3[] pos = { Vector3.zero, Vector3.zero };
            lr.SetPositions(pos);
            if (isCallEvent)
            {
                Debug.Log("Call my event");
                myEvent.Invoke();
            }
            thisTransform.localScale = localScaleDefault;
            v3Tmp = Input.mousePosition;
            v3Tmp.z = mainCam.transform.position.z;
            Ray ray = mainCam.ScreenPointToRay(v3Tmp);
            RaycastHit  hit;
            if(Physics.Raycast(ray, out hit, 100, itemMask))
            {
                if (!hit.transform.name.Equals(name))
                {
                    DestinationMode2 itemMatch = hit.transform.GetComponent<DestinationMode2>();
                    if (itemMatch == null) return;
                    if (desSelected != null)
                    {
                        if (desSelected.GetID().Equals(itemMatch.GetID()))
                        {
                            ReDraw();
                            Reaction();
                            return;
                        }
                        else
                        {
                            desSelected = itemMatch;
                            ReactionAndCount();
                        }
                        
                    }
                    else
                    {
                        desSelected = itemMatch;
                        ReactionAndCount();
                    }
                    ReDraw();
                }
            }
            else
            {
                if(desSelected != null)
                {
                    ReDraw();
                }
            }
        }
    }
    
    private void ReDraw()
    {
        Vector3 point2Position;
        if(thisTransform.position.x < desSelected.transform.position.x)
        {
            point2Position = desSelected.GetPointDrawPositionLeft();
        }
        else
        {
            point2Position = desSelected.GetPointDrawPositionRight();

        }
        Vector3[] pos = { pointToDraw.transform.position, point2Position };
        lr.SetPositions(pos);
    }
    private void Reaction()
    {
        if (!GameManager.Instance.canShowReaction) return;
        if (desSelected.GetID().Equals(id))
        {
            TimeLineManage.INSTANCE.RightPlay();
        }
        else
        {
            TimeLineManage.INSTANCE.WrongPlay();
        }
    }
    private void ReactionAndCount()
    {
        if (desSelected.GetID().Equals(id))
        {
            if (GameManager.Instance.canShowReaction)
            {
                TimeLineManage.INSTANCE.RightPlay();
            }
        }
        else
        {
            if (GameManager.Instance.canShowReaction)
            {
                TimeLineManage.INSTANCE.WrongPlay();
            }
            m_wrongChoiceCount++;
        }
    }
    public bool IsRightChoice()
    {
        if(desSelected == null)
        {
            return false;
        }
        return desSelected.GetID().Equals(id);
    }
    public int GetID()
    {
        return id;
    }
}
