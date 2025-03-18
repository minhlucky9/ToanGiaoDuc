using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Tiet5_KP : MonoBehaviour
{
    public GameObject nextButton;
    public bool canNextPhase = false;
    public List<AnimatorList> listAnimPhase1;
    public List<AnimatorList> listAnimPhase2;
    public List<AnimatorList> listAnimPhase3;
    private int curPhaseIndex = 1;
    
    public void nextPhase()
    {
        if (canNextPhase)
        {
            curPhaseIndex++;
            switch (curPhaseIndex)
            {
                case 2:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    StartCoroutine(startTimeline(2));
                    for (int index = 0; index < listAnimPhase2.Count; index++)
                    {
                        StartCoroutine(playAnim(listAnimPhase2[index]));
                    }
                   
                    break;
                case 3:
                    nextButton.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                    StartCoroutine(startTimeline(3));
                    for (int index = 0; index < listAnimPhase3.Count; index++)
                    {
                        StartCoroutine(playAnim(listAnimPhase3[index]));
                    }
                    break;
            }
            
        }
    }

    public IEnumerator startTimeline(int index)
    {
        StopCoroutine("playAnim");
        canNextPhase = false;
        nextButton.SetActive(false);
        TimeLineManage.INSTANCE.Tiet5("KP", index);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        canNextPhase = true;
        nextButton.SetActive(true);
    }

    public IEnumerator playAnim(AnimatorList list)
    {
        yield return new WaitForSeconds(list.startTime);
        list.animController.SetTrigger("play");
    }
}
