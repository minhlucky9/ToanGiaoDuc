using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemValue : MonoBehaviour
{
    private int value = 0;
    [SerializeField]
    private int valueRequire;
    [SerializeField]
    private TextMeshPro text;
    public void SetRequireValue(int val)
    {
        valueRequire = val;
        if (text != null)
        {
            text.text = val.ToString();
        }
    }
    public int GetRequireValue()
    {
        return valueRequire;
    }
    public int GetValue()
    {
        return value;
    }
    public void SetValue(int val)
    {
        value = val;
    }
    public void IncValue()
    {
        value++;
    }
    public void DecValue()
    {
        value--;
    }
    public bool IsRight()
    {
        return value.Equals(valueRequire);
    }
    
}
