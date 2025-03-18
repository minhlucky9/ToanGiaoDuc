using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_RandomLT5 : MonoBehaviour
{
    int numOfItem; 
    public List<Transform> Spawnpoints;
    public Sprite[] RandomizeMainPicsprites;
    public GameObject Background;
    public Sprite[] BackgroundSpr;
    public Text questionText;
    public float waitTimeSound = 2.25f;
    public int FixedVariant = 1;
    [Range(0, 7)]
    public int FixedNumOfItem = 0;

    public enum Question {Null, Ascend, Descend }
    [HideInInspector] public Question question;
    [Range(0, 100)] public int percentageBigToSmall = 50;
    [HideInInspector] public List<int> NumberNeeded = new List<int>();
    private void Start()
    {
        //Rand Number of item
        if(FixedNumOfItem == 0)
        {
            numOfItem = Random.Range(4, 8);
        }
        else
        {
            numOfItem = FixedNumOfItem;
        }

        GetComponentInParent<SoSanh_LT5_lvlMap>().numOfItems = numOfItem;
        //SetUp layout
        CustomSpriteLayout[] layout = GetComponentsInChildren<CustomSpriteLayout>();
        foreach (CustomSpriteLayout item in layout)
        {
            item.numOfItem = numOfItem;
            item.Run();
        }

        //Random number variant
        int RandVariant=1;
        if (FixedVariant == 0) { Random.Range(1, 3); }
        else { RandVariant = FixedVariant; }

        switch (RandVariant)
        {
            case 1:
                for (int i = 0; i < numOfItem; i++)
                {
                    int _num;
                    do
                    {
                        _num = Random.Range(1, 11);
                    }
                    while (NumberNeeded.Contains(_num));
                    Spawnpoints[i].GetComponent<SpriteRenderer>().sprite = RandomizeMainPicsprites[_num - 1];
                    Spawnpoints[i].GetComponent<SS_ItemMoveLT5>().itemValue = _num;
                    NumberNeeded.Add(_num);
                }
                Background.GetComponent<SpriteRenderer>().sprite = BackgroundSpr[0];
                break;
            case 2:
                for (int i = 0; i < numOfItem; i++)
                {
                    int _num;
                    do
                    {
                        _num = Random.Range(1, 11);
                    }
                    while (NumberNeeded.Contains(_num));
                    Spawnpoints[i].GetComponent<SpriteRenderer>().sprite = RandomizeMainPicsprites[_num + 9];
                    Spawnpoints[i].GetComponent<SS_ItemMoveLT5>().itemValue = _num;
                    NumberNeeded.Add(_num);
                }
                Background.GetComponent<SpriteRenderer>().sprite = BackgroundSpr[1];
                break;
            case 3:
                for (int i = 0; i < numOfItem; i++)
                {
                    int _num;
                    do
                    {
                        _num = Random.Range(9, 99);
                    }
                    while (NumberNeeded.Contains(_num));
                    Spawnpoints[i].GetComponent<SpriteRenderer>().sprite = RandomizeMainPicsprites[Random.Range(0,RandomizeMainPicsprites.Length)];
                    Spawnpoints[i].GetChild(0).GetChild(0).GetComponent<Text>().text = _num.ToString();
                    Spawnpoints[i].GetComponent<SS_ItemMoveLT5>().itemValue = _num;
                    NumberNeeded.Add(_num);
                }
                break;
        }

        if(Random.Range(0,100) < percentageBigToSmall)
        {
            //descend
            question = Question.Descend;
        }
        else
        {
            question = Question.Ascend;
        }
        switch (question)
        {
            case Question.Ascend:
                NumberNeeded.Sort((a, b) => a.CompareTo(b));
                questionText.text = "                 BÉ          LỚN";
                StartCoroutine(audioPlay(waitTimeSound, "LT5", 0));
                break;
            case Question.Descend:
                NumberNeeded.Sort((a, b) => b.CompareTo(a));
                questionText.text = "                LỚN        BÉ";
                StartCoroutine(audioPlay(waitTimeSound, "LT5", 1));
                break;
        }
        layout[1].inputValue(NumberNeeded);
    }

    IEnumerator audioPlay(float waitBefore, string s, int id)
    {
        yield return new WaitForSeconds(waitBefore);

        TimeLineManage.INSTANCE.Tiet3(s, id);

        yield return 0;
    }
}
