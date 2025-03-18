using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableItemWithTouch : MonoBehaviour
{
    private float startPosX, startPosY;
    private bool isBeingHeld;
    public bool isMoving;
    public BoxCollider2D boxCol;
    public Vector2 topL, botR;
    Vector3 colliderPosition;
    Vector3 colliderSize;
    private Vector2 v2Tmp;
    private Vector3 v3Tmp;
    private Transform thisTransform;
    public Transform DP;
    public bool stayDefault, isWrongShow;
    private Camera mainCam;
    private void Start()
    {
        thisTransform = transform;
        mainCam = Camera.main;
        try
        {
            boxCol = GetComponent<BoxCollider2D>();
        }
        catch { }
        try
        {
            DP = thisTransform.parent;
        }
        catch { }
    }
    Touch t1;
    private void Update()
    {
        if (GameManager.Instance.canPlay)
        {
            try
            {
                //Vector3 colliderCenter = Vector3.Scale(transform.lossyScale, transform.position);
                colliderPosition = thisTransform.position;
                v2Tmp = Vector3.Scale(transform.lossyScale, boxCol.offset);
                colliderPosition.x += v2Tmp.x;
                colliderPosition.y += v2Tmp.y;
                colliderSize = Vector3.Scale(transform.lossyScale, boxCol.size);
                topL.x = colliderPosition.x - (colliderSize.x / 2);
                topL.y = colliderPosition.y + (colliderSize.y / 2);
                botR.x = colliderPosition.x + (colliderSize.x / 2);
                botR.y = colliderPosition.y - (colliderSize.y / 2);
                Debug.DrawLine(topL, botR, Color.blue);
            }
            catch
            {
                Debug.LogError("Loi o day nay.");
            }
            try
            {
                isMoving = Vector2.Distance(thisTransform.position, DP.position) > 0.4f;
            }
            catch { }
            if (Input.touchCount > 0)
            {
                for(int i=0; i<Input.touchCount; i++)
                {
                    t1 = Input.GetTouch(i);
                    v3Tmp = mainCam.ScreenToWorldPoint(t1.position);
                    v3Tmp.z = 0;
                    if (GameManager.Instance.canPlay)
                    {
                        if (t1.phase == TouchPhase.Began)
                        {
                            if (v3Tmp.x >= topL.x && v3Tmp.x <= botR.x && v3Tmp.y >= botR.y && v3Tmp.y <= topL.y)
                            {
                                isBeingHeld = true;
                                try
                                {
                                    //TapEffect.Instance.On(v3Tmp);
                                }
                                catch { }
                            }
                        }
                    }
                    if (isBeingHeld && GameManager.Instance.canPlay)
                    {
                        v3Tmp.x = v3Tmp.x - startPosX;
                        v3Tmp.y = v3Tmp.y - startPosY;
                        v3Tmp.z = thisTransform.position.z;
                        thisTransform.position = v3Tmp;
                    }
                    if ((t1.phase == TouchPhase.Ended || t1.phase == TouchPhase.Canceled) && isBeingHeld)
                    {
                        isBeingHeld = false;
                        if (GameManager.Instance.canPlay)
                        {
                            if (stayDefault)
                            {
                                thisTransform.position = DP.position;
                                if (isWrongShow)
                                {
                                    try
                                    {
                                        //WrongEffect.Instance.On();
                                    }
                                    catch { }
                                }
                            }
                            else
                            {

                            }

                        }
                    }
                }
               
            }
        }
    }
}
