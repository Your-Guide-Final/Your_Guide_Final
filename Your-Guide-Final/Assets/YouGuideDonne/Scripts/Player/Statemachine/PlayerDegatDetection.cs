using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDegatDetection : StateMachineBehaviour
{
    PlayerControler pControler;



    [Header("Degat Parameter")]
    public int degatValue;
    public float attRange;
    public float bumpForce;
    public bool getAdrenaline;
    public bool canBump;

    [Header("HitBox Parameter")]
    public bool useConeDetection;
    public float effectiveRange;

    [Header("Timing")]
    public float effectiveTimeBeforeDegat;

    [Header("FX")]
    public PlayerFX.typeOfAttack typeOfAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pControler = FindObjectOfType<PlayerControler>();
        pControler.pAttaque.StartDamageCoroutine( effectiveTimeBeforeDegat, useConeDetection, degatValue, attRange, effectiveRange, bumpForce, typeOfAttack, getAdrenaline,canBump);
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
