using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimmingAttaque : StateMachineBehaviour
{
    private PlayerControler pControler;

    [Header("Combo")]
    bool canCombo;
    [SerializeField] float minNormalizedTimeToCombo;
    [SerializeField] float maxNormalizedTimeToCombo;

    

    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //pControler = animator.GetComponent<PlayerControler>();
        pControler = FindObjectOfType<PlayerControler>();

        canCombo = true;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetButtonDown(pControler.pInput.attInput) && getIsOnTime(stateInfo) && canCombo)
        {
            canCombo = false;
            Debug.Log("attack");
            animator.SetTrigger(pControler.pAnimator.attTrigger);
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
        //Debug.Log(currentAnimeTime);
        bool isOnTime = currentAnimeTime >= minNormalizedTimeToCombo && currentAnimeTime <= maxNormalizedTimeToCombo;
        return isOnTime;
    }
}
