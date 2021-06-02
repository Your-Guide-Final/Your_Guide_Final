using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGestion : MonoBehaviour
{
    public enum ennemiType { Cac,Range};


    [Header("Spawn")]
    [SerializeField] GameObject prefabEnnemiCac;
    [SerializeField] GameObject prefabEnnemiRange;

    [SerializeField] List<Transform> spawnPointCac;
    [SerializeField] List<Transform> spawnPointRange;

    [SerializeField] float minRangeBetweenEnnemi;
    [SerializeField] LayerMask spawnCheckLayer;

    [Header("Start")]
    [SerializeField] List<GameObject> murZoneBattle;
    [SerializeField] int nbEnnemiCacStart;
    int nbEnnemiCac;
    [SerializeField] int nbEnnemiRangeStart;
    int nbEnnemiRange;

    [Header("Battle")]
    [SerializeField] int nbEnnemiCacToKill;
    [SerializeField] int nbEnnemiRangeToKill;
    //[SerializeField] int nbEnnemiCacToRespawn;
    //[SerializeField] int nbEnnemiRangeToRespawn;

    

    bool onBattle;

    private void Awake()
    {
        ChangeEtatMur(false);
        onBattle = false;

    }

    private void Update()
    {
        if (onBattle)
        {
            bool endBattle = nbEnnemiCac == 0 && nbEnnemiRange == 0;

            if (endBattle)
            {
                EndBattle();
            }
        }
    }




    public void StartBattle()
    {
        onBattle = true;
        ChangeEtatMur(true);
        StartCoroutine(SpawnEnnemi(spawnPointCac, prefabEnnemiCac, nbEnnemiCacStart));
        StartCoroutine(SpawnEnnemi(spawnPointRange, prefabEnnemiRange, nbEnnemiRangeStart));
        nbEnnemiCac = nbEnnemiCacToKill;
        nbEnnemiRange = nbEnnemiRangeToKill;
    }

    public void EndBattle()
    {
        onBattle = false;
        ChangeEtatMur(false);
    }

    public void ChangeEtatMur(bool etat)
    {
        foreach (var item in murZoneBattle)
        {
            item.SetActive(etat);
        }
    }

    




    public IEnumerator SpawnEnnemi(List<Transform> spawnPoint,GameObject ennemiObject, int nbEnnemiToSpawn)
    {
        for (int i = 0; i < nbEnnemiToSpawn; i++)
        {
            int randomIndex = Random.Range(0, spawnPoint.Count);
            while (CheckIfSpawnIsClear(spawnPoint[randomIndex]))
            {
                randomIndex += 1;
                if (randomIndex > spawnPoint.Count)
                {
                    randomIndex = 0;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }

            GameObject newEnnemi =Instantiate(ennemiObject, spawnPoint[randomIndex].position, spawnPoint[randomIndex].rotation);
            newEnnemi.GetComponent<EnemiControler>().eLife.combatGestion = this;
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }


    public void Respawn(ennemiType type)
    {
        switch (type)
        {
            case ennemiType.Cac:

                if (nbEnnemiCac > 0)
                {
                    SpawnEnnemi(spawnPointCac, prefabEnnemiCac, 1);
                }
                nbEnnemiCac = Mathf.Clamp(nbEnnemiCac - 1, 0, nbEnnemiCacToKill);

                break;

            case ennemiType.Range:

                if (nbEnnemiRange > 0)
                {
                    SpawnEnnemi(spawnPointRange, prefabEnnemiRange, 1);
                }
                nbEnnemiRange = Mathf.Clamp(nbEnnemiRange - 1, 0, nbEnnemiRangeToKill);

                break;
        }
    }


    public bool CheckIfSpawnIsClear(Transform pointToCheck)
    {
        Collider[] hitSphere = Physics.OverlapSphere(pointToCheck.position, minRangeBetweenEnnemi, spawnCheckLayer);

        if (hitSphere.Length != 0)
        {
            return false;
        }
        else
        {
            return true;

        }
    }

}
