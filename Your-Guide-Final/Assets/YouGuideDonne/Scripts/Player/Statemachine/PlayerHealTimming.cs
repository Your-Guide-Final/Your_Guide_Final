using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealTimming : StateMachineBehaviour
{
    PlayerControler pControler;
    bool isActive;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isActive = true;
        pControler = animator.GetComponentInParent<PlayerControler>();
        bool canHeal = pControler.pHeal.CanHeal();
        if (canHeal)
        {
            pControler.pHeal.Heal();

        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool onHeal = Input.GetButton(pControler.pInput.healInput) && pControler.pHeal.CanHeal();
        if (onHeal)
        {
            if (isActive)
            {
                Debug.Log("Player is Healing");
                pControler.pHeal.Heal();
            }

        }
        else
        {
            Debug.Log("Player Heal is stop");
            isActive = false;
            pControler.pAnimator.playerAnimator.SetBool(pControler.pAnimator.onHealParameter, false);
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
