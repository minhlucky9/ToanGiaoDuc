using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiet5_TuKT : MonoBehaviour
{
    public List<AnimatorList> listAnim;
    [HideInInspector] public Tiet5_Inputs.InputType operatorType;
    public Transform ObjA;
    public Transform ObjB;
    [HideInInspector] public List<Tiet5_Inputs> InputA;
    [HideInInspector] public List<Tiet5_Inputs> InputB;
    [HideInInspector] public List<Tiet5_Inputs> InputAns;
    [HideInInspector] public bool tuKT;
    private void Start()
    {
        Randomized();
    }

    void Randomized()
    {
        int randomA = Random.Range(1, ObjB.childCount), randomB = Random.Range(1, ObjA.childCount);
        foreach (Tiet5_Inputs item in InputA)
        {
            if (!tuKT & item.hasFixedInput)
            {
                randomA = item.neededInputInt;
                break;
            }
        }
        foreach (Tiet5_Inputs item in InputB)
        {
            if (!tuKT & item.hasFixedInput)
            {
                randomB = item.neededInputInt;
                break;
            }
        }
        foreach (Tiet5_Inputs item in InputA)
        {
            item.neededInputInt = randomA;
        }
        foreach (Tiet5_Inputs item in InputB)
        {
            item.neededInputInt = randomB;
        }
        int Ans = randomA + randomB;

        for (int i = ObjA.childCount-1; i >= randomA; i--)
        {
            ObjA.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = ObjB.childCount - 1; i >= randomB; i--)
        {
            ObjB.GetChild(i).gameObject.SetActive(false);
        }
        foreach (Tiet5_Inputs item in InputAns)
        {
            item.neededInputInt = Ans;
        }

        Debug.Log(randomA + " " + randomB + " " + Ans);
    }

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
