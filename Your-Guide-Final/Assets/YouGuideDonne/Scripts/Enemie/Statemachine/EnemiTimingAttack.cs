using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiTimingAttack : StateMachineBehaviour
{
    EnemiControler eControler;

    

    [Header("Degat Parameter")]
    
    [SerializeField] private float attRange;

    [Header("HitBox Parameter")]
    [SerializeField] private float effectiveRange;

    [Header("Timing")]
    [SerializeField] private float effectiveTimeBeforeDegat;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        eControler = animator.GetComponent<EnemiRefControler>().eControler;
        int degatValue = eControler.eAttack.degatValue;
        float bumpForce = eControler.eAttack.forceDegatPlayer;
        eControler.eAttack.StartDamageCoroutine(effectiveTimeBeforeDegat, degatValue, attRange, effectiveRange, bumpForce);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
