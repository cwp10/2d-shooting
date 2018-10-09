using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firemoth : Monster
{
	protected override void Move()
	{
		Vector2 dir = Vector2.down;
		_rigidbody.velocity = dir * speed * Time.deltaTime;
	}
}
