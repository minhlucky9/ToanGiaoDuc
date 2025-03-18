using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayManage : MonoBehaviour
{
    public static UIGameplayManage INSTANCE;

    private void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
    }

}
