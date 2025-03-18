using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_RandomLT6 : MonoBehaviour
{
    public List<Transform> Spawnpoints;
    public List<Sprite> possibleSprites;
    public GameObject nextButton;

    [Header("Phase 1")]
    public List<SS_InputAreaLT6> inputAreas;

    [Header("Phase 2")]
    public Transform Phase2;
    public Text AText;
    public Text ComparText;
    public Text BText;
    public float MaxValueTimer = 25f;
    float _valueTimer;
    public Slider timer;
    bool startTimer;
    enum compar { Bigger, Smaller, Equal }
    compar comp;
    bool answer;

    [Header("States")]
    public bool ableToWrite = false;
    public bool ableToNextPhase = false;
    bool ableToAnswer;
    public int maxQuestion = 3;
    int CurQuestion = 1;
    public int rightCount;
    public int wrongCount;
    public int wrongChoiceCount;
    List<int> indexSpriteTaken = new List<int>();
    int startUp = 0;
    private void Start()
    {
        RandomPhase1();
        _valueTimer = MaxValueTimer;
    }

    void RandomPhase1()
    {
        for (int i = 0, j = 0; i < Spawnpoints.Count; i += 8, j++)
        {
            int RandomIndexOfItem = Random.Range(2, 8);

            int RandomIndexOfSprite;
            do { RandomIndexOfSprite = Random.Range(0, possibleSprites.Count); }
            while (indexSpriteTaken.Contains(RandomIndexOfSprite));
            indexSpriteTaken.Add(RandomIndexOfSprite);

            inputAreas[j].setNeededNum(RandomIndexOfItem + 1);

            for (int k = 0; k <= RandomIndexOfItem; k++)
            {
                Spawnpoints[i + k].gameObject.SetActive(true);
                Spawnpoints[i + k].GetComponent<SpriteRenderer>().sprite = possibleSprites[RandomIndexOfSprite];
            }
        }
    }

    private void Update()
    {
        if (GameManager.Instance.canPlay && startUp == 0)
        {
            startUp++;
            ableToWrite = true;
            ableToNextPhase = true;
        }

        if (startTimer)
        {
            if (_valueTimer > 0)
            {
                _valueTimer -= Time.deltaTime;
                timer.value = _valueTimer;
            }
            else
            {
                Answer(!answer);
            }
        }
    }

    void RandomPhase2()
    {
        Phase2.gameObject.SetActive(true);
        int randomAText = Random.Range(0, 4), randomBText;
        do { randomBText = Random.Range(0, 4); } while (randomAText == randomBText);

        string a = null, b = null, soundA = null, soundB = null;
        switch (randomAText)
        {
            case 0:
                a = "Số cây";
                soundA = "LT6_Cay";
                break;
            case 1:
                a = "Số nhà";
                soundA = "LT6_Nha";
                break;
            case 2:
                a = "Số con vật";
                soundA = "LT6_CV";
                break;
            case 3:
                a = "Số quả";
                soundA = "LT6_Qua";
                break;
        }
        switch (randomBText)
        {
            case 0:
                b = "Số cây";
                soundB = "LT6_Cay";
                break;
            case 1:
                b = "Số nhà";
                soundB = "LT6_Nha";
                break;
            case 2:
                b = "Số con vật";
                soundB = "LT6_CV";
                break;
            case 3:
                b = "Số quả";
                soundB = "LT6_Qua";
                break;
        }
        string Comp = RandomQuestion(a, b);

        SS_InputAreaLT6 ATextInputArea = inputAreas[indexSpriteTaken.IndexOf(randomAText)];
        SS_InputAreaLT6 BTextInputArea = inputAreas[indexSpriteTaken.IndexOf(randomBText)];
        int ATextNeededNum = ATextInputArea.getNeededNum();
        int BTextNeededNum = BTextInputArea.getNeededNum();
        switch (comp)
        {
            case compar.Bigger:
                if (ATextNeededNum > BTextNeededNum) { answer = true; }
                else { answer = false; }
                break;
            case compar.Smaller:
                if (ATextNeededNum < BTextNeededNum) { answer = true; }
                else { answer = false; }
                break;
            case compar.Equal:
                if (ATextNeededNum == BTextNeededNum) { answer = true; }
                else { answer = false; }
                break;
        }

        if (CurQuestion == 1) { StartCoroutine(audioPlay((373f/60), .5f, 1f, soundA, Comp, soundB)); }
        else { StartCoroutine(audioPlay(.5f, 1f, soundA, Comp, soundB)); }
    }

    string RandomQuestion(string aText, string bText)
    {
        AText.text = aText;
        BText.text = bText + "?";
        string Comp = null;
        int randomCompar = Random.Range(0, 3);
        switch (randomCompar)
        {
            case 0:
                ComparText.text = "NHIỀU HƠN";
                Comp = "LT6_N";
                comp = compar.Bigger;
                break;
            case 1:
                ComparText.text = "ÍT HƠN";
                Comp = "LT6_I";
                comp = compar.Smaller;
                break;
            case 2:
                ComparText.text = "BẰNG";
                Comp = "LT6_B";
                comp = compar.Equal;
                break;
        }

        return Comp;
    }

    void deactivateButton()
    {
        nextButton.SetActive(false);
    }
    //for button
    public void nextPhase()
    {
        if (ableToNextPhase)
        {
            deactivateButton();
            ableToWrite = false;
            RandomPhase2();
            StartCoroutine(audioPlay(.5f, "LT6", 1));
        }
    }
    void StartTimer()
    {
        timer.maxValue = MaxValueTimer;
        timer.value = MaxValueTimer;
        _valueTimer = MaxValueTimer;
        startTimer = true;
        ableToAnswer = true;
    }
    void StopTimer()
    {
        ableToAnswer = false;
        startTimer = false;
        timer.maxValue = MaxValueTimer;
        timer.value = MaxValueTimer;
    }

    public void Answer(bool ans)
    {
        if (ableToAnswer)
        {
            if (ans == answer)
            {
                rightCount++;
                if (GameManager.Instance.canShowReaction)
                {
                    TimeLineManage.INSTANCE.RightPlay();
                }
            }
            else
            {
                wrongCount++;
                if (GameManager.Instance.canShowReaction)
                {
                    TimeLineManage.INSTANCE.WrongPlay();
                }
            }
            Phase2.GetChild(0).gameObject.SetActive(false);
            Phase2.GetChild(1).gameObject.SetActive(false);
            StopTimer();
            CurQuestion++;

            if(CurQuestion <= maxQuestion)
            {
                RandomPhase2();
            }
            else
            {
                Phase2.GetChild(2).gameObject.SetActive(false);
                Phase2.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    IEnumerator audioPlay(float waitBefore, string s, int id)
    {
        yield return new WaitForSeconds(waitBefore);

        TimeLineManage.INSTANCE.Tiet3(s, id);

        yield return 0;
    }
    IEnumerator audioPlay(float waitBefore, float waitAfter, float waitBtw, string ASound, string Comp, string BSound)
    {
        Tiet3AudioManager audio = Tiet3AudioManager.instance;
        audio.Stop();

        yield return new WaitForSeconds(waitBefore);

        Phase2.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(waitBtw);
        TimeLineManage.INSTANCE.Tiet3("Blank", 2);

        audio.Play(ASound);
        yield return new WaitForSeconds(audio.getCurSoundClip().length);
        audio.Play(Comp);
        yield return new WaitForSeconds(audio.getCurSoundClip().length);
        audio.Play(BSound);
        yield return new WaitForSeconds(audio.getCurSoundClip().length);

        Phase2.GetChild(0).gameObject.SetActive(true);
        Phase2.GetChild(1).gameObject.SetActive(true);
        StartTimer();
        
        yield return new WaitForSeconds(waitAfter);
    }

    IEnumerator audioPlay(float waitBefore, float waitAfter, string ASound, string Comp, string BSound)
    {
        Tiet3AudioManager audio = Tiet3AudioManager.instance;
        audio.Stop();

        yield return new WaitForSeconds(waitBefore);

        TimeLineManage.INSTANCE.Tiet3("Blank", 2);

        audio.Play(ASound);
        yield return new WaitForSeconds(audio.getCurSoundClip().length);
        audio.Play(Comp);
        yield return new WaitForSeconds(audio.getCurSoundClip().length);
        audio.Play(BSound);
        yield return new WaitForSeconds(audio.getCurSoundClip().length);

        Phase2.GetChild(0).gameObject.SetActive(true);
        Phase2.GetChild(1).gameObject.SetActive(true);
        StartTimer();

        yield return new WaitForSeconds(waitAfter);
    }
}
