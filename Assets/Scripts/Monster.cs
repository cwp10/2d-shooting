using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour 
{
	public enum State
	{
		Idle,
		Hit,
		Dead,
	}

	protected Rigidbody2D _rigidbody = null;
	protected Animator _animator = null;
	protected State _state = State.Idle;
	
	private float _health = 0.0f;

	public float speed = 50.0f;
	public float maxHealth = 10.0f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_state = State.Idle;
		_health = maxHealth;
	}

	private void Update()
	{
		if(_state != State.Dead)
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
		if(other.tag == "Bullet" && _state != State.Dead)
		{
			_state = State.Hit;
			_animator.SetTrigger("Hit");

			_health -= 2.0f;

			if(_health <= 0)
			{
				_state = State.Dead;
				Invoke("Dead", 0.3f);
			}
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
