using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultList
{
    public int idA;
    public string actionStr;
    public string nameStr1;
    public string nameStr2;
    public string idStr;
    public string timeStr;
    public int wrongi;
    public int correcti;
}
public class GeneralController : MonoBehaviour, IPanel
{
    public static GeneralController instance;
    public GameObject panelScroll;
    public GameObject panelVertical;
    public RectTransform panelChild1;
    public RectTransform panelChild2;
    public TrialLogger trialLogger;
    public GameObject panelMessage;
    List<ResultList> resultList = new List<ResultList>();
    
    // Start is called before the first frame update
    void Awake()
    {
         instance = this;
         panelMessage.SetActive(false);
    }
    GameObject panelChild;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        try
        {
            panelChild = transform.GetChild(0).gameObject;
        }
        catch
        {
            Debug.LogError(gameObject.name);
        }
        /*   ResultList item = new ResultList();
              item.idA = 1; // kham pha
              item.actionStr = "KHÁM PHÁ";
              item.nameStr1 = "";
              item.nameStr2 = "";
              item.idStr = "abc";
              item.timeStr = "t";
              item.wrongi = 0;
              item.correcti = 1;
          resultList.Add(item);
          for(int i  = 0;i<5;i++)
          {
              ResultList item1 = new ResultList();
              item1.idA = 2; // luyen tap
              if(i == 0)
                  item1.actionStr = "LUYỆN TẬP";
              else
                  item1.actionStr = "";
              item1.nameStr1 = "Bài "+(i+1);
              item1.nameStr2 = "";
              item1.idStr = "abc";
              item1.timeStr = "t"+i;
              item1.wrongi = i;
              item1.correcti = i+2;
              resultList.Add(item1);
          }
          for(int i  = 0;i<2;i++)
          {
              ResultList item2 = new ResultList();
              item2.idA = 3;// tu kiem tra
              if(i==0)
                  item2.actionStr = "TỰ KIỂM TRA";
              else
                  item2.actionStr = "";
              if(i ==0)
                  item2.nameStr1 = "Trình độ 1";
              else
                  item2.nameStr1 = "";
              item2.nameStr2 = "Bài "+(i+1);
              item2.idStr = "gdqf";
              item2.timeStr = "t"+i;
              item2.wrongi = i;
              item2.correcti = i+2;
              resultList.Add(item2);
          }
          for(int i  = 0;i<3;i++)
          {
              ResultList item3 = new ResultList();
              item3.idA = 3;// tu kiem tra
              item3.actionStr = "";
              if(i==0)
                  item3.nameStr1 = "Trình độ 2";
               else
                  item3.nameStr1 = "";
              item3.nameStr2 = "Bài "+(i+1);
              item3.idStr = "snf";
              item3.timeStr = "t"+i;
              item3.wrongi = i;
              item3.correcti = i+2;
              resultList.Add(item3);
          }
          for(int i  = 0;i<5;i++)
          {
              ResultList item4 = new ResultList();
              item4.idA = 3;// tu kiem tra
              item4.actionStr = "";
              if(i==0)
                  item4.nameStr1 = "Trình độ 3";
               else
                  item4.nameStr1 = "";
              item4.nameStr2 = "Bài "+(i+1);
              item4.idStr = "fcgt";
              item4.timeStr = "t"+i;
              item4.wrongi = i;
              item4.correcti = i+2;
              resultList.Add(item4);
          }
          */

        //ShowAllData(AchivementManager.INSTANCE.GetListAchivement());
    }
    public  string SecondsToMinutes(int seconds)
    {
        var ts = new System.TimeSpan(0, 0, seconds);
        return new System.DateTime(ts.Ticks).ToString(seconds >= 3600 ? "hh:mm:ss" : "mm:ss");
    }
    public void ShowMessage(string outputFile)
    {
       panelMessage.transform.GetChild(0).GetComponent<Text>().text ="File KetQua_"+ UserManage.Instance.CurrentUser.Id+".csv được lưu tại thư mục sau:\n" +outputFile;
       panelMessage.SetActive(true);
    }
    public void OnHideMessage()
    {
        panelMessage.SetActive(false);
    }
    public void OnBack()
    {
        Deactive();
    }
    public void OnExportFile()
    {
        trialLogger.InitData(UserManage.Instance.CurrentUser.Id,"1A", AchivementManager.INSTANCE.GetListAchivement());
    }
    public void OnSendEmail()
    {
        SendEmail.instance.OnSend(UserManage.Instance.CurrentUser.Id, "1A", AchivementManager.INSTANCE.GetListAchivement());
    }
    public void ShowAllData(List<Achivement> a)
    {
        while (panelVertical.transform.childCount > 0)
        {
            DestroyImmediate(panelVertical.transform.GetChild(0).gameObject);
        }
          for (int i = 0; i < a.Count; i++)
        {
            RectTransform newItem;
            
            if(a[i].GameMode == ModeLessonEnum.SelfExamination)
                newItem = Instantiate(panelChild2) as RectTransform;   
             else
                newItem = Instantiate(panelChild1) as RectTransform;               
            newItem.SetParent(panelVertical.transform);
            newItem.position = panelVertical.transform.position;
            newItem.localScale = Vector3.one;
            newItem.name = i+"";
            newItem.gameObject.SetActive(true);
            newItem.GetComponent<RowController>().ShowData(a[i]);
            
        }
    }
    public void Active()
    {
        try
        {
            GetComponent<Animator>().Play("in");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        ShowAllData(AchivementManager.INSTANCE.GetListAchivement());
    }
    public void Deactive()
    {
        try
        {
            GetComponent<Animator>().Play("out");
        }
        catch
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public bool IsActive()
    {
        if (panelChild != null)
            return panelChild.activeSelf;
        return false;
    }
}
