using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : MonoBehaviour
{
    [SerializeField] int objectsToSpawn = 4;
    [SerializeField] GameObject simpleBeam;
    [SerializeField] GameObject crossBeam;

    Vector3[] usedPositions = new Vector3[4];
    int usedPositionCount = 0;

    void Start()
    {
        while(usedPositionCount < objectsToSpawn)
        {
            SpawnBeam(Random.Range(0, 4));
        }
    }

    void SpawnBeam(int beamToSpawn)
    {
        bool locationTaken = false;
        Vector3 spawnLocation = GetSpawnLocation();
        foreach (Vector3 vector in usedPositions)
        {
            if (vector.x == spawnLocation.x && vector.y == spawnLocation.y)
            {
                locationTaken = true;
            }
        }
        if (!locationTaken)
        {
            usedPositions[usedPositionCount] = spawnLocation;
            usedPositionCount++;
            switch (beamToSpawn)
            {
                case (0):
                    var spawnedBeam = Instantiate(simpleBeam, spawnLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), transform);
                    spawnedBeam.GetComponent<BeamLine>().Rotate(false);
                    break;
                case (1):
                    var spawnedRotatingBeam = Instantiate(simpleBeam, spawnLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), transform);
                    spawnedRotatingBeam.GetComponent<BeamLine>().Rotate(true);
                    break;
                case (2):
                    Instantiate(crossBeam, spawnLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), transform);
                    break;
            }
        }
    }

    Vector3 GetSpawnLocation()
    {
        Vector3 newLocation = new Vector3();
        newLocation.x = -15.3f + (15.3f * Random.Range(0, 3));
        newLocation.y = -28.75f + (11.5f * Random.Range(0, 6));
        newLocation.z = 0f;
        return newLocation;
    }
}
