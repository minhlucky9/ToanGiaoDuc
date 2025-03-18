using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_AreaReceiverManagerLT4 : MonoBehaviour
{
    [SerializeField] List<SS_AreaReceiverLT4> listArea;

    public List<SS_AreaReceiverLT4> GetList()
    {
        return listArea;
    }
}
