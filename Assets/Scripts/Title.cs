using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour 
{
	[SerializeField] private Button startButton_ = null;

	public void OnClickStart()
	{
		SceneManager.LoadScene("Play");
	}
	
}
