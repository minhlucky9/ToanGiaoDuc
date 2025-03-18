using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLessonPanel : MonoBehaviour, IPanel
{
    public static SelectLessonPanel Instance;

    [SerializeField]
    private Transform buttonsDadTransform;
    private GameObject panelChild;
    List<SelectLessonButton> listButtons = new List<SelectLessonButton>();
    [SerializeField]
    private SelectLessonButton buttonPrefab;
    [SerializeField]
    private bool isInitButton = false;
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
        catch { }
    }
    private void Start()
    {
        isInitButton = false;
    }
    public void Active()
    {
        InitButtons();
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

    private void InitButtons()
    {
        if (!isInitButton)
        {
            List<TietHoc> listBaiHoc = GameManager.Instance.GetCurrentBaiHoc().GetListTietHoc();
            int totalSize = listBaiHoc.Count;
            Debug.Log("Setup tiet hoc count: " + listBaiHoc.Count);
            for (int i = 0; i < totalSize; i++)
            {
                TietHoc th = listBaiHoc[i];
                SelectLessonButton button = Instantiate(buttonPrefab, buttonsDadTransform);
                button.SetId(th.GetMaTietHoc());
                button.SetTietHoc(th);
                button.SetImage(th.GetImageIcon());
                button.SetTitle(th.GetTenTietHoc());
                button.GetComponent<Button>().onClick.AddListener(() => SelectLesson(th));
                listButtons.Add(button);
            }
            isInitButton = true;
        }
        else
        {
            List<TietHoc> listBaiHoc = GameManager.Instance.GetCurrentBaiHoc().GetListTietHoc();
            int totalSize = listBaiHoc.Count;
            int currentSize = listButtons.Count;
            if (currentSize < totalSize)
            {
                int needSize = totalSize - currentSize;
                for (int i = 0; i < needSize; i++)
                {
                    SelectLessonButton button = Instantiate(buttonPrefab, buttonsDadTransform);
                    listButtons.Add(button);
                }
            }
            for (int i = 0; i < currentSize; i++)
            {
                SelectLessonButton button = listButtons[i];
                if (i < totalSize)
                {
                    TietHoc th = listBaiHoc[i];
                    button.SetId(th.GetMaTietHoc());
                    button.SetTietHoc(th);
                    button.SetImage(th.GetImageIcon());
                    button.SetTitle(th.GetTenTietHoc());
                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener(() => SelectLesson(th));
                    button.gameObject.SetActive(true);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
        }
    }

    private void SelectLesson(TietHoc tietHoc)
    {
        Deactive();
        GameManager.Instance.SelectTietHoc(tietHoc);
        tietHoc.SetInstances();
        SelectModeLessonPanel.Instance.Active();
        UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Select_Mode);
    }

    public void ButtonBack()
    {
        Deactive();
        HomePanel.Instance.Active();
        UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Home);
    }
}
