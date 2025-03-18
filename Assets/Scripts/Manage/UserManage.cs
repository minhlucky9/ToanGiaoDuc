using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManage : MonoBehaviour
{
    public static UserManage Instance;
    private User currentUser;
    public User CurrentUser
    {
        get
        {
            return currentUser;
        }
        set
        {
            currentUser = value;
        }
    }
    private void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
    }

}
