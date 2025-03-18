using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanRate 
{
    public static int Check()
    {
        if (LevelManager.INSTANCE.IsDoneExplore())
        {
            if (LevelManager.INSTANCE.IsDoneAllPracticeLevel())
            {
                if (LevelManager.INSTANCE.IsDoneOneSelfExamination())
                {
                    return 1;
                }
                else
                {
                    return -3;
                }
            }
            else
            {
                return -2;
            }
        }
        else
        {
            return -1;
        }
    }
}
