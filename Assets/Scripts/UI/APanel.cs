using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APanel : MonoBehaviour
{
    private GameObject panelChild;
    public void Active()
    {

        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        try
        {
            panelChild = transform.GetChild(0).gameObject;
}
        catch { }
    }

    public void Deactive()
    {
        try
        {
            GetComponent<Animator>().Play("out");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public bool IsActive()
    {
        if (panelChild != null)
            return panelChild.activeSelf;
        return false;
    }
}
