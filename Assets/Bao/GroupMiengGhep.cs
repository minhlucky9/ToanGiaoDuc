using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMiengGhep : MonoBehaviour
{
    public int rightpos;
    public List<GameObject> allPos;
    public GameObject baseBlock;
    public GameObject rightBlock;

    private void Start()
    {
        allPos[rightpos].GetComponent<HandChoice>().isRightChoice = true;
    }
    public void OnRightChoice()
    {
        rightBlock.GetComponent<RightObjectMove>().StartMove();
    }
}
