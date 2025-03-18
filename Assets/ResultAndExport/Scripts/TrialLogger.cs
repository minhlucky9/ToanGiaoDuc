using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TrialLogger : MonoBehaviour {

    
    public int currentTrialNumber = 0;    
    List<string> header;
    [HideInInspector]
    public Dictionary<string, string> trial;
    [HideInInspector]
    public string outputFolder;
    
    bool trialStarted = false;
    string ppid;
    string dataOutputPath;
    List<string> output;
    
    // Use this for initialization
    void Awake () {
        outputFolder = Application.dataPath + "/StreamingAssets" + "/output";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        output = new List<string>();
       
       // dataOutputPath = outputFolder + "/" + "KetQua_" + userName+ ".csv";
    }
	

	// Update is called once per frame
	

   /* public void Initialize(string participantID, List<string> customHeader)
    {
        output = new List<string>();
       
        dataOutputPath = outputFolder + "/" + participantID + ".csv";
       
    }*/
   public void InitData(string name1, string class1, List<Achivement> item)
    {
        output.Clear(); 
        dataOutputPath = outputFolder + "/" + "KetQua_" + name1 + "_" + GameManager.Instance.GetCurrentTietHoc().GetTenTietHoc() + ".csv";
        Debug.Log(name1);
        output.Add("Ho ten: " + name1);
        output.Add("Lop: "+class1);
        InitHeader();
      //  InitDict();
        InsertData(item);

        
    }
    private void InitHeader()
    {
        header = new List<string>();
        header.Insert(0, "Hoat dong");
        header.Insert(1, "");
        header.Insert(2, "");
        header.Insert(3, "Ma - Truong nang luc");
        header.Insert(4, "Thoi gian lam bai");
        header.Insert(5, "So lan mac loi");
        header.Insert(6, "So cau tra loi dung");
        output.Add(string.Join(",", header.ToArray()));
    }

    private void InitDict()
    {
        trial = new Dictionary<string, string>();
        foreach (string value in header)
        {
            trial.Add(value, "");
        }
    }

    public void InsertData(List<Achivement> result1)
    {
        for(int i = 0;i< result1.Count ;i++)
        {
            Achivement x = result1[i];
            List<string> rowData = new List<string>();
            if (x.GameMode == ModeLessonEnum.Explore)
            {
                rowData.Add("Kham pha ");
                rowData.Add("");
            }
            else if (x.GameMode == ModeLessonEnum.Practice)
            {
                rowData.Add("Luyen tap");
                rowData.Add("");
            }
            else
            {
                rowData.Add("Tu kiem tra");
                rowData.Add("Muc " + result1[i].HardMode);
            }
                          
            rowData.Add(result1[i].LevelName);
            
            rowData.Add(result1[i].OutputAbility);
            rowData.Add(GeneralController.instance.SecondsToMinutes((int)result1[i].GetTimeCount())+"");
            rowData.Add(result1[i].GetWrongSelectCount()+"");
            rowData.Add(result1[i].GetRightAnswerCount()+"");
            output.Add(string.Join(",", rowData.ToArray()));

        }
        if (output != null && dataOutputPath != null)
        {

            File.WriteAllLines(dataOutputPath, output.ToArray());
            Debug.Log(string.Format("Saved data to {0}.", dataOutputPath));
            GeneralController.instance.ShowMessage(dataOutputPath);
        }
        else Debug.LogError("Error saving data - TrialLogger was not initialsed properly");

    }
  /*  public void StartTrial()
    {
        trialStarted = true;
        currentTrialNumber += 1;
        InitDict();
      //  trial["Hoạt động"] = currentTrialNumber.ToString();
      //  trial["ppid"] = ppid;
       // trial["start_time"] = Time.time.ToString();
    }

    public void EndTrial()
    {
        if (output != null && dataOutputPath != null)
        {
            if (trialStarted)
            {
                trial["end_time"] = Time.time.ToString();
                output.Add(FormatTrialData());
                trialStarted = false;
            }
            else Debug.LogError("Error ending trial - Trial wasn't started properly");

        }
        else Debug.LogError("Error ending trial - TrialLogger was not initialsed properly");
     

    }

    private string FormatTrialData()
    {
        List<string> rowData = new List<string>();
        foreach (string value in header)
        {
            rowData.Add(trial[value]);
        }
        return string.Join(",", rowData.ToArray());
    }

    private void OnApplicationQuit()
    {

        if (output != null && dataOutputPath != null)
        {
            File.WriteAllLines(dataOutputPath, output.ToArray());
            Debug.Log(string.Format("Saved data to {0}.", dataOutputPath));
        }
        else Debug.LogError("Error saving data - TrialLogger was not initialsed properly");
        
    }
    */
}
