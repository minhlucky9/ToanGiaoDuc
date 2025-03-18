using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet5_double_gp : MonoBehaviour
{
    public GameObject button;
    public List<AnimatorList> listAnimPhase1;
    public List<AnimatorList> listAnimPhase2;
    public int id2Timeline = 1;
    public void nextPhase()
    {
        if (GameManager.Instance.canPlay)
        {
            button.SetActive(false);
            StartCoroutine(playTimeline());
            foreach (Tiet5_Inputs item in GetComponentInParent<Tiet5_double_lvlmap>().inputList1)
            {
                item.canInteract = false;
            }
            foreach (Tiet5_Inputs item in GetComponentInParent<Tiet5_double_lvlmap>().inputList2)
            {
                item.canInteract = false;
                item.gameObject.SetActive(true);
                item.inputText.gameObject.SetActive(true);
            }
        }
    }

    public void playAnimList(int phase)
    {
        switch (phase)
        {
            case 1:
                foreach (AnimatorList item in listAnimPhase1)
                {
                    StartCoroutine(playAnim(item));
                }
                break;
            case 2:
                foreach (AnimatorList item in listAnimPhase2)
                {
                    StartCoroutine(playAnim(item));
                }
                break;
        }
    }

    IEnumerator playAnim(AnimatorList anim)
    {
        yield return new WaitForSeconds(anim.startTime);

        anim.animController.SetTrigger("play");
    }

    IEnumerator playTimeline()
    {
        TimeLineManage.INSTANCE.Tiet5("LT_2", id2Timeline);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        GetComponentInChildren<Tiet5_double_gp>().playAnimList(2);
        yield return new WaitForSeconds((float)timeW1);

        foreach (Tiet5_Inputs item in GetComponentInParent<Tiet5_double_lvlmap>().inputList2)
        {
            item.canInteract = true;
        }
    }
}
