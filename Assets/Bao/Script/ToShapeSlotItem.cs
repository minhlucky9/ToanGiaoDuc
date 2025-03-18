using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToShapeSlotItem : MonoBehaviour
{
    public GameObject slotContainer;
    public List<GameObject> allSlot;
    private Vector3 position;
    public bool isRight = false;
    private Vector3 mousePos;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    private bool canMove = true;
    public int wrongChoiceCount = 0;
    public float distance = 0.25f;
    public shapeType type;
    public void OnStart()
    {
        isRight = false;
        wrongChoiceCount = 0;
        allSlot.Clear();
        for (int i = 0; i < slotContainer.transform.childCount; i++)
        {
            allSlot.Add(slotContainer.transform.GetChild(i).gameObject);
        }
        position = transform.position;
        //wrongChoiceCount = 0;
        //canMove = true;
        //if (slot.Count == 0)
        //{
        //    isRight = true;
        //    haveSlot = false;
        //}
        //else
        //{
        //    haveSlot = true;
        //    isRight = false;
        //}

        //for (int i = 0; i < allSlot.transform.childCount; i++)
        //{
        //    allSlots.Add(allSlot.transform.GetChild(i).gameObject);
        //}

    }
    private void OnMouseDown()
    {
        if (!GameManager.Instance.canPlay) return;
        if (canMove)
        {
            positionOfScreen = Camera.main.WorldToScreenPoint(transform.position);
            offsetValue = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
        }
    }
    private void OnMouseDrag()
    {
        if (!GameManager.Instance.canPlay) return;
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
        if (!GameManager.Instance.canPlay) return;
        if (canMove)
        {

            isRight = false;
            foreach (GameObject gameSlot in allSlot)
            {
                if (gameSlot.activeSelf)
                {
                    if (Vector2.Distance((Vector2)gameSlot.transform.position, (Vector2)transform.position) < distance)
                    {
                        SlotShape slot = gameSlot.GetComponent<SlotShape>();
                        if (!gameSlot.GetComponent<SlotShape>().isRight && gameSlot.GetComponent<SlotShape>().currentShape == null) 
                        {
                            if (type == gameSlot.GetComponent<SlotShape>().type)
                            {

                                if (type == shapeType.triangle)
                                {
                                    if (transform.rotation.eulerAngles.z % 360 == gameSlot.transform.rotation.eulerAngles.z % 360)
                                    {
                                        transform.position = gameSlot.transform.position;
                                        gameSlot.GetComponent<SlotShape>().isRight = true;
                                        TimeLineManage.INSTANCE.RightPlay();
                                        isRight = true;
                                        Instantiate(this, position, transform.rotation,transform.parent).OnStart();
                                        break;
                                    }
                                }
                                else
                                {
                                    transform.position = gameSlot.transform.position;
                                    gameSlot.GetComponent<SlotShape>().isRight = true;
                                    TimeLineManage.INSTANCE.RightPlay();
                                    isRight = true;
                                    Instantiate(this, position, transform.rotation, transform.parent).OnStart();
                                    break;
                                }
                            }
                            if (type == shapeType.triangle && gameSlot.GetComponent<SlotShape>().type == shapeType.square)
                            {
                                transform.position = gameSlot.transform.position;
                                gameSlot.GetComponent<SlotShape>().currentShape = gameObject;
                                TimeLineManage.INSTANCE.RightPlay();
                                Instantiate(this, position, transform.rotation, transform.parent).OnStart();
                                isRight = true;
                                break;
                            }
                        }
                        if (!gameSlot.GetComponent<SlotShape>().isRight && type == shapeType.triangle && gameSlot.GetComponent<SlotShape>().type == shapeType.square)
                        {
                            if (Mathf.Abs((transform.rotation.eulerAngles.z - gameSlot.GetComponent<SlotShape>().currentShape.transform.rotation.eulerAngles.z) % 360) == 180)
                            {
                                transform.position = gameSlot.transform.position;
                                gameSlot.GetComponent<SlotShape>().isRight = true;
                                TimeLineManage.INSTANCE.RightPlay();
                                Instantiate(this, position, transform.rotation, transform.parent).OnStart();
                                isRight = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!isRight)
            {
                foreach (GameObject gameSlot in allSlot)
                {
                    if (gameSlot.activeSelf)
                    {
                        if (Vector2.Distance((Vector2)gameSlot.transform.position, (Vector2)transform.position) < distance)
                        {
                            wrongChoiceCount++;
                            TimeLineManage.INSTANCE.WrongPlay();
                            break;
                        }
                    }
                }
                StartCoroutine(MoveBack());
            }
            else
            {
                canMove = false;
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
