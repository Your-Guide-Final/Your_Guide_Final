using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemi : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemi;
    private List<GameObject> enemiList;
    [SerializeField] private Transform[] spawnpoint;
    // Start is called before the first frame update
    private void Awake()
    {
        enemiList = new List<GameObject>();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in enemiList)
        {
            if (item == null)
            {
                enemiList.Remove(item);
            }
        }
        if (enemiList.Count == 0)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 pointToSpawn=transform.position;
        if (spawnpoint != null)
        {
            int random = Random.Range(0, spawnpoint.Length);
            pointToSpawn = spawnpoint[random].position;
        }
        GameObject newEnemi = Instantiate(prefabEnemi, pointToSpawn, transform.rotation, null);
        enemiList.Add(newEnemi);
    }
}
