using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdle : StateMachineBehaviour
{
	private FrogData data;
	private Animator anim;
	private Rigidbody2D rigid;

	[SerializeField]
	private float jumpDelay;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		anim = animator;
		data = animator.GetComponent<FrogData>();
		rigid = animator.GetComponent<Rigidbody2D>();

		rigid.velocity = new Vector2(0, rigid.velocity.y);
		data.StartCoroutine(DelayJump());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

	private IEnumerator DelayJump()
	{
		yield return new WaitForSeconds(jumpDelay);
		anim.SetTrigger("Jump");
	}
}
