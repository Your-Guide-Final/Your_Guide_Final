using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiAttRange : StateMachineBehaviour
{
    private EnemiControler eControler;
    private EnnemiAttRangeStat eAttStats;

    [SerializeField] float timeDelay;

    //float timer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        eControler = animator.transform.GetComponent<EnemiRefControler>().eControler;
        eAttStats = eControler.transform.GetComponent<EnnemiAttRangeStat>();
        eAttStats.arme.LookAt(eControler.eMovement.currentTarget.position + eAttStats.targetOffset);
        eAttStats.StartSpawnCoroutine(timeDelay);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    /*override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }*/

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
