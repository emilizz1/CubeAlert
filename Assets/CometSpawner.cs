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
        StartCoroutine(SpawnRocket());
    }

	IEnumerator SpawnRocket()
    {
        while (playing)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject rocketToSpawn = comets[Random.Range(0, comets.Length)];
            GameObject myObject = Instantiate(rocketToSpawn, GetRocketSpawnPos(), Quaternion.identity, transform);
            CheckIfItsInPortalsBlindSpot(myObject);
        }
    }

    Vector2 GetRocketSpawnPos()
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
}
