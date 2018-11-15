using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour 
{
	[SerializeField] private Button startButton_ = null;
	[SerializeField] private Text scoreText_ = null;
	[SerializeField] private AudioClip backgroundMusic_ = null;

	private void Start()
	{
		SoundManager.instance.PlayMusic(backgroundMusic_);
		int score = PlayerPrefs.GetInt("SCORE", 0);
		scoreText_.text = "최고점수 : " + score;
	}

	public void OnClickStart()
	{
		SceneManager.LoadScene("Play");
	}
}
