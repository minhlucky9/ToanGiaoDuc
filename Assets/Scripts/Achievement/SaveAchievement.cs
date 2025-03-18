using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAchievement : MonoBehaviour
{
    const string FILENAME = "ToanHoc_Achievement";
    public void Save()
    {
        Debug.Log("Save click!");
        SaveObject_MonHocAchievement mon = Game_AchievementManager.Instance.CurrentMonHocAchivement.ToSaveObject() ;
        //string saveString = JsonUtility.ToJson(mon);
        //Debug.Log(saveString);
        //SaveSystem.Save(FILENAME, saveString);
    }
    public void Load()
    {
        SaveSystem.Load(FILENAME);
    }
}
