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

	protected Rigidbody2D _rigidbody = null;
	protected Animator _animator = null;
	protected State _state = State.Idle;

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
			Move();
		}
		else
		{
			Stop();
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

	private void Stop()
	{
		_rigidbody.velocity = Vector2.zero;
	}

	protected virtual void Move() {}
}
