using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGestion : MonoBehaviour
{
    [Header("Zone")]
    [SerializeField] GameObject prefabEnnemiCac;
    [SerializeField] GameObject prefabEnnemiRange;

    [SerializeField] List<Transform> spawnPointCac;
    [SerializeField] List<Transform> spawnPointRange;

    [SerializeField] List<GameObject> murZoneBattle;

    [Header("Start")]
    [SerializeField] int nbEnnemiCacStart;
    int nbEnnemiCac;
    [SerializeField] int nbEnnemiRangeStart;
    int nbEnnemiRange;

    private void Awake()
    {
        ChangeEtatMur(false);
    }

    public void StartBattle()
    {

    }

    public void ChangeEtatMur(bool etat)
    {
        foreach (var item in murZoneBattle)
        {
            item.SetActive(etat);
        }
    }

}
