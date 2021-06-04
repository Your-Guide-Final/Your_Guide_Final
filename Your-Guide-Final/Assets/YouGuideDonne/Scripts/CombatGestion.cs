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
    [SerializeField] float timeToRespawn;

    [Header("Start")]
    [SerializeField] float timeBeforeSpawn;
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
        Debug.Log("StartBattle");
        onBattle = true;
        ChangeEtatMur(true);
        if (nbEnnemiCacStart > 0)
        {
            StartCoroutine(SpawnEnnemi(spawnPointCac, prefabEnnemiCac, nbEnnemiCacStart, timeBeforeSpawn));

        }
        if (nbEnnemiRangeStart > 0)
        {
            StartCoroutine(SpawnEnnemi(spawnPointRange, prefabEnnemiRange, nbEnnemiRangeStart, timeBeforeSpawn));

        }
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

    




    public IEnumerator SpawnEnnemi(List<Transform> spawnPoint,GameObject ennemiObject, int nbEnnemiToSpawn, float timeDecal)
    {
        yield return new WaitForSeconds(timeDecal);

        Debug.Log("StartSpawn");

        for (int i = 0; i < nbEnnemiToSpawn; i++)
        {
            Debug.Log("StartCheckIfCanSpawn");

            int randomIndex = Random.Range(0, spawnPoint.Count);
            while (!CheckIfSpawnIsClear(spawnPoint[randomIndex]))
            {
                randomIndex += 1;
                if (randomIndex >= spawnPoint.Count)
                {
                    randomIndex = 0;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            Debug.Log("SpawnEnnemi");
            GameObject newEnnemi =Instantiate(ennemiObject, spawnPoint[randomIndex].position, spawnPoint[randomIndex].rotation);
            newEnnemi.GetComponent<EnemiControler>().eLife.combatGestion = this;
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }


    public void Respawn(ennemiType type)
    {
        Debug.Log("respawn");
        switch (type)
        {
            case ennemiType.Cac:

                if (nbEnnemiCac > 0)
                {
                    StartCoroutine(SpawnEnnemi(spawnPointCac, prefabEnnemiCac, 1, timeToRespawn));
                }
                nbEnnemiCac = Mathf.Clamp(nbEnnemiCac - 1, 0, nbEnnemiCacToKill);

                break;

            case ennemiType.Range:

                if (nbEnnemiRange > 0)
                {
                    StartCoroutine(SpawnEnnemi(spawnPointRange, prefabEnnemiRange, 1, timeToRespawn));
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
