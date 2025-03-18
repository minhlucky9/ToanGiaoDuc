using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RateCondition 
{
    public float from;
    public float to;
    public bool isEqualFrom = false;
    public bool isEqualTo = false;
    public bool isGreaterThanFrom = false;
    public bool isLessThanTo = false;
    public string outPut;
    public bool checkConditionOk(float checkRate)
    {
        if (isEqualFrom)
        {
            if (isGreaterThanFrom)
            {
                return checkRate >= from;
            }
            if (isEqualTo)
            {
                return checkRate >= from && checkRate <= to;
            }
            else
            {
                return checkRate >= from && checkRate < to;
            }
        }
        else
        {
            if (isGreaterThanFrom)
            {
                return checkRate > from;
            }
            if (isLessThanTo)
            {
                if (isEqualTo)
                {
                    return checkRate <= to;
                }
                else
                {
                    return checkRate < to;
                }
            }
            if (isEqualTo)
            {
                return checkRate > from && checkRate <= to;
            }
            else
            {
                return checkRate > from && checkRate < to;
            }
        }
    }
}
