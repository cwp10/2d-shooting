﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField] private GameObject bulletPrefab_ = null;
	[SerializeField] private Transform bullerSpawnPoint_ = null;
	private const float fPixelPerUnit = 100.0f;

	private Camera _camera = null;
	private Rigidbody2D _rigidbody = null;
	private Vector2 _bodySize = Vector2.zero;
	private float _interval = 0;
	
	public float speed = 200.0f;
	public float bulletInterval = 0.1f;

	private void Awake()
	{
		_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		_rigidbody = GetComponent<Rigidbody2D>();

		_bodySize = CalculateBodySize();
	}

	private Vector2 CalculateBodySize()
	{
		BoxCollider2D boxCollider_ = GetComponent<BoxCollider2D>();
		return boxCollider_.size * fPixelPerUnit;
	}

	private void Update()
	{
		Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		_interval += Time.deltaTime;

		if(Input.GetKey(KeyCode.Space))
		{
			CreateBullet();
		}
	}

	private void Move(float horizontal, float vertical)
	{
		Vector2 dir = new Vector2(horizontal, vertical);
		_rigidbody.velocity = dir * speed * Time.deltaTime;
		
		Vector2 pos = _camera.WorldToScreenPoint(_rigidbody.position);

		float posX = Mathf.Clamp(pos.x, _bodySize.x * 0.5f, Screen.width - _bodySize.x * 0.5f);
		float posY = Mathf.Clamp(pos.y, _bodySize.y * 0.5f, Screen.height - _bodySize.y * 0.5f);
		
		_rigidbody.position = _camera.ScreenToWorldPoint(new Vector2(posX, posY));
	}

	private void CreateBullet()
	{
		if(_interval > bulletInterval)
		{
			_interval = 0.0f;
			Instantiate(bulletPrefab_, bullerSpawnPoint_.position, Quaternion.identity);
		}
	}
}
