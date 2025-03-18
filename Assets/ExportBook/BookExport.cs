using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sharpPDF;
using sharpPDF.Enumerators;
using System.IO;

public class BookExport : MonoBehaviour
{
    [SerializeField]
    string fileName = "ExportBook.pdf";
    [SerializeField]
    string folderName;
    public string outputFolder { get; private set; }
    public int resWidth = 2160;
    public int resHeight = 3840;
    string dataPath;
    bool isExporting = false;
    public List<LevelBookData> listKhamPha;
    public List<LevelBookData> listLT;
    public List<LevelBookData> listTKT1;
    public List<LevelBookData> listTKT2;
    public List<LevelBookData> listTKT3;
    void Start()
    {
        outputFolder = Application.dataPath + "/StreamingAssets" + "/output/Book/";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        dataPath = Application.dataPath + "/in_sach_asset/" + folderName + "/";
        Debug.Log("ApplicationPath: " + outputFolder);
        //yield return StartCoroutine(CreatePDF());
    }
    bool isClick = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isClick)
            {
                isClick = true;
            }
        }
    }

    public void CreatePDF()
    {
        if (isExporting) return;
        isExporting = true;
        try
        {

            StartCoroutine(CreatePDFIE());
        }
        catch
        {
            isExporting = false;
        }
    }
    public IEnumerator CreatePDFIE()
    {
        pdfDocument myDoc = new pdfDocument("Sample Application", "Me", false);
        pdfPage myFirstPage = myDoc.addPage();
        Debug.Log("Page width: " + myFirstPage.width);
        Debug.Log("Page height: " + myFirstPage.height);
        //myFirstPage.addText("Học sinh: ", 10, 730, predefinedFont.csHelvetica, 30, new pdfColor(predefinedColor.csOrange));
        string imagePath = dataPath + folderName + "_Firstpage.jpg";
        yield return StartCoroutine(myFirstPage.newAddImage(imagePath, 0, 0));
        //myFirstPage.addText("Hoc sinh: " + UserManage.Instance.CurrentUser.Id, 100, 500, predefinedFont.csHelveticaOblique, 30, new pdfColor(predefinedColor.csOrange));

        myFirstPage.addText("Hoc sinh: Dinh Xuan Tung", 230, 300, predefinedFont.csHelveticaBold, 35, new pdfColor(predefinedColor.csBlack));
        for (int i = 0; i < listKhamPha.Count; i++)
        {
            int version = 1;
            if (listKhamPha[i].numberOfVersion > 1)
            {
                version = Random.Range(1, listKhamPha[i].numberOfVersion + 1);
            }
            imagePath = dataPath + folderName + "_Khampha_bai" + (i + 1) + "_" + version + ".jpg";
            pdfPage secondPage = myDoc.addPage();
            yield return StartCoroutine(secondPage.newAddImage(imagePath, 0, 0));
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < listLT.Count; i++)
        {
            int version = 1;
            if (listLT[i].numberOfVersion > 1)
            {
                version = Random.Range(1, listLT[i].numberOfVersion + 1);
            }
            imagePath = dataPath + folderName + "_Luyentap_bai" + (i + 1) + "_" + version + ".jpg";
            pdfPage secondPage = myDoc.addPage();
            yield return StartCoroutine(secondPage.newAddImage(imagePath, 0, 0));
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < listTKT1.Count; i++)
        {
            int version = 1;
            if (listTKT1[i].numberOfVersion > 1)
            {
                version = Random.Range(1, listTKT1[i].numberOfVersion + 1);
            }
            imagePath = dataPath + folderName + "_KtMuc1_bai" + (i + 1) + "_" + version + ".jpg";
            pdfPage secondPage = myDoc.addPage();
            yield return StartCoroutine(secondPage.newAddImage(imagePath, 0, 0));
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < listTKT2.Count; i++)
        {
            int version = 1;
            if (listTKT2[i].numberOfVersion > 1)
            {
                version = Random.Range(1, listTKT2[i].numberOfVersion + 1);
            }
            imagePath = dataPath + folderName + "_KtMuc2_bai" + (i + 1) + "_" + version + ".jpg";
            pdfPage secondPage = myDoc.addPage();
            yield return StartCoroutine(secondPage.newAddImage(imagePath, 0, 0));
            yield return new WaitForEndOfFrame();

        }
        for (int i = 0; i < listTKT3.Count; i++)
        {
            int version = 1;
            if (listTKT3[i].numberOfVersion > 1)
            {
                version = Random.Range(1, listTKT3[i].numberOfVersion + 1);
            }
            imagePath = dataPath + folderName + "_KtMuc3_bai" + (i + 1) + "_" + version + ".jpg";
            pdfPage secondPage = myDoc.addPage();
            yield return StartCoroutine(secondPage.newAddImage(imagePath, 0, 0));
            yield return new WaitForEndOfFrame();

        }
        string outputFinal = outputFolder + fileName + ".pdf";
        if (Directory.Exists(outputFinal))
        {
            outputFinal = outputFolder + fileName + "_1.pdf";
        }
        myDoc.createPDF(outputFinal);
        MessageCallBackPopupPanel.INSTACNE.Active("File in sách đã in thành công! \nTại " + BookExportManage.Instance.currentBookExport.outputFolder);
        isExporting = false;
        //myTable = null;
    }
}
[System.Serializable]
public class LevelBookData
{
    public int numberOfVersion = 1;
}
