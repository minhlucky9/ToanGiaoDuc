
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SendEmail : MonoBehaviour {

	public static int LANG_VI = 1;
	public static int LANG_EN = 2;
	public static int LANG_CHINA = 3;
    public static SendEmail instance;
	// Use this for initialization
	void Start () {
        instance = this;
	}
    string UserName;
    public void OnSend(string name1, string class1,List<Achivement> result1)
    {
        UserName = name1;
        List<string> output = new List<string>();

        output.Add("Ho ten: "+name1);
        output.Add("Lop: "+class1);

        output.Add(string.Format("{0,-15}  {1,-15}  {2, -15}  {3,22}  {4,22}  {5, 18}  {6,20}",
           "Hoat dong","","", "Ma-Truong nang luc", "Thoi gian lam bai", "So lan mac loi", "So cau tra loi dung"));
        for (int i = 0; i < result1.Count; i++)
        {
            Achivement x = result1[i];
            List<string> rowData = new List<string>();

            if (x.GameMode == ModeLessonEnum.Explore){
               
                output.Add(string.Format("{0,-15}  {1,-15}  {2, -15}  {3,22}  {4,22}  {5, 18}  {6,20}",
                    "Kham pha", result1[i].LevelName,"", result1[i].OutputAbility, GeneralController.instance.SecondsToMinutes((int)result1[i].GetTimeCount()) + "", result1[i].GetWrongSelectCount() + "", result1[i].GetRightAnswerCount() + ""));


            }
            else if (x.GameMode == ModeLessonEnum.Practice)
            {
               
                output.Add(string.Format("{0,-15}  {1,-15}  {2, -15}  {3,22}  {4,22}  {5, 18}  {6,20}",
                     "Luyen tap", result1[i].LevelName, "", result1[i].OutputAbility, GeneralController.instance.SecondsToMinutes((int)result1[i].GetTimeCount()) + "", result1[i].GetWrongSelectCount() + "", result1[i].GetRightAnswerCount() + ""));


            }
            else
            {
                  output.Add(string.Format("{0,-15}  {1,-15}  {2, -15}  {3,22}  {4,22}  {5, 18}  {6,20}",
                     "Tu kiem tra",  "Muc "+result1[i].HardMode, result1[i].LevelName, result1[i].OutputAbility, GeneralController.instance.SecondsToMinutes((int)result1[i].GetTimeCount()) + "", result1[i].GetWrongSelectCount() + "", result1[i].GetRightAnswerCount() + ""));

            }

         }

        string text = string.Join("\n", output.ToArray());
        
        Send_Email(text);
    }
	
	// Update is called once per frame
    public void Send_Email(string text)
    {

       // string text = DatabaseController.instance.LoadData();
        string email = "";
        string subject = MyEscapeURL("Ket qua lam bai cua "+ UserName );
        string body = MyEscapeURL(text);
        
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}

