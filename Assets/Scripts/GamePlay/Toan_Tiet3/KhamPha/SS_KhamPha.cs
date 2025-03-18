using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_KhamPha : MonoBehaviour
{
    public Sprite[] backgroundSprite;
    public Sprite CorrectSprite;
    public Sprite WrongSprite;
    public Image[] buttonImage;
    public GameObject nextButton;
    public int curPhase = 1;

    [Header("Phase1")]
    public GameObject Phase1_1;
    public GameObject Phase1_2;
    public Transform Phase1_2_Question;
    public GameObject Phase1_3;
    public GameObject Phase1_3_Question;

    [Header("Phase2")]
    public GameObject Phase2_1;
    public SS_InputArea_KhamPha[] inputArea_Phase2_1;
    public GameObject Phase2_1_Question;
    public Transform Phase2_1_Extra;

    public GameObject Phase2_2;
    public SS_InputArea_KhamPha[] inputArea_Phase2_2;
    public GameObject Phase2_2_Question;
    public Transform Phase2_2_Extra;

    [Header("Phase3")]
    public GameObject Phase3;
    public GameObject Phase3_Side;

    [Header("Stats")]
    public int RightCount;
    public int WrongCount;
    public int WrongChoiceCount;
    public bool canNextPhase = false;
    public bool canChooseQuestion = false;
    public bool canMoveItem = false;
    public bool canInputNum = false;
    public bool canConnectItem = false;

    public List<MovableItem> items = new List<MovableItem>();
    public List<SS_ItemConnectLT1> itemsC = new List<SS_ItemConnectLT1>();
    private bool startUp = true;
    private bool answer_1 = false;

    private void Start()
    {
        CreatePhase1_1();
        Phase1_3.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.canPlay && startUp)
        {
            startUp = false;
            setUpStats(true, false, true, false, false);
        }
    }

    void CreatePhase1_1()
    {
        setBackground(0); 
        activateButton(new Vector3(-6f, -145.1f, 0f));
        Phase1_1.SetActive(true);
    }

    IEnumerator CreatePhase1_2()
    {
        setUpStats();

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Phantich", 1);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1 + 1f);

        yield return new WaitForSeconds(.75f);

        Phase1_1.SetActive(false);
        Phase1_2.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Huongdan", 2);
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);
        yield return new WaitForSeconds(.5f);

        Phase1_2_Question.GetChild(0).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Lan_B I D");
        TimeLineManage.INSTANCE.Tiet3("Blank", 1);
        yield return new WaitForSeconds(1.75f);

        Phase1_2_Question.GetChild(1).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Hai_B N D");
        TimeLineManage.INSTANCE.Tiet3("Blank", 0);
        yield return new WaitForSeconds(1.75f);

        activateButton(new Vector3(170f, -87f, 0));
        yield return new WaitForSeconds(.5f);
        setUpStats(true, true, false, false, false);
    }

    IEnumerator CreatePhase1_3()
    {
        setUpStats();

        Phase1_2.SetActive(false);
        activateButton(new Vector3(170f, -98f, 0));
        Phase1_3.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Huongdan", 3);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        yield return new WaitForSeconds(.5f);
        setUpStats(true, false, true, false, false);
    }

    IEnumerator Phase1_3PT2()
    {
        Transform question = Phase1_3_Question.transform.GetChild(1);
        setUpStats();
        Phase1_3_Question.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("LT1", 1);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);
        yield return new WaitForSeconds(.5f);

        question.GetChild(0).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Lan_B I D");
        TimeLineManage.INSTANCE.Tiet3("Blank", 1);
        yield return new WaitForSeconds(1.75f);

        question.GetChild(1).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Hai_B N D");
        TimeLineManage.INSTANCE.Tiet3("Blank", 0);
        yield return new WaitForSeconds(1.75f);

        question.GetChild(2).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Lan_B B D");
        TimeLineManage.INSTANCE.Tiet3("Blank", 1);
        yield return new WaitForSeconds(1.75f);

        activateButton(new Vector3(170f, -98f, 0));
        yield return new WaitForSeconds(.5f);
        setUpStats(true, true, false, false, false);
    }

    IEnumerator CreatePhase2_1()
    {
        setUpStats();

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Phantich", 3);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1 + 1f);

        Phase1_3.SetActive(false);
        setBackground(1);
        activateButton(new Vector3(-33.4f, -149.9f, 0));
        Phase2_1.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Huongdan", 4);
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        yield return new WaitForSeconds(.5f);
        setUpStats(true, false, false, true, false);
    }
    
    IEnumerator Phase2_1PT2()
    {
        setUpStats();

        TimeLineManage.INSTANCE.Tiet3("LT1", 0);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        activateButton(new Vector3(-33.4f, -149.9f, 0));

        yield return new WaitForSeconds(.5f);
        setUpStats(true, false, false, false, true);
    }

    IEnumerator Phase2_1PT3()
    {
        Transform question = Phase2_1_Question.transform.GetChild(1);
        setUpStats();

        Phase2_1_Question.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("LT1", 1);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);
        yield return new WaitForSeconds(.5f);

        question.GetChild(0).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Lan_T I C");
        TimeLineManage.INSTANCE.Tiet3("Blank", 1);
        yield return new WaitForSeconds(1.75f);

        question.GetChild(1).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Hai_C I T");
        TimeLineManage.INSTANCE.Tiet3("Blank", 0);
        yield return new WaitForSeconds(1.75f);

        activateButton(new Vector3(171, -49, 0));
        yield return new WaitForSeconds(.5f);
        setUpStats(true, true, false, false, false);
    }

    IEnumerator CreatePhase2_2()
    {
        setUpStats();

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Phantich", 4);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();

        //4
        yield return new WaitForSeconds(.5f);
        inputArea_Phase2_1[0].gameObject.SetActive(false);
        Phase2_1_Extra.GetChild(0).gameObject.SetActive(true);

        //5
        yield return new WaitForSeconds(2.5f);
        inputArea_Phase2_1[1].gameObject.SetActive(false);
        Phase2_1_Extra.GetChild(1).gameObject.SetActive(true);
        //operator
        yield return new WaitForSeconds(3f);
        Phase2_1_Extra.GetChild(2).gameObject.SetActive(true);

        //the rest
        yield return new WaitForSeconds(7f);
        Phase2_1_Extra.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Phase2_1_Extra.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Phase2_1_Extra.GetChild(5).gameObject.SetActive(true);

        yield return new WaitForSeconds(((float)timeW1 - 14.5f));

        Phase2_1.SetActive(false);
        activateButton(new Vector3(-33.4f, -149.9f, 0));
        Phase2_2.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Huongdan", 4);
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        yield return new WaitForSeconds(.5f);
        setUpStats(true, false, false, true, false);
    }

    IEnumerator Phase2_2PT2()
    {
        setUpStats();

        TimeLineManage.INSTANCE.Tiet3("LT1", 0);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);

        activateButton(new Vector3(-33.4f, -149.9f, 0));

        yield return new WaitForSeconds(.5f);
        setUpStats(true, false, false, false, true);
    }

    IEnumerator Phase2_2PT3()
    {
        Transform question = Phase2_2_Question.transform.GetChild(1);
        setUpStats();

        Phase2_2_Question.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("LT1", 1);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();
        yield return new WaitForSeconds((float)timeW1);
        yield return new WaitForSeconds(.5f);

        question.GetChild(0).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Lan_T I C");
        TimeLineManage.INSTANCE.Tiet3("Blank", 1);
        yield return new WaitForSeconds(1.75f);

        question.GetChild(1).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Hai_T N C");
        TimeLineManage.INSTANCE.Tiet3("Blank", 0);
        yield return new WaitForSeconds(1.75f);

        question.GetChild(2).gameObject.SetActive(true);
        Tiet3AudioManager.instance.Play("Lan_T B C");
        TimeLineManage.INSTANCE.Tiet3("Blank", 1);
        yield return new WaitForSeconds(1.75f);

        activateButton(new Vector3(171, -99, 0));
        yield return new WaitForSeconds(.5f);
        setUpStats(true, true, false, false, false);
    }

    IEnumerator CreatePhase3()
    {
        setUpStats();

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Phantich", 5);
        double timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();

        //4
        yield return new WaitForSeconds(.5f);
        inputArea_Phase2_2[0].gameObject.SetActive(false);
        Phase2_2_Extra.GetChild(0).gameObject.SetActive(true);
        //5
        yield return new WaitForSeconds(3f);
        inputArea_Phase2_2[1].gameObject.SetActive(false);
        Phase2_2_Extra.GetChild(1).gameObject.SetActive(true);
        //operator
        yield return new WaitForSeconds(5f);
        Phase2_2_Extra.GetChild(2).gameObject.SetActive(true);

        yield return new WaitForSeconds(((float)timeW1 - 7.5f));

        Phase2_2.SetActive(false);
        setBackground(2);
        Phase3.SetActive(true);

        TimeLineManage.INSTANCE.Tiet3("KhamPha_Huongdan", 6);
        timeW1 = TimeLineManage.INSTANCE.GetDurationOfCurrentDirector();

        //side
        yield return new WaitForSeconds(12.75f);
        Phase3_Side.SetActive(true);

        yield return new WaitForSeconds((float)timeW1 - 12f);

        yield return new WaitForSeconds(.5f);
        setUpStats(true, false, false, true, true);
    }

    public void nextPhase()
    {
        if (canNextPhase)
        {
            curPhase++;
            deactiveButton();
            switch (curPhase)
            {
                case 2: // phase 1_2
                    StartCoroutine(CreatePhase1_2());
                    break;
                case 3: // phase 1_3 pt1
                    if (answer_1) { RightCount++; }
                    else { WrongCount++; }
                    StartCoroutine(CreatePhase1_3());
                    break;
                case 4: // phase 1_3 pt2
                    StartCoroutine(Phase1_3PT2());
                    break;
                case 5: // phase 2_1
                    if (answer_1) { RightCount++; }
                    else { WrongCount++; }
                    StartCoroutine(CreatePhase2_1());
                    break;
                case 6: // phase 2_1 pt2
                    foreach (SS_InputArea_KhamPha item in inputArea_Phase2_1)
                    {
                        if(item.AreaInputedNumber == item.neededNum)
                        {
                            RightCount++;
                        }
                        else
                        {
                            WrongCount++;
                        }
                    }
                    StartCoroutine(Phase2_1PT2());
                    break;
                case 7: // phase 2_1 pt3 
                    StartCoroutine(Phase2_1PT3());
                    break;
                case 8: // phase 2_2
                    if (answer_1) { RightCount++; }
                    else { WrongCount++; }
                    StartCoroutine(CreatePhase2_2());
                    break;
                case 9: // phase 2_2 pt2
                    foreach (SS_InputArea_KhamPha item in inputArea_Phase2_2)
                    {
                        if (item.AreaInputedNumber == item.neededNum)
                        {
                            RightCount++;
                        }
                        else
                        {
                            WrongCount++;
                        }
                    }
                    StartCoroutine(Phase2_2PT2());
                    break;
                case 10:// phase 2_2 pt3
                    StartCoroutine(Phase2_2PT3());
                    break;
                case 11: // phase 3
                    if (answer_1) { RightCount++; }
                    else { WrongCount++; }
                    StartCoroutine(CreatePhase3());
                    break;
            }
        }
    }

    public void SetButtonSprite(int index)
    {
        if (canChooseQuestion)
        {
            foreach (Image button in buttonImage)
            {
                if (button.gameObject.activeInHierarchy) { button.sprite = WrongSprite; }
            }
            buttonImage[index].sprite = CorrectSprite;
        }
    }
    public void CheckAnswerButton(bool ans)
    {
        if (canChooseQuestion)
        {
            bool isRight = true;
            if (!ans)
            {
                WrongChoiceCount++;
                isRight = false;
            }
            answer_1 = ans;

            if (isRight)
            {
                TimeLineManage.INSTANCE.RightPlay();
            }
            else
            {
                TimeLineManage.INSTANCE.WrongPlay();
            }
        }
    }
    public void readQuestion(int index)
    {
        Tiet3AudioManager.instance.Stop();
        switch (index)
        {
            case 0:
                Tiet3AudioManager.instance.Play("Lan_B I D");
                TimeLineManage.INSTANCE.Tiet3("Blank", 1);
                break;
            case 1:
                Tiet3AudioManager.instance.Play("Hai_B N D");
                TimeLineManage.INSTANCE.Tiet3("Blank", 0);
                break;
            case 2:
                Tiet3AudioManager.instance.Play("Lan_B I D");
                TimeLineManage.INSTANCE.Tiet3("Blank", 1);
                break;
            case 3:
                Tiet3AudioManager.instance.Play("Hai_B N D");
                TimeLineManage.INSTANCE.Tiet3("Blank", 0);
                break;
            case 4:
                Tiet3AudioManager.instance.Play("Lan_B B D");
                TimeLineManage.INSTANCE.Tiet3("Blank", 1);
                break;
            case 5:
                Tiet3AudioManager.instance.Play("Lan_T I C");
                TimeLineManage.INSTANCE.Tiet3("Blank", 1);
                break;
            case 6:
                Tiet3AudioManager.instance.Play("Hai_C I T");
                TimeLineManage.INSTANCE.Tiet3("Blank", 0);
                break;
            case 7:
                Tiet3AudioManager.instance.Play("Lan_T I C");
                TimeLineManage.INSTANCE.Tiet3("Blank", 1);
                break;
            case 8:
                Tiet3AudioManager.instance.Play("Hai_T N C");
                TimeLineManage.INSTANCE.Tiet3("Blank", 0);
                break;
            case 9:
                Tiet3AudioManager.instance.Play("Lan_T B C");
                TimeLineManage.INSTANCE.Tiet3("Blank", 1);
                break;
        }
    }
    void activateButton(Vector3 pos)
    {
        nextButton.GetComponent<RectTransform>().localPosition = pos;
        nextButton.SetActive(true);
    }
    void deactiveButton()
    {
        nextButton.SetActive(false);
    }
    void setBackground(int index)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = backgroundSprite[index];
    }

    void setUpStats(bool _canNextPhase = false, bool _canChooseQuestion = false, bool _canMoveItem = false, bool _canInputNum = false, bool _canConnectItem = false)
    {
        canNextPhase = _canNextPhase;
        canChooseQuestion = _canChooseQuestion;
        canMoveItem = _canMoveItem;
        canInputNum = _canInputNum;
        canConnectItem = _canConnectItem;

        foreach (MovableItem item in items)
        {
            if (item.gameObject.activeInHierarchy) { item.SetCanMoveItem(canMoveItem); } 
        }

        foreach (SS_ItemConnectLT1 item in itemsC)
        {
            if (item.gameObject.activeInHierarchy) { item.setCanConnect(canConnectItem); }
        }
    }
}
