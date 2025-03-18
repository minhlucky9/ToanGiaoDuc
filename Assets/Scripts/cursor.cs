using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public Texture2D pointDefault;
    public Texture2D pointLeft;
    //public Texture2D pointRight;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(pointDefault, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(pointLeft, Vector2.zero, CursorMode.ForceSoftware);
        } else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(pointDefault, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
