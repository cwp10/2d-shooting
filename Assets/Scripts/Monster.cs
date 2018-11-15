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

	[SerializeField] protected GameObject effect_ = null;
	[SerializeField] private AudioClip damageSfx_ = null;

	protected Rigidbody2D _rigidbody = null;
	protected Animator _animator = null;
	protected State _state = State.Idle;
	
	private GameManager _gameManager = null;
	private Vector2 _deadZone = Vector2.zero;
	private float _health = 0.0f;

	public float speed = 50.0f;
	public float maxHealth = 10.0f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		_deadZone = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
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

		if(this.transform.position.y < -_deadZone.y)
		{
			Destroy(this.gameObject);
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
		_gameManager.SetMonsterKill();
		SoundManager.instance.PlaySFX(damageSfx_);
		Instantiate(effect_, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}

	private void Stop()
	{
		_rigidbody.velocity = Vector2.zero;
	}

	protected virtual void Move() {}
}
