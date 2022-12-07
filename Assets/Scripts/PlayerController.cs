using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rigid;
	private SpriteRenderer render;
	private Animator anim;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private float jumpPower;

	[SerializeField]
	private LayerMask groundLayer;

	private Vector2 moveVec
	{
		set
		{
			anim.SetFloat("xSpeed", value.x);
			anim.SetFloat("ySpeed", value.y);
		}
		get
		{
			return new Vector2(anim.GetFloat("xSpeed"), anim.GetFloat("ySpeed"));
		}
	}

	private bool IsGround
	{
		set
		{
			anim.SetBool("IsGround", value);
		}
		get
		{
			return anim.GetBool("IsGround");
		}
	}

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		Move();
		Jump();

		AnimatorUpdate();
	}

	private void FixedUpdate()
	{
		Vector2 startVec = new Vector2(transform.position.x, transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast(startVec, Vector2.down, 1.5f, groundLayer);
		Debug.DrawRay(startVec, Vector2.down * 1.5f, new Color(0, 0, 1));

		if (null != hit.collider)
		{
			IsGround = true;
		}
		else
		{
			IsGround = false;
		}
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
		if (Input.GetButtonDown("Jump") && IsGround)
		{
			rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			IsGround = false;
		}
	}

	private void AnimatorUpdate()
	{
		moveVec = new Vector2(Mathf.Abs(rigid.velocity.x), rigid.velocity.y);
	}
}
