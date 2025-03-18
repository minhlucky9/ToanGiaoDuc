using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateLesson : MonoBehaviour
{
    public List<RateConditions> listConditions;
    List<Achivement> listAchievement;
    public int NumberOfQualification { get { return listConditions.Count; } }
    public void UpdateList()
    {
        listAchievement = AchivementManager.INSTANCE.GetListAchivement();
    }

    public string getOutPutRate(int id)
    {
        UpdateList();
        int right = 0;
        int wrong = 0;
        int sum = 0;
        float ratio = 0;
        for (int i = 0; i < listConditions.Count; i++)
        {
            if (listConditions[i].id.Equals(id))
            {
                switch (id)
                {
                    case 1:
                        for (int j = 0; j < listAchievement.Count; j++)
                        {
                            if (listAchievement[j].GameMode == ModeLessonEnum.Explore)
                            {
                                right = listAchievement[j].GetRightAnswerCount();
                                wrong = listAchievement[j].GetWrongAnswerCount();
                                sum = right + wrong;
                                if (sum > 0)
                                {
                                    ratio = (float)right / sum;
                                }
                                return listConditions[i].getRate(ratio);
                            }
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                        for (int j = 0; j < listAchievement.Count; j++)
                        {
                            if (listAchievement[j].levelType == listConditions[i].levelType)
                            {
                                right += listAchievement[j].GetRightAnswerCount();
                                wrong += listAchievement[j].GetWrongAnswerCount();
                                if (sum > 0)
                                {
                                    ratio = (float)right / sum;
                                }
                            }
                            sum = right + wrong;
                            return listConditions[i].getRate(ratio);
                        }
                        break;
                    case 5:
                        float timeCount = 0;
                        for (int j = 0; j < listAchievement.Count; j++)
                        {
                            timeCount += listAchievement[j].GetTimeCount();
                        }
                        return listConditions[i].getRate(timeCount);
                        break;
                    default:
                        break;
                }
            }
        }
        return "Khong co danh gia!";
    }
    public void getOutPutRate()
    {
        UpdateList();
        int sum = 0;
        int right = 0;
        int wrong = 0;
        int wrongSelect = 0;
        float time = 0;
        for (int i = 0; i < listConditions.Count; i++)
        {
            sum = 0;
            wrongSelect = 0;
            right = 0;
            wrong = 0;
            time = 0;
            for (int j = 0; j < listAchievement.Count; j++)
            {
                if (listConditions[i].checkKhamPha)
                {
                    if (listAchievement[j].GameMode == ModeLessonEnum.Explore)
                    {
                        if (listConditions[i].checkDangBai)
                        {
                            if (listAchievement[j].levelType == listConditions[i].levelType)
                            {
                                time += listAchievement[j].GetTimeCount();
                                right += listAchievement[j].GetRightAnswerCount();
                                wrong += listAchievement[j].GetWrongAnswerCount();
                                wrongSelect += listAchievement[j].GetWrongSelectCount();
                            }
                        }
                        else
                        {
                            time += listAchievement[j].GetTimeCount();
                            right += listAchievement[j].GetRightAnswerCount();
                            wrong += listAchievement[j].GetWrongAnswerCount();
                            wrongSelect += listAchievement[j].GetWrongSelectCount();
                        }
                    }
                }
                if (listConditions[i].checkLuyenTap)
                {
                    if (listAchievement[j].GameMode == ModeLessonEnum.Practice)
                    {
                        if (listConditions[i].checkDangBai)
                        {
                            if (listAchievement[j].levelType == listConditions[i].levelType)
                            {
                                time += listAchievement[j].GetTimeCount();
                                right += listAchievement[j].GetRightAnswerCount();
                                wrong += listAchievement[j].GetWrongAnswerCount();
                                wrongSelect += listAchievement[j].GetWrongSelectCount();
                            }
                        }
                        else
                        {
                            time += listAchievement[j].GetTimeCount();
                            right += listAchievement[j].GetRightAnswerCount();
                            wrong += listAchievement[j].GetWrongAnswerCount();
                            wrongSelect += listAchievement[j].GetWrongSelectCount();
                        }
                    }
                }
                if (listConditions[i].checkTuKiemTra)
                {
                    if (listAchievement[j].GameMode == ModeLessonEnum.SelfExamination)
                    {
                        if (listConditions[i].checkDangBai)
                        {
                            if (listAchievement[j].levelType == listConditions[i].levelType)
                            {
                                time += listAchievement[j].GetTimeCount();
                                right += listAchievement[j].GetRightAnswerCount();
                                wrong += listAchievement[j].GetWrongAnswerCount();
                                wrongSelect += listAchievement[j].GetWrongSelectCount();
                            }
                        }
                        else
                        {
                            time += listAchievement[j].GetTimeCount();
                            right += listAchievement[j].GetRightAnswerCount();
                            wrong += listAchievement[j].GetWrongAnswerCount();
                            wrongSelect += listAchievement[j].GetWrongSelectCount();
                        }
                    }
                }
            }
            sum = right + wrong;
            string output = "";
            switch (listConditions[i].inputData)
            {
                case InputSource.Time:
                    output = listConditions[i].getRate(time);
                    RatePanel.Instance.AddRow(listConditions[i].tenTieuChi, output);
                    break;
                case InputSource.RightPerCent:
                    sum = right + wrong;
                    float percentR = 0f;
                    if (sum > 0)
                    {
                        percentR = (float)right / sum;
                    }
                    output = listConditions[i].getRate(percentR);
                    RatePanel.Instance.AddRow(listConditions[i].tenTieuChi, output);
                    break;
                case InputSource.RightCount:
                    
                    output = listConditions[i].getRate(right);
                    RatePanel.Instance.AddRow(listConditions[i].tenTieuChi, output);
                    break;
                case InputSource.WrongCount:
                    output = listConditions[i].getRate(wrong);
                    RatePanel.Instance.AddRow(listConditions[i].tenTieuChi, output);
                    break;
                case InputSource.WrongPercent:
                    sum = right + wrong;
                    float percentWR = 0f;
                    if (sum > 0)
                    {
                        percentR = (float)wrong / sum;
                    }
                    output = listConditions[i].getRate(percentWR);
                    RatePanel.Instance.AddRow(listConditions[i].tenTieuChi, output);
                    break;
                case InputSource.WrongSelectCount:
                    output = listConditions[i].getRate(wrongSelect);
                    RatePanel.Instance.AddRow(listConditions[i].tenTieuChi, output);
                    break;
                default:
                    RatePanel.Instance.AddRow(listConditions[i].tenTieuChi, "Không có đánh giá");
                    break;
            }
        }
    }
}
