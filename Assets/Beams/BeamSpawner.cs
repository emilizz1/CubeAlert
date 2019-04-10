using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : MonoBehaviour
{
    [SerializeField] int objectsToSpawn = 4;
    [SerializeField] GameObject simpleBeam;
    [SerializeField] GameObject crossBeam;
    [SerializeField] GameObject circleBeam;

    List< Vector3> usedPositions = new List<Vector3>();
    List< Vector3> movedPositions = new List<Vector3>();

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
                    spawnedBeam.GetComponent<BeamLine>().Rotate(GetRandomBool());
                    spawnedBeam.GetComponent<BeamLine>().Shrinking(GetRandomBool());
                    spawnedBeam.GetComponent<BeamLine>().Moving(GetRandomBool(), GetMovedPosition(spawnedBeam.transform.position));
                    break;
                case (1):
                    Instantiate(circleBeam, spawnLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), transform);
                    break;
                case (2):
                    Instantiate(crossBeam, spawnLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), transform);
                    break;
            }
        }
    }

    Vector2 GetMovedPosition(Vector2 movingFrom)
    {
        switch(Random.Range(0, 4))
        {
            case (0):
                if (movingFrom.x + 15.3f < 20f)
                {
                    movingFrom.x += 15.3f;
                    movedPositions.Add(movingFrom);
                }
                break;
            case (1):
                if (movingFrom.x - 15.3f > -20f)
                {
                    movingFrom.x -= 15.3f;
                    movedPositions.Add(movingFrom);
                }
                break;
            case (2):
                if (movingFrom.y - 11.5f > -30f)
                {
                    movingFrom.y -= 11.5f;
                    movedPositions.Add(movingFrom);
                }
                break;
            case (3):
                if (movingFrom.y + 11.5f > 30f)
                {
                    movingFrom.y += 11.5f;
                    movedPositions.Add(movingFrom);
                }
                break;
        }
        return movingFrom;
    }

    bool GetRandomBool()
    {
        if(Random.Range(0,2) == 0)
        {
            return true;
        }
        else
        {
            return false;
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
