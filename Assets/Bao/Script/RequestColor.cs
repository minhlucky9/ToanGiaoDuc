using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestColor : MonoBehaviour
{
    public GameObject pen;
    public List<int> colorRequest;
    public GameObject allItem;
    public GameObject squares, circles, retangles, triangles;
    public List<GameObject> shape;
    public Color[] color;
    public void Request(GameObject items)
    {
        allItem = items;
        pen.SetActive(true);
        if (InputCallBackPen.Instance != null)
        {
            InputCallBackPen.Instance.OnStart();
        }
        color = pen.GetComponent<InputCallBackPen>().allPensColor;
        FindRequestColor();
    }
    public void FindRequestColor()
    {
        squares = allItem.transform.GetChild(0).gameObject;
        circles = allItem.transform.GetChild(1).gameObject;
        retangles = allItem.transform.GetChild(2).gameObject;
        triangles = allItem.transform.GetChild(3).gameObject;
        List<int> colorsRequestList = new List<int>();
        for(int i = 0; i < shape.Count; i++)
        {
            int r = Random.Range(1, 8);
            int tryTime = 0;
            do
            {
                r = Random.Range(1, 8);
                tryTime++;
                if (tryTime > 8)
                {
                    break;
                }
            }
            while (colorsRequestList.Contains(r));
            colorsRequestList.Add(r);
            shape[i].GetComponent<SpriteRenderer>().color = color[r];
        }
        if (shape.Count >= 4)
        {
            SetRequestColor(squares, colorsRequestList[0]);
            SetRequestColor(circles, colorsRequestList[1]);
            SetRequestColor(retangles, colorsRequestList[2]);
            SetRequestColor(triangles, colorsRequestList[3]);
        }
    }
    public void SetRequestColor(GameObject t, int color)
    {
        for (int i = 0; i < t.transform.childCount; i++)
        {
            t.transform.GetChild(i).GetComponent<ItemValue>().SetRequireValue(color);
        }
    }
    public int CountAllWrongChoice()
    {
        return CountWrongChoice(squares)
            + CountWrongChoice(circles)
            + CountWrongChoice(retangles)
            + CountWrongChoice(triangles);

    }
    public int CountWrongChoice(GameObject t)
    {
        int count = 0;
        for (int i = 0; i < t.transform.childCount; i++)
        {
            count += t.transform.GetChild(i).GetComponent<ShapeColoring>().wrongChoiceCount;
        }
        return count;
    }
    public int CountAllWrong()
    {
        return CountWrong(squares)
            + CountWrong(circles)
            + CountWrong(retangles)
            + CountWrong(triangles);
    }
    public int CountAllRight()
    {
        return squares.transform.childCount
            + circles.transform.childCount
            + retangles.transform.childCount
            + triangles.transform.childCount
            - CountAllWrong();

    }
    public int CountWrong(GameObject t)
    {
        int count = 0;
        for (int i = 0; i < t.transform.childCount; i++)
        {
            if (!t.transform.GetChild(i).GetComponent<ShapeColoring>().isRight)
            {
                count++;
            }
        }
        return count;
    }
}
