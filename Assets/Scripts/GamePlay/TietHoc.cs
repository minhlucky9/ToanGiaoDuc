using UnityEngine;
[System.Serializable]
public class TietHoc
{
    [SerializeField]
    private int idTietHoc;

    [SerializeField]
    private string maTietHoc;

    [SerializeField]
    private string tenTietHoc;

    [SerializeField]
    private Sprite imageIcon;

    [SerializeField]
    LevelManager levelManage;
    public AchivementManager achievementManager;
    [SerializeField]
    BookExport bookExport;
    [SerializeField]
    RateLesson rateLesson;
    public int GetTietHocId()
    {
        return idTietHoc;
    }

    public void SetTietHocId(int idz)
    {
        idTietHoc = idz;
    }

    public string GetMaTietHoc()
    {
        return maTietHoc;
    }

    public string GetTenTietHoc()
    {
        return tenTietHoc;
    }

    public Sprite GetImageIcon()
    {
        return imageIcon; 
    }
    public void SetInstances()
    {
        Debug.Log("Set instance levelmanager");
        LevelManager.INSTANCE = levelManage;
        AchivementManager.INSTANCE = achievementManager;
        //BookExportManage.Instance.SelectBookExport(bookExport);
        RateManage.Instance.SetCurrentRateLesson(rateLesson);
    }
}
