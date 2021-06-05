using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptacleAnimator : MonoBehaviour
{
    private ReceptacleControler rControler;

    
    public Animator receptacleAnimator;

    public string followParameterName;
    public string sprintParameterName;
    public string stunParameterName;
    public string stunTriggerParameterName;
    public string scaredParameterName;
    public string switchParametername;
    public string switchTriggerParametername;
    public string vitesseParameterName;
    public string healParameterName;

    private void Awake()
    {
        if (receptacleAnimator == null)
        {
            receptacleAnimator = transform.GetComponent<Animator>();

        }
        rControler = transform.GetComponent<ReceptacleControler>();
    }

    public void SetParameterValue()
    {
        /*bool follow = rControler.rMovement.IsInRangeToFollow();
        receptacleAnimator.SetBool(followParameterName, follow);
        rControler.rStatue.isFollow = follow;

        bool sprint = rControler.rMovement.IsInRangeToSprint();
        receptacleAnimator.SetBool(sprintParameterName, sprint);
        rControler.rStatue.isSprint = sprint;

        bool scared = rControler.rMovement.IsInrangeToBeScared();
        receptacleAnimator.SetBool(scaredParameterName, scared);
        rControler.rStatue.isScared = scared;*/

    }
}
