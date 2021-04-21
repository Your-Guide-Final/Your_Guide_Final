using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptacleFollow : StateMachineBehaviour
{
    private ReceptacleControler rControler;

    private bool alreadyScared;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rControler = animator.GetComponentInParent<ReceptacleControler>();
        rControler.rMovement.InizialisePath();
        alreadyScared = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool canFollow = rControler.rMovement.IsInRangeToFollow();
        bool canSprint = rControler.rMovement.IsInRangeToSprint();
        bool canStop = rControler.rMovement.IsInRangeToStop();
        bool isScared = rControler.rMovement.IsInrangeToBeScared();

        if (canFollow && !canStop && !isScared)
        {
            if (canSprint)
            {
                animator.SetFloat(rControler.rAnimator.vitesseParameterName, 1f);
                float vitesse = rControler.rMovement.vitesseSprint;
                float emission = rControler.rMovement.emissionRateRun;
                rControler.rMovement.Follow(vitesse, emission);
            }
            else
            {
                animator.SetFloat(rControler.rAnimator.vitesseParameterName, 0.5f);
                float vitesse = rControler.rMovement.vitesseFollow;
                float emission = rControler.rMovement.emissionRateWalk;
                rControler.rMovement.Follow(vitesse, emission);

            }
        }
        else if (isScared)
        {
            animator.SetFloat(rControler.rAnimator.vitesseParameterName, -1f);
            rControler.rMovement.SetEmissionParticuleToNull();
            /*alreadyScared = true;
            animator.SetBool(rControler.rAnimator.scaredParameterName, true);*/
        }
        else
        {
            animator.SetFloat(rControler.rAnimator.vitesseParameterName, 0f);
            rControler.rMovement.SetEmissionParticuleToNull();
        }

        /*if (canStop)
        {
        }*/

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
