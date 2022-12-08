using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PigController : MonoBehaviour
{
	private Rigidbody2D rigid;
	private Vector2 moveDir = Vector2.right;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private Transform groundCheckPosition;

	[SerializeField]
	private LayerMask groundLayer;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Move();
	}

	private void FixedUpdate()
	{
		GroundCheck();
	}

	private void Move()
	{
		rigid.velocity = new Vector2(transform.right.x * moveSpeed, rigid.velocity.y);
	}

	private void GroundCheck()
	{
		Vector2 startVec = new Vector2(groundCheckPosition.position.x, groundCheckPosition.position.y);
		RaycastHit2D hit = Physics2D.Raycast(startVec, Vector2.down, 3f, groundLayer);
		Debug.DrawRay(startVec, Vector2.down * 3f, new Color(1, 0, 0));

		if (null == hit.collider)
		{
			Turn();
		}
	}

	private void Turn()
	{
		moveDir *= -1;
		transform.rotation = Quaternion.Euler(0, 90 + moveDir.x * 90, 0);
	}
}
