using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]private Animator playerAnimator;

    [Header("Attaque Parameter")]
    public string attTrigger;
    public string attResetTrigger;

    [Header("Player Statue Parameter")]
    public string stunParameter;

    [Header("Adrenaline Parameter")]
    public string switchParameter;
    public string healParameter;

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
