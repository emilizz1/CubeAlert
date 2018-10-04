using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupernovaSpawner : MonoBehaviour
{
    [SerializeField] GameObject supernova;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    bool playing = true;

    Vector3 pos;

    void Start ()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnSupernova()
    {
        while (playing)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minSpawnTime, maxSpawnTime));
            Instantiate(supernova, GetNewTargetPos(), Quaternion.identity, transform);
        }
    }

    Vector3 GetNewTargetPos()
    {
        float minX = pos.x * 0.3f;
        float maxX = pos.x * -0.7f;
        float minY = pos.y * 0.3f;
        float maxY = pos.y * -0.7f;
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
