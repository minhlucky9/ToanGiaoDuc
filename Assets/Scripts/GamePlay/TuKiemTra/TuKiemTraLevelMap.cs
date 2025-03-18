using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuKiemTraLevelMap : MonoBehaviour, LevelMap
{
    public GameObject[] listLevel;
    [SerializeField]
    private int currentId=0;
    public void OnFinishLevel()
    {
        if(currentId == listLevel.Length)
        {
            GameManager.Instance.isDoneAll = true;
            listLevel[currentId].GetComponent<LevelMap>().OnFinishLevel();
        }
        else
        {
            listLevel[currentId].GetComponent<LevelMap>().OnFinishLevel();
            currentId++;
        }
    }

    public void OnQuitLevel()
    {
        
    }

    public void OnStartLevel()
    {
        listLevel[currentId].GetComponent<LevelMap>().OnStartLevel();
    }

    public void Setup()
    {
        
    }
}
