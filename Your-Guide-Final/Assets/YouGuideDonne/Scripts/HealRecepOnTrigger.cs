using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HealRecepOnTrigger : MonoBehaviour
{
    bool alreadyTrigger;

    [FMODUnity.EventRef]
    [SerializeField] private string eventHealSfxName;
    [SerializeField] VisualEffect healVfx;
    [SerializeField] string eventVfx;

    private void Awake()
    {
        alreadyTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        ReceptacleControler rControler = other.GetComponent<ReceptacleControler>();

        if (rControler != null)
        {
            if (!alreadyTrigger)
            {
                rControler.rLife.TakeDamage(-Mathf.RoundToInt(rControler.rLife.maxLifeValue));
                alreadyTrigger = true;
                FMODUnity.RuntimeManager.PlayOneShot(eventHealSfxName, rControler.transform.position);
                healVfx.SendEvent(eventVfx);
            }
        }
    }
}
