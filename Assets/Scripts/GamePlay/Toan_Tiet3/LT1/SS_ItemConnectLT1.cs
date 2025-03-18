using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.Events;

public class SS_ItemConnectLT1 : MonoBehaviour
{
    bool isBeingHeld;
    Vector3 v3Tmp;

    [SerializeField] private LayerMask itemMask;
    [SerializeField] private Transform pointToDraw;
    [SerializeField] private SS_ItemConnectLT1 Destination;
    SS_RandomLT1 randLT1;
    public bool isConnected;
    private bool canConnect = false;

    LineRenderer lr;

    private void Start()
    {
        pointToDraw = transform.GetChild(0);

        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.02f;
        lr.startColor = Color.yellow;
        randLT1 = GetComponentInParent<SS_RandomLT1>();
    }

    public void setCanConnect(bool _CanConnect)
    {
        canConnect = _CanConnect;
    }

    private void Update()
    {
        if (isBeingHeld && ((randLT1 != null && randLT1.canConnect) || canConnect))
        {
            DrawMouse();
        }
    }

    private void DrawMouse()
    {
        v3Tmp = Input.mousePosition;
        v3Tmp.z = Camera.main.transform.position.z;
        Vector3[] pos = { pointToDraw.transform.position, Camera.main.ScreenToWorldPoint(v3Tmp) };
        lr.SetPositions(pos);
    }
    private void OnMouseDown()
    {
        if ((randLT1 != null && randLT1.canConnect) || canConnect)
        {
            isBeingHeld = true;

            if (Destination != null)
            {
                Destination.resetLr();
            }
            resetLr();

            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }

    private void OnMouseUp()
    {
        if ((randLT1 != null && randLT1.canConnect) || canConnect)
        {
            isBeingHeld = false;
            resetLr();
            transform.localScale = new Vector3(1, 1, 1);

            v3Tmp = Input.mousePosition;
            v3Tmp.z = Camera.main.transform.position.z;
            Ray ray = Camera.main.ScreenPointToRay(v3Tmp);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, itemMask))
            {
                if (!hit.transform.name.Equals(name))
                {
                    SS_ItemConnectLT1 item = hit.transform.GetComponent<SS_ItemConnectLT1>();

                    if (item.Destination == null)
                    {
                        Transform itemHit = hit.transform;
                        if (itemHit == null) { return; }
                        Destination = itemHit.GetComponent<SS_ItemConnectLT1>();
                        Destination.setConnection(this);
                        isConnected = true;
                        item.isConnected = true;
                        ReDraw();
                    }
                }
            }
            else
            {
                if (Destination != null)
                {
                    ReDraw();
                }
            }
        }
    }

    private void ReDraw()
    {
        Vector3[] pos = { pointToDraw.transform.position, Destination.transform.GetChild(0).position };
        lr.SetPositions(pos);
    }

    public void resetLr()
    {
        isConnected = false;
        if (Destination != null)
        {
            Destination.isConnected = false;
            Destination.setConnection(null);
            Destination = null;
        }

        Vector3[] pos = { Vector3.zero, Vector3.zero };
        lr.SetPositions(pos);
    }

    public void setConnection(SS_ItemConnectLT1 des)
    {
        Destination = des;
    }
}
