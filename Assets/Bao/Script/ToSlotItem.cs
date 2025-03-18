using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSlotItem : MonoBehaviour
{
    public List<GameObject> slot;
    private Vector3 position;
    public bool isRight = false;
    private Vector3 mousePos;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    private bool canMove = true;
    public GameObject allSlot;
    public List<GameObject> allSlots;
    public int wrongChoiceCount = 0;
    public bool haveSlot;
    public float distance = 0.25f;
    void Start()
    {
        wrongChoiceCount = 0;
        canMove = true;
        if (slot.Count == 0)
        {
            isRight = true;
            haveSlot = false;
        }
        else
        {
            haveSlot = true;
            isRight = false;
        }
        position = transform.position;
        for(int i = 0; i < allSlot.transform.childCount; i++)
        {
            allSlots.Add(allSlot.transform.GetChild(i).gameObject);
        }
    }
    private void OnMouseDown()
    {
        if (canMove)
        {
            positionOfScreen = Camera.main.WorldToScreenPoint(transform.position);
            offsetValue = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
        }
    }
    private void OnMouseDrag()
    {
        if (canMove)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue;
            currentPosition.z = position.z;
            transform.position = currentPosition;
        }

    }
    private void OnMouseUp()
    {
        if (canMove)
        {

            isRight = false;
            foreach (GameObject gameSlot in slot)
            {
                if (gameSlot.activeSelf)
                {
                    if (Vector2.Distance((Vector2)gameSlot.transform.position, (Vector2)transform.position) < distance)
                    {
                        transform.position = gameSlot.transform.position;
                        TimeLineManage.INSTANCE.RightPlay();
                        isRight = true;
                        break;
                    }
                }
            }
            if (!isRight)
            {
                foreach(GameObject wrongslot in allSlots)
                {
                    if (wrongslot.activeSelf)
                    {
                        if (Vector2.Distance((Vector2)wrongslot.transform.position, (Vector2)transform.position) < distance)
                        {
                            //transform.position = position;
                            StartCoroutine(MoveBack());
                            wrongChoiceCount++;
                            TimeLineManage.INSTANCE.WrongPlay();
                            break;
                        }
                    }
                }
            }
            else
            {
                canMove = false;
                position = transform.position;
            }
        }
    }
    private IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(0.1f);
        float startTime = Time.time;
        while (Vector3.Distance(this.transform.position, position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.time - startTime);
            yield return 1;
        }
        transform.position = position;
    }
}
