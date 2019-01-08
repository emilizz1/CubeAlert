using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawnerPuzzle : MonoBehaviour
{
    [SerializeField] GameObject comet;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float rotation;

    bool playing = true;

    Vector2 pos;

    void Start()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        pos = new Vector2(pos.x - 4f, pos.y - 4f);
        StartCoroutine(SpawnComet());
    }

    IEnumerator SpawnComet()
    {
        while (playing)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject myObject = Instantiate(comet, transform.position, Quaternion.identity, transform);
            myObject.GetComponent<Comet>().GiveStartingRotation(Quaternion.Euler(0f,0f,rotation));
            myObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
