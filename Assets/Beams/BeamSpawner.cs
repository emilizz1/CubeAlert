using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : MonoBehaviour
{
    [SerializeField] int objectsToSpawn = 4;
    [SerializeField] GameObject simpleBeam;
    [SerializeField] GameObject crossBeam;

    List< Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        while(usedPositions.Count < objectsToSpawn)
        {
            SpawnBeam(Random.Range(0, 3));
        }
    }

    void SpawnBeam(int beamToSpawn)
    {
        Vector3 spawnLocation = GetSpawnLocation();
        if (IsSpawnLocationAllowed(spawnLocation))
        {
            usedPositions.Add(spawnLocation);
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

    bool IsSpawnLocationAllowed(Vector3 spawnLoc)
    {
        foreach(Vector3 pos in usedPositions)
        {
            if((spawnLoc.x == pos.x && spawnLoc.y == pos.y) || (spawnLoc.x + 15.3f == pos.x && spawnLoc.y == pos.y) || (spawnLoc.x - 15.3f == pos.x && spawnLoc.y == pos.y) ||
                (spawnLoc.x == pos.x && spawnLoc.y + 11.5f == pos.y) || (spawnLoc.x == pos.x && spawnLoc.y - 11.5f == pos.y))
            {
                return false;
            }
        }
        return true;
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
