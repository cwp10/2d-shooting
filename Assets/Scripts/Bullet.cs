using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	private Rigidbody2D _rigidbody = null;

	public float speed = 500.0f;

	private void Awake () 
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_rigidbody.velocity = new Vector2(0, speed * Time.deltaTime);
	}
}
