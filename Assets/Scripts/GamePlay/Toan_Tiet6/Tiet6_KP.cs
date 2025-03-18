using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet6_KP : MonoBehaviour
{
    public GameObject nextButton;
    public bool canNextPhase = false;
    public List<AnimatorList> listAnimPhase1;
    public List<AnimatorList> listAnimPhase2;
    public List<AnimatorList> listAnimPhase3;
    public List<AnimatorList> listAnimPhase4;
    public List<AnimatorList> listAnimPhase5_1;
    public List<AnimatorList> listAnimPhase5_2;
    public List<AnimatorList> listAnimPhase5_3;
    public List<AnimatorList> listAnimPhase6_1;
    public List<AnimatorList> listAnimPhase6_2;
    public List<AnimatorList> listAnimPhase6_3;
    private int curPhaseIndex = 1;

    public void nextPhase()
    {
        if (canNextPhase)
        {
            curPhaseIndex++;
            switch (curPhaseIndex)
            {
                case 2:
                    next(2, listAnimPhase2);
                    break;
                case 3:
                    next(3, listAnimPhase3);
                    break;
                case 4:
                    next(4, listAnimPhase4);
                    break;
                case 5: //5_1
                    next(5, listAnimPhase5_1);
                    break;
                case 6: //5_2
                    next(6, listAnimPhase5_2);
                    break;
                case 7: //5_3
                    next(7, listAnimPhase5_3);
                    break;
                case 8: //6_1
                    next(8, listAnimPhase6_1);
                    break;
                case 9: //6_2
                    next(9, listAnimPhase6_2);
                    break;
                case 10: //6_3
                    next(10, listAnimPhase6_3, true);
                    break;
            }

        }
    }

    void next(int phase, List<AnimatorList> ListAnim, bool end = false)
    {
        transform.GetChild(phase - 2).gameObject.SetActive(false);
        transform.GetChild(phase - 1).gameObject.SetActive(true);
        StartCoroutine(startTimeline(phase, end));
        for (int index = 0; index < ListAnim.Count; index++)
        {
            StartCoroutine(playAnim(ListAnim[index]));
        }
    }

    public IEnumerator startTimeline(int index, bool end = false)
    {
        StopCoroutine("playAnim");
        canNextPhase = false;
        nextButton.SetActive(false);
        TimeLineManage.INSTANCE.Tiet6("KP", index);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        if (!end)
        {
            canNextPhase = true;
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }
    }

    public IEnumerator playAnim(AnimatorList list)
    {
        yield return new WaitForSeconds(list.startTime);
        list.animController.SetTrigger("play");
    }
}
