using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RowController : MonoBehaviour
{
    public Text actionText;
    public Text nameText;
    public Text nameText1;
    public Text idText;
    public Text timeText;
    public Text wrongText;
    public Text correctText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowData(Achivement x)
    {
       

        nameText.text = x.LevelName;
        if (x.GameMode == ModeLessonEnum.Explore)
            actionText.text = "Khám phá ";
        else if (x.GameMode == ModeLessonEnum.Practice)
            actionText.text = "Luyện tập";
        else
        {
            actionText.text = "Tự kiểm tra";
            nameText.text = "Muc " + x.HardMode;
            if (nameText1 != null)
                nameText1.text = x.LevelName;
        }
           
            idText.text = x.OutputAbility;
            timeText.text = GeneralController.instance.SecondsToMinutes((int) x.GetTimeCount())+"";
            wrongText.text = x.GetWrongSelectCount()+ "";
            correctText.text = x.GetRightAnswerCount ()+ "";
    }
}
