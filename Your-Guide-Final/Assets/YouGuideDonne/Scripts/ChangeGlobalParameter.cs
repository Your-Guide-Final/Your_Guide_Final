using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class ChangeGlobalParameter : StateMachineBehaviour
{
    public enum TriggerEvent { Start, Exit };

    [System.Serializable]
    public class ParamEvent
    {
        [FMODUnity.ParamRef]
        public string paraName;

        public float parameterValue;

        
        public TriggerEvent triggerEvent=TriggerEvent.Start;

    }

    [SerializeField] private List<ParamEvent> paramEventsList = new List<ParamEvent>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < paramEventsList.Count; i++)
        {
            if(paramEventsList[i].triggerEvent == TriggerEvent.Start)
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName(paramEventsList[i].paraName, paramEventsList[i].parameterValue);

            }

        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < paramEventsList.Count; i++)
        {
            if (paramEventsList[i].triggerEvent == TriggerEvent.Exit)
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName(paramEventsList[i].paraName, paramEventsList[i].parameterValue);

            }

        }

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
