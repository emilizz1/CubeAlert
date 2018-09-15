using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] Portal portalPrefab;
    [SerializeField] Vector2[] spawnPositions;

    void Start()
    {
        Invoke("SpawnPortal", 10f);
    }

    void SpawnPortal()
    {
        Instantiate(portalPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity, transform);
    }
}
