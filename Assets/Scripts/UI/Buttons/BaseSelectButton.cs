using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSelectButton : MonoBehaviour
{
    [SerializeField]
    private string id;
    public string GetId()
    {
        return id;
    }
    public void SetId(string idToSet)
    {
        id = idToSet;
    }
}

public class BaseSelectButtonInt : MonoBehaviour
{
    [SerializeField]
    private int id;
    public int GetId()
    {
        return id;
    }
    public void SetId(int idToSet)
    {
        id = idToSet;
    }
}
