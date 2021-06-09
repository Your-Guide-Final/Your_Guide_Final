using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealRecepOnTrigger : MonoBehaviour
{
    bool alreadyTrigger;

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
            }
        }
    }
}
