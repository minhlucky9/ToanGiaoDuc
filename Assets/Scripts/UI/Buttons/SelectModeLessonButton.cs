using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModeLessonButton : BaseSelectButton
{
    [SerializeField]
    private ModeLessonEnum mode;
    public ModeLessonEnum getMode()
    {
        return mode;
    }
}
