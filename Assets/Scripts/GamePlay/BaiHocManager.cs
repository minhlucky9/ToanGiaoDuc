


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaiHocManager : MonoBehaviour
{
    public static BaiHocManager INSTANCE;

    [SerializeField]
    private List<BaiHoc> listBaiHoc;


    private void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        
    }

    public List<BaiHoc> GetListBaiHoc()
    {
        return listBaiHoc;
    }
    public List<TietHoc> GetListTietHocByBaiHocID(string baiHocID)
    {
        for(int i=0; i<listBaiHoc.Count; i++)
        {
            if (baiHocID.Equals(listBaiHoc[i].GetMaBaiHoc()))
            {
                return listBaiHoc[i].GetListTietHoc();

            }
        }
        return new List<TietHoc>();
    }
    void Init()
    {
        for(int i=0; i<listBaiHoc.Count; i++)
        {
            List<TietHoc> listTietHoc = listBaiHoc[i].GetListTietHoc();
            for(int j=0; j<listTietHoc.Count; j++)
            {
                TietHoc th = listTietHoc[j];
                th.achievementManager.tietHoc = th;
            }
        }
    }
}
