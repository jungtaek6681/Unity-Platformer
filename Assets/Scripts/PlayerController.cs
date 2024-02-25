using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Animator animator;

    [Header("Spec")]
    [SerializeField] float maxSpeed;
    [SerializeField] float accelPower;
    [SerializeField] float decelPower;
    [SerializeField] float jumpSpeed;
    [SerializeField] float maxFallSpeed;
    [SerializeField] LayerMask groundCheckLayer;

    private Vector2 inputDir;
    private bool isGround;
    private int groundCount;

    private void FixedUpdate()
    {
        Move();
        Fall();
    }

    private void Move()
    {
        // Accelation
        if (inputDir.x > 0 && rigidbody.velocity.x < maxSpeed)
        {
            rigidbody.AddForce(Vector2.right * inputDir.x * accelPower);
            animator.SetBool("Move", true);
            renderer.flipX = false;
        }
        else if (inputDir.x < 0 && rigidbody.velocity.x > -maxSpeed)
        {
            rigidbody.AddForce(Vector2.right * inputDir.x * accelPower);
            animator.SetBool("Move", true);
            renderer.flipX = true;
        }

        // Top Speed

        // Deceleration
        if (inputDir.x == 0 && rigidbody.velocity.x > 0.1f)
        {
            rigidbody.AddForce(Vector2.left * decelPower);
            animator.SetBool("Move", false);
        }
        else if (inputDir.x == 0 && rigidbody.velocity.x < -0.1f)
        {
            rigidbody.AddForce(Vector2.right * decelPower);
            animator.SetBool("Move", false);
        }
    }

    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
    }

    private void Fall()
    {
        if (rigidbody.velocity.y < maxFallSpeed)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, maxFallSpeed);
        }
        animator.SetFloat("YSpeed", rigidbody.velocity.y);
    }

    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && isGround)
        {
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount++;
            isGround = groundCount != 0;
            animator.SetBool("IsGround", isGround);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount--;
            isGround = groundCount != 0;
            animator.SetBool("IsGround", isGround);
        }
    }
}
