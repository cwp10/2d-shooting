using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField] private GameManager gameManager_ = null;
	[SerializeField] private GameObject bulletPrefab_ = null;
	[SerializeField] private Transform bullerSpawnPoint_ = null;
	[SerializeField] private GameObject effect_ = null;

	private const float fPixelPerUnit = 100.0f;

	private Camera _camera = null;
	private Rigidbody2D _rigidbody = null;
	private Vector2 _bodySize = Vector2.zero;
	private float _interval = 0;
	private float _health = 100.0f;
	
	public float speed = 200.0f;
	public float bulletInterval = 0.1f;
	public float maxHealth = 100.0f;
	

	private void Awake()
	{
		_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		_rigidbody = GetComponent<Rigidbody2D>();

		_bodySize = CalculateBodySize();
	}

	private void Start()
	{
		_rigidbody.position = _camera.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, -Screen.height + _bodySize.y * 0.5f));
		_rigidbody.position = new Vector2(_rigidbody.position.x, -4.0f);
		_health = maxHealth;
	}

	private Vector2 CalculateBodySize()
	{
		BoxCollider2D boxCollider_ = GetComponent<BoxCollider2D>();
		return boxCollider_.size * fPixelPerUnit;
	}

	private void Update()
	{
		_interval += Time.deltaTime;

		MouseInput();
		KeybordInput();
	}

    private void MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
			Vector2 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
			
			_rigidbody.position = Vector2.Lerp(_rigidbody.position, pos, speed * 0.02f * Time.deltaTime);
			CreateBullet();
        }
    }

	private void KeybordInput()
	{
		Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Monster")
		{
			Instantiate(effect_, this.transform.position, Quaternion.identity);
			_health -= 10f;
			
			if(_health > 0)
			{
				float value = _health / maxHealth;
				gameManager_.SetPlayerHealth(value);
			}
			else
			{
				gameManager_.SetPlayerHealth(0);
				gameManager_.GoToTitle();
			}
		}
	}
}
