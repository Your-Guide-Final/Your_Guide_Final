using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleWithTrigger : MonoBehaviour
{
    [SerializeField] CombatGestion combatGestion;

    bool alreadyTrigger;

    private void Awake()
    {
        alreadyTrigger = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerControler pControler = other.GetComponent<PlayerControler>();

        if ( pControler != null && !alreadyTrigger)
        {
            alreadyTrigger = true;
            combatGestion.StartBattle(pControler.transform);
        }
    }
}
