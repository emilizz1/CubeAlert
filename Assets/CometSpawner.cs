using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] comets;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float minDistanceToPortal;

    Vector2 pos;
    Portal portal;
    bool playing = true;
    
	void Start ()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        pos = new Vector2(pos.x - 3, pos.y - 3);
        portal = FindObjectOfType<Portal>();
        StartCoroutine(SpawnComet());
    }

	IEnumerator SpawnComet()
    {
        while (playing)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject cometToSpawn = comets[Random.Range(0, comets.Length)];
            GameObject myObject = Instantiate(cometToSpawn, GetCometSpawnPos(), Quaternion.identity, transform);
            LookAtPortal(myObject);
            CheckIfItsInPortalsBlindSpot(myObject);
        }
    }

    Vector2 GetCometSpawnPos()
    {
        Vector2 rocketPos = new Vector2(Random.Range(-10f,10f), Random.Range(-10f, 10f));
        float trueDistanceToCenter = Vector2.Distance(pos, new Vector2(0f,0f));
        float currentDistanceToCenter = Vector2.Distance(rocketPos, new Vector2(0f, 0f));
        while (currentDistanceToCenter < trueDistanceToCenter)
        {
            currentDistanceToCenter = Vector2.Distance(rocketPos, new Vector2(0f, 0f));
            rocketPos = rocketPos * 1.1f;
        }
        return rocketPos;
    }

    void CheckIfItsInPortalsBlindSpot(GameObject rocket)
    {
        if(Vector2.Distance(rocket.transform.position, portal.transform.position) < minDistanceToPortal)
        {
            Destroy(rocket);
        }
    }

    void LookAtPortal(GameObject myObject)
    {
        Vector3 targ = portal.transform.position;
        Vector3 myPos = myObject.transform.position;
        targ.z = 0f;
        targ.x = targ.x - myPos.x;
        targ.y = targ.y - myPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        myObject.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
