using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour 
{
	public enum State
	{
		Idle,
		Hit,
	}

	private Rigidbody2D _rigidbody = null;
	private Animator _animator = null;
	private State _state = State.Idle;

	public float speed = 50.0f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_state = State.Idle;
	}

	private void Update()
	{
		if(_state == State.Idle)
		{
			_rigidbody.velocity = Vector2.down * speed * Time.deltaTime;
		}
		else
		{
			_rigidbody.velocity = Vector2.zero;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Bullet" && _state == State.Idle)
		{
			_state = State.Hit;
			_animator.SetTrigger("Hit");
			
			Invoke("Dead", 1.0f);
		}
	}

	private void Dead()
	{
		Destroy(this.gameObject);
	}
}
