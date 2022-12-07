using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rigid;
	private SpriteRenderer render;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private float jumpPower;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		Move();
		Jump();
	}

	private void Move()
	{
		rigid.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rigid.velocity.y);

		if (rigid.velocity.x > 0)
		{
			render.flipX = false;
		}
		else if (rigid.velocity.x < 0)
		{
			render.flipX = true;
		}
	}

	private void Jump()
	{
		if (Input.GetButtonDown("Jump"))
		{
			rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}
	}
}
