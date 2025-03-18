using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum shapeType
{
    square, circle, triangle
}
public class SlotShape : MonoBehaviour
{
    public shapeType type;
    public bool isRight = false;
    public GameObject currentShape;
}
