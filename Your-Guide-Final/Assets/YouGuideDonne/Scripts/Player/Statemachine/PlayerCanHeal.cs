using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanHeal : StateMachineBehaviour
{
    private PlayerControler pControler;

    [Range(0,1)]
    [SerializeField] float minNormalizedTimeToHeal;
    [Range(0,1)]
    [SerializeField] float maxNormalizedTimeToHeal;

    bool canHeal;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //pControler = animator.GetComponent<PlayerControler>();
        pControler = animator.GetComponentInParent<PlayerControler>();

        canHeal = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool canTrigger = pControler.pHeal.CanHeal() && pControler.pHeal.EnoughAdrenalineToStartHeal() && getIsOnTime(stateInfo);

        if (Input.GetButtonDown(pControler.pInput.healInput) && canTrigger && canHeal)
        {
            canHeal = false;
            
            animator.SetTrigger(pControler.pAnimator.healParameter);
            animator.SetBool(pControler.pAnimator.onHealParameter, true);
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


    bool getIsOnTime(AnimatorStateInfo stateInfo)
    {

        float currentAnimeTime = stateInfo.normalizedTime;
        currentAnimeTime = currentAnimeTime % 1;
        
        bool isOnTime = currentAnimeTime >= minNormalizedTimeToHeal && currentAnimeTime <= maxNormalizedTimeToHeal;
        return isOnTime;
    }
}
