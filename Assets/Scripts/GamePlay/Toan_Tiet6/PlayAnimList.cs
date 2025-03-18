using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimList : MonoBehaviour
{
    public List<AnimatorList> listAnim;

    public void playAnimList()
    {
        for (int i = 0; i < listAnim.Count; i++)
        {
            StartCoroutine(playAnim(listAnim[i]));
        }
    }

    IEnumerator playAnim(AnimatorList anim)
    {
        yield return new WaitForSeconds(anim.startTime);

        anim.animController.SetTrigger("play");
    }
}
