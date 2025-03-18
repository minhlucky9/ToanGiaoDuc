using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_AchievementManager : MonoBehaviour
{
    public static Game_AchievementManager Instance;
    [SerializeField]
    private List<MonHoc_Achievement> listMonHocAchievement;
    public List<MonHoc_Achievement> ListMonHoc { get { return listMonHocAchievement; } }
    [SerializeField]
    private MonHoc_Achievement _currentMonHocAchievement;
    public MonHoc_Achievement CurrentMonHocAchivement { get { return _currentMonHocAchievement; } }
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this); 
    }

}
