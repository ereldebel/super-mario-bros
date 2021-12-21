using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FollowMarioOnlyToTheRight : MonoBehaviour
{
	[SerializeField] private Rigidbody2D marioRigidbody2D;

	private void Update()
	{
		if (marioRigidbody2D.velocity.x <= 0) return;
		if (marioRigidbody2D.position.x <= transform.position.x) return;
		var pos = transform.position;
		pos.x = marioRigidbody2D.position.x;
		transform.position = pos;
	}
}