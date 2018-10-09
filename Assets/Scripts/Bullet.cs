using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	private Rigidbody2D _rigidbody = null;
	private Camera _camera = null;

	public float speed = 500.0f;

	private void Awake () 
	{
		_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_rigidbody.velocity = new Vector2(0, speed * Time.deltaTime);

		Vector2 pos = _camera.WorldToScreenPoint(_rigidbody.position);

		if(pos.y > Screen.height)
		{
			Destroy(this.gameObject);
		}
	}
}
