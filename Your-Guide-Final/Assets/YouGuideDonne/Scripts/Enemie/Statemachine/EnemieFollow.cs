using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieFollow : StateMachineBehaviour
{
    private EnemiControler eControler;

    bool isAttacking;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        eControler = animator.GetComponent<EnemiRefControler>().eControler;
        eControler.eMovement.InizialisePath();
        isAttacking = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool canMove = eControler.eMovement.IsInRangeToFollowReceptacle() || eControler.eMovement.IsInRangeToFollowPlayer();
        bool canAttack = eControler.eMovement.IsInRangeToAttackTarget();

        if (canMove && !canAttack)
        {
            eControler.eMovement.Follow();
            eControler.eAnimator.SetVitesseParameterValue(2f);
        }
        else
        {
            eControler.eAnimator.SetVitesseParameterValue(0f);

        }

        if(canAttack && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger(eControler.eAnimator.attParameterName);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
}
