using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	[SerializeField] private GameObject[] monsterPrefabs = null;
	[SerializeField] private Image playerHealthbar_ = null;
	[SerializeField] private Text textScoreCount_ = null;
	[SerializeField] private AudioClip backgroundMusic_ = null;
	
	private bool _isGameOver = false;
	private Vector2 screenToWorldPoint = Vector2.zero;
	private int _monsterKillCount = 0;

	public int maxMonsterCount = 10;
	public float createTime = 1f;

	private void Start()
	{
		Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		screenToWorldPoint = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		SoundManager.instance.PlayMusic(backgroundMusic_);
		StartCoroutine(CreateMonster());
	}

	private IEnumerator CreateMonster()
	{
		while(!_isGameOver)
		{
			int count = GameObject.FindGameObjectsWithTag("Monster").Length;

			if(count < maxMonsterCount)
			{
				yield return new WaitForSeconds(createTime);

				int idx = Random.Range(0, monsterPrefabs.Length);

				Vector2 pos = new Vector2(Random.Range(-screenToWorldPoint.x, screenToWorldPoint.x), screenToWorldPoint.y);
				Instantiate(monsterPrefabs[idx], pos, Quaternion.identity);
			}
			else
			{
				yield return null;
			}
		}
	}

	public void SetPlayerHealth(float value)
	{
		playerHealthbar_.fillAmount = value;
	}

	public void SetMonsterKill()
	{
		_monsterKillCount += 1;
		textScoreCount_.text = string.Format("{0}", _monsterKillCount);
	}

	public void GoToTitle()
	{
		int score = PlayerPrefs.GetInt("SCORE");
		if(score < _monsterKillCount)
		{
			PlayerPrefs.SetInt("SCORE", _monsterKillCount);
		}
		SceneManager.LoadScene("Title");
	}
}
