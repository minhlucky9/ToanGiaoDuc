using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_AreaReceiverManager_KhamPha : MonoBehaviour
{
    [SerializeField] List<SS_AreaReceiver_KhamPha> listArea;

    public List<SS_AreaReceiver_KhamPha> GetList()
    {
        return listArea;
    }
}
