using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnimator;

    [Header("Attaque Parameter")]
    public string attTrigger;
    public string attResetTrigger;

    [Header("Player Statue Parameter")]
    public string stunParameter;
    public string bumpParameter;

    [Header("Adrenaline Parameter")]
    public string switchParameter;
    public string healParameter;
    public string onHealParameter;

    [Header("Movement Parameter")]
    public string speedParameter;

    public void TriggerSwitchParameter()
    {
        playerAnimator.SetTrigger(switchParameter);
    }

    public void TriggerAttparameter()
    {
        playerAnimator.SetTrigger(attTrigger);
    }

    public void SetVitesseParameterValue(float value)
    {
        playerAnimator.SetFloat(speedParameter, value);
    }
}
