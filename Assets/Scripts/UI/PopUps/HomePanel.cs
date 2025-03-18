using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour, IPanel
{
    public static HomePanel Instance;
    private GameObject panelChild;
    [SerializeField]
    private Transform buttonsDadTransform;
    SelectLessonFormatButton[] listButton;
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
    private void Start()
    {
        InitButton();
    }
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
    private void InitButton()
    {
        listButton = buttonsDadTransform.GetComponentsInChildren<SelectLessonFormatButton>();
        List<BaiHoc> listBaiHoc = BaiHocManager.INSTANCE.GetListBaiHoc();
        for(int i=0; i<listBaiHoc.Count; i++)
        {
            if (i < listButton.Length)
            {
                BaiHoc bh = listBaiHoc[i];
                SelectLessonFormatButton button = listButton[i];
                button.SetId(bh.GetMaBaiHoc());
                button.SetBaiHoc(bh);
            }
        }
        for (int i=0; i<listButton.Length; i++)
        {
            SelectLessonFormatButton buttonTemplate = listButton[i];
            BaiHoc bh = buttonTemplate.GetBaiHoc();
            buttonTemplate.GetComponent<Button>().onClick.AddListener(() => SelectLesson(bh));
        }
    }
    private void SelectLesson(BaiHoc bh)
    {
        //Debug.Log("Selected: " + id);
        GameManager.Instance.SelectBaiHoc(bh);
        Deactive();
        SelectLessonPanel.Instance.Active();
        UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Select_Lesson);
    }
}
