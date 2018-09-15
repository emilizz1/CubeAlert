using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] Portal portalPrefab;
    [SerializeField] Vector2[] spawnPositions;
    [SerializeField] int numberOfPortalsToSpawn = 2;
    [SerializeField] float timeBetweenSpawns = 10f;

    bool spawningFinished = false;

    void Start()
    {
        StartCoroutine(SpawnPortal());
    }

    IEnumerator SpawnPortal()
    {
        while(numberOfPortalsToSpawn > 0)
        {
            yield return new WaitForSecondsRealtime(timeBetweenSpawns);
            Instantiate(portalPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity, transform);
            numberOfPortalsToSpawn--;
        }
        spawningFinished = true;
    }

    public bool GetSpawningFinished()
    {
        return spawningFinished;
    }
}
