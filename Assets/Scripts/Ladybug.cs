using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : Monster 
{
	protected override void Move()
	{
		Vector2 dir = new Vector2(Mathf.Sin(_rigidbody.position.y), -1);
		_rigidbody.velocity = dir * speed * Time.deltaTime;
	}
}
