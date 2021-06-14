using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGestion : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        [Header("Ennemi Cac")]
        public GameObject prefabEnemiCac;
        public int nbEnemiCac;
        public List<Transform> spawnPointCac;

        [Header("Ennemi Range")]
        public GameObject prefabEnemiRange;
        public int nbEnemiRange;
        public List<Transform> spawnPointRange;

        [Header("Wave")]
        public int nbEnemiToKillToNextWave;
        public float timeDelayBeforeNextWave;
        
    }


    public enum ennemiType { Cac,Range};


    [Header("Wave")]
    public List<Wave> waves;

    [Header("Spawn")]
    [SerializeField] float minRangeBetweenEnnemi;
    [SerializeField] LayerMask spawnCheckLayer;
    //[SerializeField] float timeToRespawn;

    [Header("Start")]
    [SerializeField] float timeBeforeFirstWave;
    [SerializeField] List<Animator> murZoneBattle;
    [SerializeField] string openWallParameter;

    

    
    int nbEnnemiCac;
    
    int nbEnnemiRange;

    int nbEnemi;

    bool onBattle;

    int currentWaveIndex;
    int nbEnemiKilled;

    CameraManager camManager;
    SoundManager soundManager;

    private void Awake()
    {
        ChangeEtatMur(true);
        onBattle = false;
        currentWaveIndex = 0;
        nbEnemiKilled=0;
        nbEnemi = 0;
        camManager = FindObjectOfType<CameraManager>();
        soundManager = FindObjectOfType<SoundManager>();
        
    }

    private void Update()
    {
        if (onBattle)
        {
            if (IfWaveClear())
            {
                if(currentWaveIndex< waves.Count - 1)
                {
                    NextWave();

                }
                else
                {
                    if (nbEnemi == 0)
                    {
                        EndBattle();

                    }
                }
            }

        }
    }




    public void StartBattle()
    {
        //Debug.Log("StartBattle");
        onBattle = true;
        ChangeEtatMur(false);

        if (waves[currentWaveIndex].nbEnemiCac > 0)
        {
            StartCoroutine(SpawnEnnemi(waves[currentWaveIndex].spawnPointCac, waves[currentWaveIndex].prefabEnemiCac, waves[currentWaveIndex].nbEnemiCac, timeBeforeFirstWave));

        }
        if (waves[currentWaveIndex].nbEnemiRange > 0)
        {
            StartCoroutine(SpawnEnnemi(waves[currentWaveIndex].spawnPointRange, waves[currentWaveIndex].prefabEnemiRange, waves[currentWaveIndex].nbEnemiRange, timeBeforeFirstWave));

        }

        nbEnemi += waves[currentWaveIndex].nbEnemiCac + waves[currentWaveIndex].nbEnemiRange;

        camManager.ChangeActifCamera(2);
        soundManager.StartBattleMusic();

    }

    public void EndBattle()
    {
        onBattle = false;
        ChangeEtatMur(true);
        camManager.ChangeActifCamera(0);
        soundManager.StopBattleMusic();
    }

    public void ChangeEtatMur(bool etat)
    {
        foreach (var item in murZoneBattle)
        {
            item.SetBool(openWallParameter,etat);
        }
    }

    




    public IEnumerator SpawnEnnemi(List<Transform> spawnPoint,GameObject ennemiObject, int nbEnnemiToSpawn, float timeDecal)
    {
        yield return new WaitForSeconds(timeDecal);

        //Debug.Log("StartSpawn");

        for (int i = 0; i < nbEnnemiToSpawn; i++)
        {
            //Debug.Log("StartCheckIfCanSpawn");

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
            //Debug.Log("SpawnEnnemi");
            GameObject newEnnemi = Instantiate(ennemiObject, spawnPoint[randomIndex].position, spawnPoint[randomIndex].rotation);
            newEnnemi.GetComponent<EnemiControler>().eLife.combatGestion = this;
            camManager.targetGroupCombat.AddMember(newEnnemi.transform, 1, 1);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    public void NextWave()
    {
        currentWaveIndex++;

        if (waves[currentWaveIndex].nbEnemiCac > 0)
        {
            StartCoroutine(SpawnEnnemi(waves[currentWaveIndex].spawnPointCac, waves[currentWaveIndex].prefabEnemiCac, waves[currentWaveIndex].nbEnemiCac, waves[currentWaveIndex-1].timeDelayBeforeNextWave));

        }
        if (waves[currentWaveIndex].nbEnemiRange > 0)
        {
            StartCoroutine(SpawnEnnemi(waves[currentWaveIndex].spawnPointRange, waves[currentWaveIndex].prefabEnemiRange, waves[currentWaveIndex].nbEnemiRange, waves[currentWaveIndex - 1].timeDelayBeforeNextWave));

        }

        nbEnemi += waves[currentWaveIndex].nbEnemiCac + waves[currentWaveIndex].nbEnemiRange;
        nbEnemiKilled = 0;


    }

    public bool IfWaveClear()
    {
        bool nbKill = nbEnemiKilled >= waves[currentWaveIndex].nbEnemiToKillToNextWave;
        return nbKill;
    }


    public void AnEnemiWasKill(Transform eTransform)
    {
        nbEnemi --;
        nbEnemiKilled ++;
        camManager.targetGroupCombat.RemoveMember(eTransform);
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
