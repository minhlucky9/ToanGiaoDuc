using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BaiHoc 
{
    [SerializeField]
    private string maBaiHoc;
    [SerializeField]
    private string tenBaiHoc;
    [SerializeField]
    private Sprite imageIcon;
    [SerializeField]
    private List<TietHoc> listTietHoc;
    public string GetMaBaiHoc()
    {
        return maBaiHoc;
    }
    public string GetTenBaiHoc()
    {
        return tenBaiHoc;
    }
    public Sprite GetImageIcon()
    {
        return imageIcon;
    }
    public List<TietHoc> GetListTietHoc()
    {
        return listTietHoc;
    }
    public TietHoc GetTietHocAt(int index)
    {
        if(index < listTietHoc.Count)
        {
            return listTietHoc[index];
        }
        return null;
    }
    public TietHoc GetTietHocWithID(string id)
    {
        for(int i=0; i<listTietHoc.Count; i++)
        {
            if (listTietHoc[i].GetMaTietHoc().Equals(id))
            {
                return listTietHoc[i];
            }
        }
        return null;
    }
}
