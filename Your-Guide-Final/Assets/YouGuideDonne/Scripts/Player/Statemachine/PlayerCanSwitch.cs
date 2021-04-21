using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanSwitch : StateMachineBehaviour
{
    private PlayerControler pControler;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //pControler = animator.GetComponent<PlayerControler>();
        pControler = FindObjectOfType<PlayerControler>();

        bool canSwitch = pControler.pAdrenaline.IsAdrenalineMax() && pControler.pSwitch.IsInRange();
        //pControler.pSwitch.ChangeSignEnable(canSwitch);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool canSwitch = pControler.pAdrenaline.IsAdrenalineMax() && pControler.pSwitch.IsInRange();
        //pControler.pSwitch.ChangeSignEnable(canSwitch);

        if (pControler.pInput.GetSwitchInputDown() && canSwitch)
        {
            pControler.pSwitch.StartSwitch();
            Debug.Log("Switch");
        }
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //pControler.pSwitch.ChangeSignEnable(false);
        
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
}
