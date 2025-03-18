using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeColoring : MonoBehaviour
{
    public bool isRight = false;
    public ItemValue itemValue;
    public int wrongChoiceCount = 0;
    private void Start()
    {
        itemValue = GetComponent<ItemValue>();
        wrongChoiceCount = 0;
    }
    private void OnMouseDown()
    {
        if (InputCallBackPen.Instance != null)
        {
            isRight = false;
            if (InputCallBackPen.Instance.currentPen != 0)
            {
                if (itemValue.GetRequireValue() == InputCallBackPen.Instance.currentPen)
                {
                    isRight = true;
                    TimeLineManage.INSTANCE.RightPlay();
                }
                else
                {
                    wrongChoiceCount++;
                    TimeLineManage.INSTANCE.WrongPlay();
                }
            }
            GetComponent<SpriteRenderer>().color = InputCallBackPen.Instance.allPensColor[InputCallBackPen.Instance.currentPen];
        }

    }
}
