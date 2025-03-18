using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_RandomLT4 : MonoBehaviour
{
    public List<Transform> Spawnpoints;
    public Sprite[] RandomizeMainPicsprites;
    public Text questionText;
    public float waitTimeSound = 2.25f;
    public int variant = 1;
    [Range(0, 5)]
    public int FixedNumOfItem = 0;
    public enum Question { Biggest, Smallest }
    [HideInInspector] public Question question;
    [HideInInspector] public List<int> NumberNeeded;
    private void Start()
    {
        Run();
        switch (question)
        {
            case Question.Biggest:
                NumberNeeded.Sort((a, b) => b.CompareTo(a));
                break;
            case Question.Smallest:
                NumberNeeded.Sort((a, b) => a.CompareTo(b));
                break;
        }
    }

    public void Run()
    {
        int numOfFruit;
        //Random so sprite xuat hien, so cua sprite
        if (FixedNumOfItem == 0)
        {
            numOfFruit = Random.Range(3, 6);
        }
        else
        {
            numOfFruit = FixedNumOfItem;
        }

        int typOfFruit = Random.Range(0, RandomizeMainPicsprites.Length);
        NumberNeeded = new List<int>();
        for (int i = 0; i < numOfFruit; i++)
        {
            GameObject fruit = Spawnpoints[i].gameObject;
            fruit.SetActive(true);
            fruit.GetComponent<SpriteRenderer>().sprite = RandomizeMainPicsprites[typOfFruit];
            int _num = 0;
            do
            {
                if (variant == 1) { _num = Random.Range(1, 11); }
                else if (variant == 2) { _num = Random.Range(9, 99); }
            }
            while (NumberNeeded.Contains(_num));
            fruit.GetComponent<SS_ItemMoveLT4>().itemValue = _num;
            NumberNeeded.Add(_num);

            fruit.GetComponentInChildren<Text>().text = NumberNeeded[i].ToString();
        }

        //Random cau hoi
        int _question = Random.Range(0, 2);
        if(_question == 0)
        {
            question = Question.Biggest;
            StartCoroutine(audioPlay(waitTimeSound, "LT4", 1));
            questionText.text = " LỚN";
        }
        else
        {
            question = Question.Smallest;
            StartCoroutine(audioPlay(waitTimeSound, "LT4", 0));
            questionText.text = "  BÉ";
        }
    }

    IEnumerator audioPlay(float waitBefore, string s, int id)
    {
        yield return new WaitForSeconds(waitBefore);

        TimeLineManage.INSTANCE.Tiet3(s, id);

        yield return 0;
    }
}
