using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_RandomLT7 : MonoBehaviour
{
    [Range(1, 9)]
    public int numberOfQuestion = 4;

    public List<Transform> Spawnpoints;

    public enum oper { Null, Bigger, Equal, Smaller }
    public int variant = 1;
    public int WrongCount = 0;
    public bool canAnswer = false;

    private void Start()
    {
        int RandomA = 0, RandomB = 0;
        for (int i = 0; i < numberOfQuestion; i++)
        {
            Spawnpoints[i].gameObject.SetActive(true);
            
            if(variant == 1)
            {
                RandomA = Random.Range(0, 11);
                RandomB = Random.Range(0, 11);
            }
            else if (variant == 2)
            {
                RandomA = Random.Range(9, 99);
                RandomB = Random.Range(9, 99);
            }
            

            Spawnpoints[i].GetChild(0).GetComponent<Text>().text = RandomA.ToString();
            Spawnpoints[i].GetChild(1).GetComponent<Text>().text = RandomB.ToString();

            if(RandomA > RandomB) { Spawnpoints[i].GetComponentInChildren<SS_InputOperatorLT7>().neededOper = oper.Bigger; }
            else if(RandomA < RandomB) { Spawnpoints[i].GetComponentInChildren<SS_InputOperatorLT7>().neededOper = oper.Smaller; }
            else { Spawnpoints[i].GetComponentInChildren<SS_InputOperatorLT7>().neededOper = oper.Equal; }
        }
    }
}
