using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemQuantity : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text;
    private ItemValue ItemValue;
    [SerializeField]
    private Color textColorDefault;
    [SerializeField]
    private Color textColorRight;
    public int value;
    private void Start()
    {
        ItemValue = this.GetComponent<ItemValue>();
    }
    private void OnMouseDown()
    {
        if (InputCallBackPopupPanel.INSTANCE.IsActive()) return;
        Debug.Log("Item Btn Call");
        InputCallBackPopupPanel.INSTANCE.Active(value, UpdateValue, AfterInput);
    }
    public void UpdateValue(int val)
    {
        value = val;
    }
    public void AfterInput()
    {

    }
}
