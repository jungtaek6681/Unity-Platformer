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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			IsGround = true;
		}
	}
}
