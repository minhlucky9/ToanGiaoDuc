using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Achivement
{
    [SerializeField]
    private float time;
    [SerializeField]
    private int rightAnswerCount;
    [SerializeField]
    private int wrongAnswerCount;
    [SerializeField]
    private int selectWrongTimeCount;
    [SerializeField]
    private ModeLessonEnum gameMode;
    public ModeLessonEnum GameMode { get { return gameMode; } set { gameMode = value; } }
    [SerializeField]
    private string levelName;
    public string levelType { get; set; }
    public string LevelName { get { return levelName; } set { levelName = value; } }
    [SerializeField]
    public int HardMode { get; set; }
    [SerializeField]
    private string levelHardess;
    public string LevelHardess { get { return levelHardess; } set { levelHardess = value; } }
    [SerializeField]
    private string outputAbility;
    public string OutputAbility { get { return outputAbility; } set { outputAbility = value; } }
    public void SetTimeCount(float timeCount)
    {
        time = timeCount;
    }
    public void SetRightAnswerCount(int count)
    {
        rightAnswerCount = count;
    }
    public void SetWrongAnswerCount(int count)
    {
        wrongAnswerCount = count;

    }
    public void SetWrongSelectTimeCount(int count)
    {
        selectWrongTimeCount = count;
    }
    public float GetTimeCount()
    {
        return time;
    }
    public int GetRightAnswerCount()
    {
        return rightAnswerCount;
    }
    public int GetWrongAnswerCount()
    {
        return wrongAnswerCount;
    }
    public int GetWrongSelectCount()
    {
        return selectWrongTimeCount;
    }
    public SaveObject_Achievement ToSaveObject()
    {
        SaveObject_Achievement ret = new SaveObject_Achievement();
        ret.gameMode = gameMode;
        ret.HardMode = HardMode;
        ret.levelHardess = levelHardess;
        ret.levelName = levelName;
        ret.levelType = levelType;
        ret.outputAbility = outputAbility;
        ret.rightAnswerCount = rightAnswerCount;
        ret.selectWrongTimeCount = selectWrongTimeCount;
        ret.wrongAnswerCount = wrongAnswerCount;
        ret.time = time;
        return ret;
    }
    public void FromSaveObject(SaveObject_Achievement saved)
    {
        gameMode = saved.gameMode;
        HardMode = saved.HardMode;
        levelHardess = saved.levelHardess;
        levelName = saved.levelName;
        levelType = saved.levelType;
        outputAbility = saved.outputAbility;
        rightAnswerCount = saved.rightAnswerCount;
        selectWrongTimeCount = saved.selectWrongTimeCount;
        wrongAnswerCount = saved.wrongAnswerCount;
        time = saved.time;
    }
}
