using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChoice : MonoBehaviour
{
    public bool isRightChoice = false;
    public bool isRight = false;
    public int wrongCount = 0;
    public LuyenTapChonMiengGhep level;
    private void OnMouseDown()
    {
        if (!GameManager.Instance.canPlay) return;
        if (isRightChoice)
        {
            isRight = true;
            level.OnRightChoice();
            GameManager.Instance.canPlay = false;
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
