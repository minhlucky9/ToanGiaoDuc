using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public int color;
    public bool isIn = false;
    public Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (!isIn)
        {
            if (InputCallBackPen.Instance != null)
            {
                InputCallBackPen.Instance.AllPensOut();
                InputCallBackPen.Instance.currentPen = color;
            }
            Debug.Log("Pen");
            anim.SetTrigger("In");
            isIn = true;
        }
        
    }
}
