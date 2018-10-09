using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour 
{
	private Rigidbody2D _rigidbody = null;

	public float speed = 50.0f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_rigidbody.velocity = Vector2.down * speed * Time.deltaTime;
	}
}
