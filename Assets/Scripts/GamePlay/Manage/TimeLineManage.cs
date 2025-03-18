  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManage : MonoBehaviour
{
    public static TimeLineManage INSTANCE;
    [SerializeField]
    private PlayableDirector rightDirector;
    [SerializeField]
    private PlayableDirector wrongDirector;
    [SerializeField]
    private PlayableDirector entranceDirector;
    [SerializeField]
    private PlayableDirector entranceHomeDirector;
    [SerializeField]
    private PlayableDirector huongDanDirector;
    [SerializeField]
    private PlayableDirector countDownDirector;
    [SerializeField]
    private PlayableDirector nhacBaiDirector;
    [SerializeField]
    private PlayableDirector phanTichDirector;
    [SerializeField]
    private PlayableDirector giaoNhiemVuBeCaDirector;
    [SerializeField]
    private PlayableDirector phanTichBeCaDirector;
    [SerializeField]
    private PlayableDirector luyenTap1Tut;
    [SerializeField]
    private PlayableDirector luyenTap2Tut;
    [SerializeField]
    private PlayableDirector luyenTap3Tut;
    [SerializeField]
    private PlayableDirector luyenTap5Tut;
    [SerializeField] private PlayableDirector Tiet3_LT1Phase1tut;
    [SerializeField] private PlayableDirector Tiet3_LT1Phase2tut;
    [SerializeField] private PlayableDirector Tiet3_LT1Phase3tut;
    [SerializeField] private PlayableDirector Tiet3_LT1Phase4tut;
    [SerializeField] private PlayableDirector Tiet3_LT2atut;
    [SerializeField] private PlayableDirector Tiet3_LT2btut;
    [SerializeField] private PlayableDirector Tiet3_LT2ctut;
    [SerializeField] private PlayableDirector Tiet3_LT3tut;
    [SerializeField] private PlayableDirector Tiet3_LT4_1tut;
    [SerializeField] private PlayableDirector Tiet3_LT4_2tut;
    [SerializeField] private PlayableDirector Tiet3_LT5_1tut;
    [SerializeField] private PlayableDirector Tiet3_LT5_2tut;
    [SerializeField] private PlayableDirector Tiet3_LT6_2tut;
    [SerializeField] private PlayableDirector Tiet3_haiTalk;
    [SerializeField] private PlayableDirector Tiet3_lanTalk;
    [SerializeField] private PlayableDirector Tiet3_giaovienTalk;
    [SerializeField] private PlayableDirector[] Tiet3_HuongDan;
    [SerializeField] private PlayableDirector[] Tiet3_PhanTich;
    [SerializeField] private PlayableDirector Tiet4_tutToMau;
    [SerializeField] private PlayableDirector Tiet4_tutXepGach;
    [SerializeField] private PlayableDirector Tiet4_tutChonHinh;
    [SerializeField] private PlayableDirector Tiet4_tutDemHinh;
    [SerializeField] private PlayableDirector Tiet4_tutDemLapPhuong;
    [SerializeField] private PlayableDirector Tiet4_tutDemKhoiChuNhat;
    [SerializeField] private PlayableDirector Tiet4_tutChonKhoiChuNhat;
    [SerializeField] private PlayableDirector Tiet4_tutChonLapPhuong;
    [SerializeField] private PlayableDirector Tiet4_tutChonMiengGhep;
    [SerializeField] private PlayableDirector[] Tiet5_KP;
    [SerializeField] private PlayableDirector[] Tiet5_LT;
    [SerializeField] private PlayableDirector[] Tiet5_LT2;
    [SerializeField] private PlayableDirector[] Tiet5_TuKt;
    [SerializeField] private PlayableDirector[] Tiet6_KP;
    [SerializeField] private PlayableDirector[] Tiet6_LT;

    [SerializeField]
    private PlayableDirector congratulationDirector;
    [SerializeField]
    private PlayableDirector phanTich6_10Director;

    [SerializeField]
    private PlayableDirector currentDirector;
    public bool isCapturing = false;
    private void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
    }
    private void Start()
    {
        EntranceHomePlay();
    }
    public void WrongPlay()
    {
        if (isCapturing) return;
        if(currentDirector!= null)
        {
            currentDirector.Stop();
        }
        wrongDirector?.Stop();
        wrongDirector?.Play();
        currentDirector = wrongDirector;
    }
    public void ToMauTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutToMau?.Stop();
        Tiet4_tutToMau?.Play();
        currentDirector = Tiet4_tutToMau;
    }
    public void XepGachTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutXepGach?.Stop();
        Tiet4_tutXepGach?.Play();
        currentDirector = Tiet4_tutXepGach;
    }
    public void ChonHinhTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutChonHinh?.Stop();
        Tiet4_tutChonHinh?.Play();
        currentDirector = Tiet4_tutChonHinh;
    }
    public void DemHinhTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutDemHinh?.Stop();
        Tiet4_tutDemHinh?.Play();
        currentDirector = Tiet4_tutDemHinh;
    }
    public bool IsWrongPlaying()
    {
        if(wrongDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void RightPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        rightDirector?.Stop();
        rightDirector?.Play();
        currentDirector = rightDirector;
    }
    public void SetCurrentPayableDirector(PlayableDirector director)
    {
        currentDirector = director;
    }
    public void SetAndPlayCurrentPayableDirector(PlayableDirector director)
    {
        if (isCapturing) return;
        currentDirector?.Stop();
        currentDirector = director;
        currentDirector.Play();
    }
    public void PlayCurrentDirector()
    {
        if (isCapturing) return;
        currentDirector?.Stop();
        currentDirector?.Play();
    }
    public bool IsRightPlaying()
    {

        if (rightDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void EntrancePlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        entranceDirector?.Stop();
        entranceDirector?.Play();
        currentDirector = entranceDirector;
    }
    public bool IsEntrancePlaying()
    {
        if (entranceDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void EntranceHomePlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        if(entranceHomeDirector!= null)
        {
            entranceHomeDirector?.Stop();
            entranceHomeDirector?.Play();
            currentDirector = entranceHomeDirector;
        }
    }
    public void HuongDanPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            Debug.Log("xx");
            currentDirector.Stop();
        }
        else
        {
            Debug.Log("zz");

        }
        huongDanDirector?.Stop();
        huongDanDirector?.Play();
        currentDirector = huongDanDirector;
    }
    public bool IsHuongDanPlaying()
    {
        if (huongDanDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void CountDownPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        countDownDirector?.Stop();
        countDownDirector?.Play();
        currentDirector = countDownDirector;
    }
    public void PhanTich6_10Play()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        phanTich6_10Director?.Stop();
        phanTich6_10Director?.Play();
        currentDirector = phanTich6_10Director;
    }
    public bool IsCountPlaying()
    {
        if (countDownDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void NhacBaiPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        nhacBaiDirector?.Stop();
        nhacBaiDirector?.Play();
        currentDirector = nhacBaiDirector;
    }
    public bool IsNhacBaiPlaying()
    {
        if (nhacBaiDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void PhanTichDirectorPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        phanTichDirector?.Stop();
        phanTichDirector?.Play();
        currentDirector = phanTichDirector;
    }
    public bool IsPhanTichPlaying()
    {
        if (phanTichBeCaDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void GiaoNhiemVuBeCaPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        giaoNhiemVuBeCaDirector?.Stop();
        giaoNhiemVuBeCaDirector?.Play();
        currentDirector = giaoNhiemVuBeCaDirector;
    }
    public bool IsGiaoNhiemVuBeCaPlaying()
    {
        if (giaoNhiemVuBeCaDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void PhanTichBeCaPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        phanTichBeCaDirector?.Stop();
        phanTichBeCaDirector?.Play();
        currentDirector = phanTichBeCaDirector;
    }
    public bool IsPhanTichBeCaPlaying()
    {
        if (phanTichBeCaDirector.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }
    public void LuyenTap1TutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        luyenTap1Tut?.Stop();
        luyenTap1Tut?.Play();
        currentDirector = luyenTap1Tut;
    }
    public void LuyenTap2TutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        luyenTap2Tut?.Stop();
        luyenTap2Tut?.Play();
        currentDirector = luyenTap2Tut;
    }
    public void LuyenTap3TutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        luyenTap3Tut?.Stop();
        luyenTap3Tut?.Play();
        currentDirector = luyenTap3Tut;
    }
    public void LuyenTap5TutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        luyenTap5Tut?.Stop();
        luyenTap5Tut?.Play();
        currentDirector = luyenTap5Tut;
    }
    public void Tiet3(string lvl, int id)
    {
        if (isCapturing) return;
        if (lvl == "Blank") { }
        else if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        PlayableDirector thisDirector = null;
        switch (lvl)
        {
            case "KhamPha_Huongdan":
                if (id == 1) { thisDirector = Tiet3_HuongDan[0]; }
                else if (id == 2) { thisDirector = Tiet3_HuongDan[1]; }
                else if (id == 3) { thisDirector = Tiet3_HuongDan[2]; }
                else if (id == 4) { thisDirector = Tiet3_HuongDan[3]; }
                else if (id == 6) { thisDirector = Tiet3_HuongDan[4]; }
                break;
            case "KhamPha_Phantich":
                if (id == 1) { thisDirector = Tiet3_PhanTich[0]; }
                else if (id == 3) { thisDirector = Tiet3_PhanTich[1]; }
                else if (id == 4) { thisDirector = Tiet3_PhanTich[2]; }
                else if (id == 5) { thisDirector = Tiet3_PhanTich[3]; }
                break;
            case "LT1":
                if(id == 0) { thisDirector = Tiet3_LT1Phase1tut; }
                else if(id == 1) { thisDirector = Tiet3_LT1Phase2tut; }
                else if(id == 2) { thisDirector = Tiet3_LT1Phase3tut; }
                else if(id == 3) { thisDirector = Tiet3_LT1Phase4tut; }
                break;
            case "LT2":
                if (id == 0) { thisDirector = Tiet3_LT2btut; }
                else if (id == 1) { thisDirector = Tiet3_LT2ctut; }
                else if (id == 2) { thisDirector = Tiet3_LT2atut; }
                break;
            case "LT3":
                thisDirector = Tiet3_LT3tut;
                break;
            case "LT4":
                if (id == 0) { thisDirector = Tiet3_LT4_1tut; }
                else if (id == 1) { thisDirector = Tiet3_LT4_2tut; }
                break;
            case "LT5":
                if (id == 0) { thisDirector = Tiet3_LT5_1tut; }
                else if (id == 1) { thisDirector = Tiet3_LT5_2tut; }
                break;
            case "LT6":
                if (id == 0) { thisDirector = luyenTap1Tut; }
                else if (id == 1) { thisDirector = Tiet3_LT6_2tut; }
                break;
            case "Blank":
                if(id == 0) { thisDirector = Tiet3_haiTalk; }
                else if(id == 1) { thisDirector = Tiet3_lanTalk; }
                else if(id == 2) { thisDirector = Tiet3_giaovienTalk; }
                break;
        }

        thisDirector?.Stop();
        thisDirector?.Play();
        currentDirector = thisDirector;
    }

    public void Tiet5(string lvl, int id)
    {
        if (isCapturing) return;
        if (lvl == "Blank") { }
        else if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        PlayableDirector thisDirector = null;
        switch (lvl)
        {
            case "KP":
                thisDirector = Tiet5_KP[id-1];
                break;
            case "LT":
                thisDirector = Tiet5_LT[id-1];
                break;
            case "LT_2":
                thisDirector = Tiet5_LT2[id - 1];
                break;
            case "KT":
                thisDirector = Tiet5_TuKt[id-1];
                break;
        }

        thisDirector?.Stop();
        thisDirector?.Play();
        currentDirector = thisDirector;
    }

    public void Tiet6(string lvl, int id)
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        PlayableDirector thisDirector = null;
        switch (lvl)
        {
            case "KP":
                thisDirector = Tiet6_KP[id - 1];
                break;
            case "LT":
                thisDirector = Tiet6_LT[id - 1];
                break;
        }

        thisDirector?.Stop();
        thisDirector?.Play();
        currentDirector = thisDirector;
    }
    public void DemLPTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutDemLapPhuong?.Stop();
        Tiet4_tutDemLapPhuong?.Play();
        currentDirector = Tiet4_tutToMau;
    }
    public void DemCNTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutDemKhoiChuNhat?.Stop();
        Tiet4_tutDemKhoiChuNhat?.Play();
        currentDirector = Tiet4_tutToMau;
    }
    public void ChonLPTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutChonLapPhuong?.Stop();
        Tiet4_tutChonLapPhuong?.Play();
        currentDirector = Tiet4_tutToMau;
    }
    public void ChonCNTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutChonKhoiChuNhat?.Stop();
        Tiet4_tutChonKhoiChuNhat?.Play();
        currentDirector = Tiet4_tutToMau;
    }

    public void ChonMiengGhepTutPlay()
    {
        if (isCapturing) return;
        if (currentDirector != null)
        {
            currentDirector.Stop();
        }
        Tiet4_tutChonMiengGhep?.Stop();
        Tiet4_tutChonMiengGhep?.Play();
        currentDirector = Tiet4_tutToMau;
    }


    public void StopCurrentDirector()
    {
        if(currentDirector != null)
        {
            currentDirector.Stop();
        }
    }
    public double GetDurationOfCurrentDirector()
    {
        return currentDirector.duration;
    }
    public void CongratulationPlay()
    {
        if (isCapturing) return;
        rightDirector?.Stop();
        rightDirector?.Play();
    }
}
