using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource = null;
    public static SoundManager instance = null;
  
    void Awake()
    {
        if (instance == null)
		{
			instance = this;
		}
        else if (instance != this)
		{
			Destroy(gameObject);
		}
        DontDestroyOnLoad(gameObject);
    }


    public void PlaySFX(AudioClip clip)
    {
		GameObject go = new GameObject();
		AudioSource sfx = go.AddComponent<AudioSource>();
        sfx.clip = clip;
        sfx.Play();
		Destroy(go, clip.length);
    }

	public void PlayMusic(AudioClip clip)
	{
		musicSource.clip = clip;
		musicSource.Play();
	}
}