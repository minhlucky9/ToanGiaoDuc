using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateRow: MonoBehaviour
{
    public Text QualificationText;
    public Text ResultText;
    public int id;
    public bool isUsed = false;
    public void SetValue(string qualification, string value)
    {
        if (isUsed) return;
        isUsed = true;
        QualificationText.text = qualification;
        ResultText.text = value;
    }
    public void FreeRow()
    {
        isUsed = false;
    }
}
