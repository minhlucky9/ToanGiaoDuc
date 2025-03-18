using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLessonFormatButton : BaseSelectButton
{
    [SerializeField]
    private BaiHoc baiHoc;

    public BaiHoc GetBaiHoc()
    {
        return baiHoc;
    }
    public void SetBaiHoc(BaiHoc bh)
    {
        baiHoc = bh;
    }
}
