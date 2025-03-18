using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookExportManage : MonoBehaviour
{
    public static BookExportManage Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public BookExport currentBookExport { get; private set; }
    public void SelectBookExport(BookExport bookExport)
    {
        currentBookExport = bookExport;
    }
}
