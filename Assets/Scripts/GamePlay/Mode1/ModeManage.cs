using System.Collections.Generic;
using UnityEngine;
public class ModeManage : MonoBehaviour
{
    public static ModeManage INSTANCE;

    [SerializeField]
    private List<AreaPlay> listAreaMode1;

    [SerializeField]
    private List<AreaPlay> listAreaMode2;

    [SerializeField]
    private List<AreaPlay> listAreaMode3;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
    }

    public List<AreaPlay> GetListArea(int mode)
    {
        switch (mode)
        {
            case 1: return listAreaMode1;
            case 2: return listAreaMode2;
            case 3: return listAreaMode3;
            default: return null;
        }
    }
}
