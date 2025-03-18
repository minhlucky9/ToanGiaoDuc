using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_AreaReceiverManagerLT5 : MonoBehaviour
{
    [SerializeField] List<SS_AreaReceiverLT5> listArea;

    public List<SS_AreaReceiverLT5> GetList()
    {
        return listArea;
    }
}
