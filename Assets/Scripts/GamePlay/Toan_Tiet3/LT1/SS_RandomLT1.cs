using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_RandomLT1 : MonoBehaviour
{
    public GameObject background;
    public List<Sprite> PosibleBackground;
    public GameObject nextButton;

    [Header("Phase 1")]
    public List<GameObject> spawnpointsA;
    public List<GameObject> spawnpointsB;
    [Header("Phase 2")]
    int randomPhase2Question;
    public Transform Phase2;
    Transform Question;
    public Sprite CorrectSprite;
    public Sprite WrongSprite;
    int phase2ButtonIndexChoosed = 9;
    [Header("Phase 3")]
    public GameObject InputArea;
    [Header("Phase 4")]
    public GameObject OperatorInput;

    [Header("State")]
    public bool canConnect = false;
    public bool canChooseQuestion = false;
    public bool canWriteNumber = false;
    public bool canNextPhase = false;
    private int curPhase = 1;
    int startUp = 0;

    public int rightCount;
    public int wrongCount;
    public int wrongChoiceCount;
    public enum QuestionState {Null, Bigger, Smaller, Equal }
    private QuestionState winCondition;
    public enum asset{ tho_carot, khi_nha, bi_chau }
    public asset LTAsset;
    int IndexWin;

    [Header("Sprite")]
    public List<Sprite> bunny;
    public Sprite carot;
    public List<Sprite> monkey;
    public Sprite house;
    public List<Sprite> marble;
    public List<Sprite> vase;

    private void Start()
    {
        RandomPhase1();
        activateButton(new Vector3(-153.6f, -149f, 0f));
        Question = Phase2.GetChild(1);
    }
    private void Update()
    {
        if (GameManager.Instance.canPlay && startUp == 0)
        {
            startUp++;
            canConnect = true;
            canNextPhase = true;
        }
    }
    void RandomPhase1()
    {
        int randomNumberA = Random.Range(3, spawnpointsA.Count);
        int randomNumberB = Random.Range(3, spawnpointsB.Count);
        switch (LTAsset)
        {
            case asset.tho_carot:     //carot & tho
                do { randomNumberB = Random.Range(3, 7); } while (randomNumberA == randomNumberB);
                background.GetComponent<SpriteRenderer>().sprite = PosibleBackground[0];
                setUpSprite(spawnpointsA, randomNumberA, carot);
                setUpSprite(spawnpointsB, randomNumberB, bunny);
                break;
            case asset.khi_nha:     //khi & nha
                do { randomNumberB = Random.Range(3, 7); } while (randomNumberA == randomNumberB);
                background.GetComponent<SpriteRenderer>().sprite = PosibleBackground[1];
                setUpSprite(spawnpointsA, randomNumberA, monkey);
                setUpSprite(spawnpointsB, randomNumberB, house);
                break;
            case asset.bi_chau:     //bi & chau
                background.GetComponent<SpriteRenderer>().sprite = PosibleBackground[2];
                setUpSprite(spawnpointsA, randomNumberA, marble);
                setUpSprite(spawnpointsB, randomNumberB, vase);
                break;
        }

        if (randomNumberA > randomNumberB)
        {
            winCondition = QuestionState.Bigger;
        }
        else if (randomNumberA < randomNumberB)
        {
            winCondition = QuestionState.Smaller;
        }
        else
        {
            winCondition = QuestionState.Equal;
        }

        SS_InputAreaLT1 oper = OperatorInput.GetComponent<SS_InputAreaLT1>();
        SS_InputAreaLT1 area1 = InputArea.transform.GetChild(0).GetComponent<SS_InputAreaLT1>();
        SS_InputAreaLT1 area2 = InputArea.transform.GetChild(1).GetComponent<SS_InputAreaLT1>();
        oper.operatorNeeded = winCondition;
        area1.leftNum = randomNumberA;
        area2.leftNum = randomNumberA;
        area1.rightNum = randomNumberB;
        area2.rightNum = randomNumberB;
    }

    void setUpSprite(List<GameObject> Obj, int length, List<Sprite> sprite)
    {
        for (int i = 0; i < length; i++)
        {
            Obj[i].SetActive(true);
            Obj[i].GetComponent<SpriteRenderer>().sprite = sprite[Random.Range(0, sprite.Count)];
        }
    }
    void setUpSprite(List<GameObject> Obj, int length, Sprite sprite)
    {
        for (int i = 0; i < length; i++)
        {
            Obj[i].SetActive(true);
            Obj[i].GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    void activateButton(Vector3 pos)
    {
        nextButton.GetComponent<RectTransform>().localPosition = pos;
        nextButton.SetActive(true);
    }
    void deactivateButton()
    {
        nextButton.SetActive(false);
    }

    //for button
    public void nextPhase()
    {
        if (canNextPhase)
        {
            curPhase++;
            canNextPhase = false;
            deactivateButton();
            switch (curPhase)
            {
                case 2:
                    canConnect = false;
                    StartCoroutine(audioPlay(1f, "LT1", 1));
                    RandomPhase2();
                    activateButton(new Vector3(126, -143, 0));
                    break;
                case 3:
                    if(phase2ButtonIndexChoosed == IndexWin) { rightCount++; }
                    else                                     { wrongCount++; }
                    canChooseQuestion = false;
                    canWriteNumber = true;
                    StartCoroutine(audioPlay(1f, "LT1", 2));
                    InputArea.SetActive(true);
                    activateButton(new Vector3(-6, -151.3f, 0));
                    break;
                case 4:
                    StartCoroutine(audioPlay(1f, "LT1", 3));
                    canWriteNumber = false;
                    OperatorInput.SetActive(true);
                    break;
            }
        }
    }

    void RandomPhase2()
    {
        Phase2.gameObject.SetActive(true);
        switch (LTAsset)
        {
            case asset.tho_carot: //tho
                Question.GetChild(2).gameObject.SetActive(false);
                randomPhase2Question = Random.Range(0, 2);
                if (randomPhase2Question == 0)
                {
                    questionRandom("Số Cà rốt ÍT HƠN số Thỏ?", "Số Cà rốt NHIỀU HƠN số Thỏ?");
                    StartCoroutine(audioPlay("LT1_Carot I Tho", "LT1_Carot N Tho", 4.5f, 3f, "Nam", "Nu"));
                    switch (winCondition)
                    {
                        case QuestionState.Bigger:
                            IndexWin = 1;
                            break;
                        case QuestionState.Smaller:
                            IndexWin = 0;
                            break;
                    }
                }
                else
                {
                    questionRandom("Số Cà rốt NHIỀU HƠN số Thỏ?", "Số Cà rốt ÍT HƠN số Thỏ?");
                    StartCoroutine(audioPlay("LT1_Carot N Tho", "LT1_Carot I Tho", 4.25f, 3f, "Nu", "Nam"));
                    switch (winCondition)
                    {
                        case QuestionState.Bigger:
                            IndexWin = 0;
                            break;
                        case QuestionState.Smaller:
                            IndexWin = 1;
                            break;
                    }
                }
                break;
            case asset.khi_nha: //khi
                Question.GetChild(2).gameObject.SetActive(false);
                randomPhase2Question = Random.Range(0, 2);
                if (randomPhase2Question == 0)
                {
                    questionRandom("Số Khỉ ÍT HƠN số Nhà?", "Số Khỉ NHIỀU HƠN số Nhà?");
                    StartCoroutine(audioPlay("LT1_Khi I Nha", "LT1_Khi N Nha", 4.25f, 2.5f, "Nam", "Nu"));
                    switch (winCondition)
                    {
                        case QuestionState.Bigger:
                            IndexWin = 1;
                            break;
                        case QuestionState.Smaller:
                            IndexWin = 0;
                            break;
                    }
                }
                else
                {
                    questionRandom("Số Khỉ NHIỀU HƠN số Nhà?", "Số Khỉ ÍT HƠN số Nhà?");
                    StartCoroutine(audioPlay("LT1_Khi N Nha", "LT1_Khi I Nha", 4.25f, 2.5f, "Nu", "Nam"));
                    switch (winCondition)
                    {
                        case QuestionState.Bigger:
                            IndexWin = 0;
                            break;
                        case QuestionState.Smaller:
                            IndexWin = 1;
                            break;
                    }
                }
                break;
            case asset.bi_chau: //bi
                randomPhase2Question = Random.Range(0, 3);
                if (randomPhase2Question == 0)
                {
                    questionRandom("Số Bi ÍT HƠN số Chậu?", "Số Bi NHIỀU HƠN số Chậu?", "Số Bi BẰNG số Chậu?");
                    StartCoroutine(audioPlay("LT1_Bi I Chau", "LT1_Bi N Chau", "LT1_Bi B Chau", 4.25f, 2.5f, "Nam", "Nu", "Nam"));
                    switch (winCondition)
                    {
                        case QuestionState.Bigger:
                            IndexWin = 1;
                            break;
                        case QuestionState.Smaller:
                            IndexWin = 0;
                            break;
                        case QuestionState.Equal:
                            IndexWin = 2;
                            break;
                    }
                }
                else if (randomPhase2Question == 1)
                {
                    questionRandom("Số Bi NHIỀU HƠN số Chậu?", "Số Bi BẰNG số Chậu?", "Số Bi ÍT HƠN số Chậu?");
                    StartCoroutine(audioPlay("LT1_Bi N Chau", "LT1_Bi B Chau", "LT1_Bi I Chau", 4.25f, 2.5f, "Nu", "Nam", "Nam"));
                    switch (winCondition)
                    {
                        case QuestionState.Bigger:
                            IndexWin = 0;
                            break;
                        case QuestionState.Smaller:
                            IndexWin = 2;
                            break;
                        case QuestionState.Equal:
                            IndexWin = 1;
                            break;
                    }
                }
                else
                {
                    questionRandom("Số Bi BẰNG số Chậu?", "Số Bi ÍT HƠN số Chậu?", "Số Bi NHIỀU HƠN số Chậu?");
                    StartCoroutine(audioPlay("LT1_Bi B Chau", "LT1_Bi I Chau", "LT1_Bi N Chau", 4.25f, 2.5f, "Nam", "Nam", "Nu"));
                    switch (winCondition)
                    {
                        case QuestionState.Bigger:
                            IndexWin = 2;
                            break;
                        case QuestionState.Smaller:
                            IndexWin = 1;
                            break;
                        case QuestionState.Equal:
                            IndexWin = 0;
                            break;
                    }
                }
                break;
        }
    }

    public void WinConditionPhase2(int buttonIndex)
    {
        if (canChooseQuestion)
        { 
            bool isRight = true;
            if (buttonIndex != IndexWin)
            {
                wrongChoiceCount++;
                isRight = false;
            }

            if (GameManager.Instance.canShowReaction)
            {
                if (isRight)
                {
                    TimeLineManage.INSTANCE.RightPlay();
                }
                else
                {
                    TimeLineManage.INSTANCE.WrongPlay();
                }
            }


            phase2ButtonIndexChoosed = buttonIndex;
            for (int i = 0; i < 3; i++)
            {
                Sprite sprite;

                if (i == buttonIndex) { sprite = CorrectSprite; }
                else { sprite = WrongSprite; }

                Question.GetChild(i).GetComponent<Image>().sprite = sprite;
            }
            StopAllCoroutines();
            switch (LTAsset)
            {
                case asset.tho_carot: //tho
                    if (randomPhase2Question == 0)
                    {
                        if(buttonIndex == 0)
                        {
                            StartCoroutine(audioPlay("LT1_Carot I Tho", .1f, "Nam"));
                        }
                        else
                        {
                            StartCoroutine(audioPlay("LT1_Carot N Tho", .1f, "Nu"));
                        }
                    }
                    else
                    {
                        if (buttonIndex == 1)
                        {
                            StartCoroutine(audioPlay("LT1_Carot I Tho", .1f, "Nam"));
                        }
                        else
                        {
                            StartCoroutine(audioPlay("LT1_Carot N Tho", .1f, "Nu"));
                        }
                    }
                    break;
                case asset.khi_nha: //khi
                    if (randomPhase2Question == 0)
                    {
                        if (buttonIndex == 0)
                        {
                            StartCoroutine(audioPlay("LT1_Khi I Nha", .1f, "Nam"));
                        }
                        else
                        {
                            StartCoroutine(audioPlay("LT1_Khi N Nha", .1f, "Nu"));
                        }
                    }
                    else
                    {
                        if (buttonIndex == 1)
                        {
                            StartCoroutine(audioPlay("LT1_Khi I Nha", .1f, "Nam"));
                        }
                        else
                        {
                            StartCoroutine(audioPlay("LT1_Khi N Nha", .1f, "Nu"));
                        }
                    }
                    break;
                case asset.bi_chau: //bi
                    if (randomPhase2Question == 0) 
                    { 
                        if (buttonIndex == 0)
                        {
                            StartCoroutine(audioPlay("LT1_Bi I Chau", .1f, "Nam"));
                        }
                        else if (buttonIndex == 1)
                        {
                            StartCoroutine(audioPlay("LT1_Bi N Chau", .1f, "Nu"));
                        }
                        else
                        {
                            StartCoroutine(audioPlay("LT1_Bi B Chau", .1f, "Nam"));
                        }
                    }
                    else if (randomPhase2Question == 1)
                    {
                        if (buttonIndex == 0)
                        {
                            StartCoroutine(audioPlay("LT1_Bi N Chau", .1f, "Nu"));
                        }
                        else if (buttonIndex == 2)
                        {
                            StartCoroutine(audioPlay("LT1_Bi I Chau", .1f, "Nam"));
                        }
                        else
                        {
                            StartCoroutine(audioPlay("LT1_Bi B Chau", .1f, "Nam"));
                        }
                    }
                    else
                    {
                        if (buttonIndex == 1)
                        {
                            StartCoroutine(audioPlay("LT1_Bi I Chau", .1f, "Nam"));
                        }
                        else if (buttonIndex == 2)
                        {
                            StartCoroutine(audioPlay("LT1_Bi N Chau", .1f, "Nu"));
                        }
                        else
                        {
                            StartCoroutine(audioPlay("LT1_Bi B Chau", .1f, "Nam"));
                        }
                    }
                    break;
            }
        }
    }
    IEnumerator audioPlay(string s, float waitBefore, string gender)
    {
        Tiet3AudioManager.instance.Stop();

        yield return new WaitForSeconds(waitBefore);
        Tiet3AudioManager.instance.Play(s);

        TimeLineManage.INSTANCE.Tiet3("Blank", GenderCheck(gender));

        yield return 0;
    }
    IEnumerator audioPlay(float waitBefore, string lvl, int id)
    {
        yield return new WaitForSeconds(waitBefore);
        TimeLineManage.INSTANCE.Tiet3(lvl, id);

        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        canNextPhase = true;
        yield return 0;
    }
    IEnumerator audioPlay(string question1, string question2, float waitBefore, float waitPerQuestion, string gender1, string gender2)
    {
        Tiet3AudioManager.instance.Stop();
        canChooseQuestion = false;
        canNextPhase = false;
        yield return new WaitForSeconds(waitBefore);

        Question.GetChild(0).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play(question1);
        TimeLineManage.INSTANCE.Tiet3("Blank", GenderCheck(gender1));
        yield return new WaitForSeconds(waitPerQuestion);

        Question.GetChild(1).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play(question2);
        TimeLineManage.INSTANCE.Tiet3("Blank", GenderCheck(gender2));

        canNextPhase = true;
        canChooseQuestion = true;
        yield return 0;
    }
    IEnumerator audioPlay(string question1, string question2, string question3, float waitBefore, float waitPerQuestion, string gender1, string gender2, string gender3)
    {
        Tiet3AudioManager.instance.Stop();
        canChooseQuestion = false;
        canNextPhase = false;
        yield return new WaitForSeconds(waitBefore);

        Question.GetChild(0).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play(question1);
        TimeLineManage.INSTANCE.Tiet3("Blank", GenderCheck(gender1));
        yield return new WaitForSeconds(waitPerQuestion);

        Question.GetChild(1).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play(question2);
        TimeLineManage.INSTANCE.Tiet3("Blank", GenderCheck(gender2));
        yield return new WaitForSeconds(waitPerQuestion);

        Question.GetChild(2).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play(question3);
        TimeLineManage.INSTANCE.Tiet3("Blank", GenderCheck(gender3));

        canNextPhase = true;
        canChooseQuestion = true;
        yield return 0;
    }

    int GenderCheck(string gen)
    {
        if(gen == "Nam")
        {
            return 0;
        }
        else if (gen =="Nu")
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    void questionRandom(string question1, string question2)
    {
        Question.GetChild(0).GetComponentInChildren<Text>().text = question1;
        Question.GetChild(1).GetComponentInChildren<Text>().text = question2;
    }
    void questionRandom(string question1, string question2, string question3)
    {
        Question.GetChild(0).GetComponentInChildren<Text>().text = question1;
        Question.GetChild(1).GetComponentInChildren<Text>().text = question2;
        Question.GetChild(2).GetComponentInChildren<Text>().text = question3;
    }
}
