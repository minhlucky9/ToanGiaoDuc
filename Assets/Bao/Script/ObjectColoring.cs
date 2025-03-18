using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColoring : MonoBehaviour
{
    public ItemValue item;
    public bool isColored = false;
    private void OnMouseDown()
    {
        if (InputCallBackPen.Instance != null)
        {
            if (InputCallBackPen.Instance.currentPen == 0)
            {
                if (isColored == true)
                {
                    item.DecValue();
                }
                isColored = false;
            }
            else
            {
                if (isColored == false)
                {
                    item.IncValue();
                }
                isColored = true;
            }
            GetComponent<SpriteRenderer>().color = InputCallBackPen.Instance.allPensColor[InputCallBackPen.Instance.currentPen];
        }

    }
}
