﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] comets;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    
    bool playing = true;

    Vector2 pos;
    GameObject cometToSpawn;

    void Start ()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        pos = new Vector2(pos.x - 0.5f, pos.y - 0.5f);
        cometToSpawn = comets[Random.Range(0, comets.Length)];
        StartCoroutine(SpawnComet());
    }

	IEnumerator SpawnComet()
    {
        while (playing)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minSpawnTime, maxSpawnTime));
            Vector2 cometSpawnPos = GetCometSpawnPos();
                GameObject myObject = Instantiate(cometToSpawn, cometSpawnPos, Quaternion.identity, transform);
                myObject.GetComponent<Comet>().GiveStartingRotation(LookAtPortal( cometSpawnPos));
        }
    }

    Vector2 GetCometSpawnPos()
    {
        Vector2 cometPos = new Vector2(Random.Range(-10f,10f), Random.Range(-10f, 10f));
        float trueDistanceToCenter = Vector2.Distance(pos, new Vector2(0f,0f));
        float currentDistanceToCenter = Vector2.Distance(cometPos, new Vector2(0f, 0f));
        while (currentDistanceToCenter < trueDistanceToCenter)
        {
            currentDistanceToCenter = Vector2.Distance(cometPos, new Vector2(0f, 0f));
            cometPos = cometPos * 1.1f;
        }
        return cometPos;
    }

    Quaternion LookAtPortal(Vector2 myObject)
    {
        Vector3 targ = FindObjectOfType<BlackHole>().transform.position;
        Vector3 myPos = myObject;
        targ.z = 0f;
        targ.x = targ.x - myPos.x;
        targ.y = targ.y - myPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
