using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosChoice : MonoBehaviour
{
    public bool isRightChoice = false;
    public bool isRight = false;
    public int wrongCount = 0;
    private void OnMouseDown()
    {
        if (!GameManager.Instance.canPlay) return;
        if (transform.GetChild(0).gameObject.activeSelf){
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else 
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (isRightChoice)
            {

                isRight = true;
            }
            else
            {
                isRight = false;
                wrongCount++;
            }
            if (GameManager.Instance.canShowReaction)
            {
                if (isRight)
                {
                    TimeLineManage.INSTANCE.RightPlay();
                }
                else
                {
                    TimeLineManage.INSTANCE.WrongPlay();
                }
            }
        }
        
        
    }
}
