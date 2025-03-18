using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    [Header("Audio sources")]
    public AudioSource source, source1, source3;
    public const string sound = "Sound", music = "Music", vibrate ="Vibrate";
    [Header("Music clip")]
    public AudioClip BGSound;
    [Header("Booleans")]
    public bool soundBool, musicBool, vibrateBool;
    private void Awake()
    {
        MakeInstance();
    }
    private void Start()
    {
        soundBool = IsSoundOn();
        musicBool = IsMusicOn();
        vibrateBool = IsVibrateOn();
        Background();
    }
    
    public void Background()
    {
        if (source1.clip == null)
        {
            if (musicBool)
            {
                source1.clip = BGSound;
                source1.loop = true;
                source1.Play();
            }
        }
        else if ( (source1.clip.name != BGSound.name && musicBool ) || (!source1.isPlaying && musicBool))
        {
            source1.clip = BGSound;
            source1.loop = true;
            source1.Play();
        }
    }
    public void InGameMusic()
    {
        if ((source1.clip != null && source1.clip.name == BGSound.name && musicBool) || (!source1.isPlaying && musicBool))
        {
            
        }
    }
    public void StopMusic()
    {
        source1.Stop();
        musicBool = false;
        SetMusicState(0);
    }
    public void StopMusicTemporary()
    {
        source1.Stop();
    }
    public void SoundOff()
    {
        soundBool = false;
        SetSoundState(0);
    }
    public void MusicOn()
    {
        musicBool = true;
        SetMusicState(1);
        Background();
    }
    public void VibrateOn()
    {
        vibrateBool = true;
        SetVibrateState(1);
    }
    public void VibrateOff()
    {
        vibrateBool = false;
        SetVibrateState(0);
    }
    public void SoundOn()
    {
        soundBool = true;
        SetSoundState(1);
    }
    public static void FirstInit()
    {
        PlayerPrefs.SetInt(sound, 1);
        PlayerPrefs.SetInt(music, 1);
        PlayerPrefs.SetInt(vibrate, 1);
    }
    public static bool IsSoundOn()
    {
        return 1 == PlayerPrefs.GetInt(sound);
    }
    public static bool IsVibrateOn()
    {
        return 1 == PlayerPrefs.GetInt(vibrate);
    }
    public static bool IsMusicOn()
    {
        return 1 == PlayerPrefs.GetInt(music);
    }
    public static void SetSoundState(int t)
    {
        if (t == 1)
        {
            PlayerPrefs.SetInt(sound, 1);
        }
        else if (t == 0)
        {
            PlayerPrefs.SetInt(sound, 0);
        }
        else
        {
            Debug.Log("Sai so roi, k chuyen trang thai Sound trong PlayerPref dc");
        }
    }
    public static void SetVibrateState(int t)
    {
        if (t == 1)
        {
            PlayerPrefs.SetInt(vibrate, 1);
        }
        else if (t == 0)
        {
            PlayerPrefs.SetInt(vibrate, 0);
        }
        else
        {
            Debug.Log("Sai so roi, k chuyen trang thai Vibrate trong PlayerPref dc");
        }
    }
    public static void SetMusicState(int t)
    {
        if (t == 1)
        {
            PlayerPrefs.SetInt(music, 1);
        }
        else if (t == 0)
        {
            PlayerPrefs.SetInt(music, 0);
        }
        else
        {
            Debug.Log("Sai so roi, k chuyen trang thai Music trong PlayerPref dc");
        }
    }
    public void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
