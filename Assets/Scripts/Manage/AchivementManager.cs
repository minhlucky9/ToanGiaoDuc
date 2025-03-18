using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    public static AchivementManager INSTANCE;
    [SerializeField]
    private List<Achivement> listAchievement;
    public TietHoc tietHoc { get; set; }
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        //if(INSTANCE == null)
        //{
        //    INSTANCE = this;
        //}
        //else
        //{
        //    Destroy(this);
        //}
    }
    private void Start()
    {
        listAchievement = new List<Achivement>();
    }
    public void AddAchiveMent(Achivement achivement)
    {
        bool isAdd = false;
        for(int i=0; i<listAchievement.Count; i++)
        {
            if(listAchievement[i].GameMode.Equals(achivement.GameMode) && listAchievement[i].LevelName.Equals(achivement.LevelName))
            {
                if(achivement.GameMode == ModeLessonEnum.SelfExamination)
                {
                    if(achivement.HardMode == listAchievement[i].HardMode)
                    {
                        listAchievement.Remove(listAchievement[i]);
                        listAchievement.Insert(i, achivement);
                        isAdd = true;
                        break;
                    }
                }
                else
                {
                    listAchievement.Remove(listAchievement[i]);
                    listAchievement.Insert(i, achivement);
                    isAdd = true;
                    break;
                }
            }
        }
        if (!isAdd)
        {
            listAchievement.Add(achivement);
        }
    }
    public List<Achivement> GetListAchivement()
    {
        return listAchievement;
    }
    public Achivement GetAchivementAtPosition(int index)
    {
        if(index >=0 && index < listAchievement.Count)
        {
            return listAchievement[index];
        }
        return null;
    }
}
