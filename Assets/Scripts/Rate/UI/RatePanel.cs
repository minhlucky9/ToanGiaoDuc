using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatePanel : MonoBehaviour, IPanel
{
    public static RatePanel Instance;
    [SerializeField]
    RateRow RowPrefab;
    GameObject panelChild;
    [SerializeField]
    Transform rowsDad;
    public List<RateRow> listRow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        try
        {
            panelChild = transform.GetChild(0).gameObject;
        }
        catch
        {
            Debug.LogError(gameObject.name);
        }
    }

    public void Active()
    {
        UpdateValue();
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
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
    public void UpdateValue()
    {
        ClearAllRow();
        RateManage.Instance.currentRateLesson.getOutPutRate();
    }
    public void ClearAllRow()
    {
        for(int i=0; i<listRow.Count; i++)
        {
            listRow[i].FreeRow();
            listRow[i].gameObject.SetActive(false);
        }
    }
    public void AddRow(string title, string value)
    {
        for(int i=0; i<listRow.Count; i++)
        {
            if (!listRow[i].isUsed)
            {
                listRow[i].gameObject.SetActive(true);
                listRow[i].SetValue(title, value);
                return;
            }
        }
        RateRow newRow = Instantiate(RowPrefab, rowsDad);
        newRow.gameObject.SetActive(true);
        newRow.SetValue(title, value);
        listRow.Add(newRow);
    }
}
