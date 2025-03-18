using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonHoc_Achievement : MonoBehaviour
{
    [SerializeField]
    AchivementManager[] _listBaiHocAchievementManager;
    public AchivementManager[] ListBaiHocAchievement { get { return _listBaiHocAchievementManager; } }
    public SaveObject_MonHocAchievement ToSaveObject()
    {
        Debug.Log("MonHoc_Achievement: ToSaveObject!");
        SaveObject_MonHocAchievement t = new SaveObject_MonHocAchievement();

        SaveObject_AchievementManager[] listBaiHoc = new SaveObject_AchievementManager[_listBaiHocAchievementManager.Length];
        for (int i = 0; i < _listBaiHocAchievementManager.Length; i++)
        {
            listBaiHoc[i] = new SaveObject_AchievementManager();
            List<Achivement> listAchieve = _listBaiHocAchievementManager[i].GetListAchivement();
            SaveObject_Achievement[] listAchievement = new SaveObject_Achievement[listAchieve.Count];
            for (int j = 0; j > listAchieve.Count; j++)
            {
                listAchievement[j] = listAchieve[j].ToSaveObject();
                Debug.Log(JsonUtility.ToJson(listAchievement[j]));
            }
            listBaiHoc[i].listAchievement = new SaveObject_Achievement[listAchievement.Length];
            listBaiHoc[i].listAchievement = listAchievement;
        }
        t._listBaiHocAchievementManager = listBaiHoc;
        return t;
    }
}
