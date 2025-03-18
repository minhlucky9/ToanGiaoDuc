using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_RandomLT2 : MonoBehaviour
{
    public List<Transform> Spawnpoints;
    public Sprite[] RandomizeMainPicsprites;
    public List<RandomizePics> RandomizeIndividualSprites;
    public Sprite[] QuestionIcon;
    public GameObject MainPic;
    public GameObject QuestionPic;
    public float waitTimeSound = 2.25f;
    int NumberNeeded;
    public Text mainText;

    public enum Question {Null, Bigger, Equal, Smaller}
    [HideInInspector] public Question question;

    public Question fixedQuestion;
    public bool inverted = false;

    private void Start()
    {
        Run();
    }

    public void Run()
    {
        //Random mainpic sprite && for sprite
        int TempRandomNum = Random.Range(0, 2);
        if (TempRandomNum == 0)
        {
            NumberNeeded = Random.Range(1, 11);
            randomMainPics(NumberNeeded, 0);
            mainText.text = NumberNeeded.ToString();

            //Random individual sprite and spawn
            int _RandomMagicNum = Random.Range(0, 3);   //0,1,2
            randomIndivSprites(_RandomMagicNum);
        }
        else
        {
            NumberNeeded = Random.Range(10, 21);
            randomMainPics(NumberNeeded, 10);
            mainText.text = (NumberNeeded - 10).ToString();

            //Random individual sprite and spawn
            int _RandomMagicNum = 3;
            randomIndivSprites(_RandomMagicNum);
        }

        //Random cau hoi
        int RandomQuestion = 4;
        switch (fixedQuestion)
        {
            case Question.Equal:
                if (!inverted)
                {
                    RandomQuestion = 2;
                }
                else
                {
                    if (NumberNeeded == 10) { RandomQuestion = 1; }
                    else if(NumberNeeded == 1) { RandomQuestion = 0; }
                    else
                    {
                        RandomQuestion = Random.Range(0, 2);
                    }
                }
                break;
        }

        if (RandomQuestion == 0)
        {
            Debug.Log("LT2bigger");
            QuestionPic.GetComponent<SpriteRenderer>().sprite = QuestionIcon[2];
            StartCoroutine(audioPlay(waitTimeSound, "LT2", 1));
            question = Question.Bigger;
        }
        else if(RandomQuestion == 1)
        {
            Debug.Log("LT2Smaller");
            QuestionPic.GetComponent<SpriteRenderer>().sprite = QuestionIcon[0];
            StartCoroutine(audioPlay(waitTimeSound, "LT2", 2));
            question = Question.Smaller;
        }
        else 
        {
            Debug.Log("LT2equal");
            QuestionPic.GetComponent<SpriteRenderer>().sprite = QuestionIcon[1];
            StartCoroutine(audioPlay(waitTimeSound, "LT2", 0));
            question = Question.Equal;
        }
    }

    IEnumerator audioPlay(float waitBefore, string s, int id)
    {
        yield return new WaitForSeconds(waitBefore);

        TimeLineManage.INSTANCE.Tiet3(s, id);

        yield return 0;
    }

    void randomMainPics(int index, int offset)
    {
        MainPic.GetComponent<SpriteRenderer>().sprite = RandomizeMainPicsprites[index - 1];
        GetComponentInChildren<SS_AreaReceiverLT2>().setItemCountRequire(index - offset);
        Debug.Log("Number needed " + (NumberNeeded - offset));
    }

    void randomIndivSprites(int index)
    {
        List<Sprite> spritesToRandomize = new List<Sprite>();
        spritesToRandomize.AddRange(RandomizeIndividualSprites[index].GetSprites());
        foreach (Transform location in Spawnpoints)
        {
            int Rng = Random.Range(0, spritesToRandomize.Count);
            location.GetComponent<SpriteRenderer>().sprite = spritesToRandomize[Rng];
        }
    }
}
