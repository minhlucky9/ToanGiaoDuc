using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCallBackPen : MonoBehaviour
{
    public static InputCallBackPen Instance;
    public int currentPen = 0;
    public Color[] allPensColor;
    public Animator[] allPensAnim;
    [SerializeField]
    private GameObject[] allPens;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        OnStart();
    }
    public void OnStart()
    {
        currentPen = 0;
        AllPensOut();
        allPensAnim[0].SetTrigger("In");
    }
    public void AllPensOut()
    {
        for(int i = 0; i < allPens.Length; i++)
        {
            if (allPens[i].GetComponent<Pen>().isIn)
            {
                allPensAnim[i].SetTrigger("Out");
                allPens[i].GetComponent<Pen>().isIn = false;
                break;
            }
        }
    }

}
