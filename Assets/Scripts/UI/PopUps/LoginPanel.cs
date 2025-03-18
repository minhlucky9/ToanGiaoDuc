using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour, IPanel
{
    public static LoginPanel INSTANCE;
    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
    }
    private GameObject panelChild;
    [SerializeField]
    private InputField usernameInputField;
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
    public void ButtonStart()
    {
        string uid = usernameInputField.text;
        uid = uid.Trim();
        for (int i = 0; i < uid.Length; i++)
        {
            if (uid[i].Equals(" "))
            {
                uid.Remove(i, 1);
            }
        }
        if (uid.Length < 6)
        {
            MessageCallBackPopupPanel.INSTACNE.Active("Tên đăng nhập phải chứa ít nhất 6 kí tự, không tính khoảng cách!");
            return;
        }
        User user = new User();
        user.Id = uid;
        UserManage.Instance.CurrentUser = user;
        Debug.Log("Save user & start clicked!");
        SceneLoader.INSTANCE.LoadScene(1);
        //Deactive();
        //HomePanel.Instance.Active();
        //UIManager.Instance.SetCurrentPanel(UIManager.EnabledPanel.Home);
    }
    public void ButtonQuit()
    {
        MessageCallBackPopupPanel.INSTACNE.Active("Bạn muốn quit à?", QuitApplication);
    }
    private void QuitApplication(bool canQuit)
    {
        if (canQuit == true)
        {
            Debug.Log("Quit");
        }
        else
        {
            Debug.Log("Stay");
        }
    }
}
