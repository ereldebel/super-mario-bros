using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ThrowFireball : MonoBehaviour
{
	[SerializeField] private KeyCode rightKey = KeyCode.RightArrow;
	[SerializeField] private KeyCode leftKey = KeyCode.LeftArrow;
	[SerializeField] private KeyCode throwKey = KeyCode.LeftControl;
	[SerializeField] private GameObject Fireball;
	[SerializeField] private float throwForce;

	private Vector2 throwDirection = Vector2.right;

	private void Update()
	{
		if (Input.GetKey(rightKey))
			throwDirection = Vector2.right;
		if (Input.GetKey(leftKey))
			throwDirection = Vector2.left;
		if (!Input.GetKeyDown(throwKey)) return;
		var fireball = Instantiate(Fireball, transform.position, Quaternion.identity);
		fireball.GetComponent<Rigidbody2D>().velocity = throwDirection * throwForce;
	}
}