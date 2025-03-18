using UnityEngine.Audio;
using System;
using UnityEngine;

public class Tiet3AudioManager : MonoBehaviour
{
	public static Tiet3AudioManager instance;
	[SerializeField] private Sound[] sounds;
	Sound curSound;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		curSound = s;

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stop()
	{
		Sound s = curSound;
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}

	public AudioClip getCurSoundClip()
    {
		return curSound.clip;
    }
}
