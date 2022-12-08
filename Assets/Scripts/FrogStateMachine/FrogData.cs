using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class FrogData : MonoBehaviour
{
	private Rigidbody2D rigid;
	private Animator anim;

	[SerializeField]
	private LayerMask groundLayer;

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

	private float YSpeed
	{
		set
		{
			anim.SetFloat("YSpeed", value);
		}
		get
		{
			return anim.GetFloat("YSpeed");
		}
	}

	private void Awake()
	{
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		GroundCheck();
		UpdateAnimator();
	}

	private void GroundCheck()
	{
		Vector2 startVec = new Vector2(transform.position.x, transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast(startVec, Vector2.down, 1f, groundLayer);
		Debug.DrawRay(startVec, Vector2.down * 1f, new Color(1, 0, 0));

		if (null != hit.collider)
		{
			IsGround = true;
		}
		else
		{
			IsGround = false;
		}
	}

	private void UpdateAnimator()
	{
		YSpeed = rigid.velocity.y;
	}
}