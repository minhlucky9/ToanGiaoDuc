using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltilityCustom : MonoBehaviour
{
    public float minimumDisTance = 0.1f;
    public static string FloatToStringTime(float num)
    {
        string returnval = string.Empty;
        num = num * 100;
        long time = (long)num;
        time = time / 100;
        if (time / 3600 >= 1)
        {
            long hour = time / 3600;
            returnval += string.Empty + hour + "h";
            time = time - hour*3600;
        }
        if (time / 60 >= 1)
        {
            long minute = time / 60;
            returnval += string.Empty + minute + "\'";
            time = time - minute*60;
        }
        else
        {
            returnval += string.Empty + "00\'";
        }
        if (time < 10)
        {
            returnval += "0";
        }
        returnval += string.Empty + time + "\"";
        return returnval;
    }
    public static IEnumerator MoveOverSecondIE(Transform tp, Vector3 des, float time)
    {
        float t = 0f;
        Vector3 v3Tmp;
        while (t < time)
        {
            v3Tmp = Vector3.Lerp(tp.position, des, t / time);
            tp.position = v3Tmp;
            t += Time.deltaTime;
            Debug.Log(v3Tmp);
            yield return new WaitForEndOfFrame();
        }
        tp.position = des;
        Debug.Log("Move done :" + tp.position);

    }
    public static IEnumerator MoveOverSecondLocalPosIE(Transform tp, Vector3 des, float time)
    {
        float t = 0f;
        Vector3 v3Tmp;
        while (t < time)
        {
            v3Tmp = Vector3.Lerp(tp.localPosition, des, t / time);
            tp.localPosition = v3Tmp;
            Debug.Log(v3Tmp);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        tp.localPosition = des;
        Debug.Log("Move done local: " + tp.localPosition);
    }
    public static IEnumerator MoveAndScaleOverSecond(Transform tp, Vector3 posDes, Vector3 scaleDes, float time)
    {
        float t = 0f;
        Vector3 v3Tmp, v3tmp1;
        while (t < time)
        {
            v3Tmp = Vector3.Lerp(tp.position, posDes, t / time);
            v3tmp1 = Vector3.Lerp(tp.localScale, scaleDes, t / time);
            tp.position = v3Tmp;
            tp.localScale = v3tmp1;
            Debug.Log(v3Tmp);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        tp.position = posDes;
        tp.localScale = scaleDes;
        Debug.Log("Move and scale done: " + tp.localPosition);
    }
    public static IEnumerator MoveAndScaleLocalOverSecond(Transform tp, Vector3 posDes, Vector3 scaleDes, float time)
    {
        float t = 0f;
        Vector3 v3Tmp, v3tmp1;
        while (t < time)
        {
            v3Tmp = Vector3.Lerp(tp.localPosition, posDes, t / time);
            v3tmp1 = Vector3.Lerp(tp.localScale, scaleDes, t / time);
            tp.localPosition = v3Tmp;
            tp.localScale = v3tmp1;
            //Debug.Log(v3Tmp);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        tp.localPosition = posDes;
        tp.localScale = scaleDes;
        Debug.Log("Move and scale done local: " + tp.localPosition);
    }
    public static IEnumerator ScaleOverSecond(Transform tp, Vector3 des, float time)
    {
        float t = 0f;
        Vector3 v3Tmp ;
        while (t < time)
        {
            v3Tmp = Vector3.Lerp(tp.localScale, des, t / time);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        tp.localScale = des;
    }
}
