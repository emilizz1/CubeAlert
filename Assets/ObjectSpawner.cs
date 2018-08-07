using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] objectToThrow;
    [SerializeField] Vector2 throwForce = new Vector2(0f,-3f);
    [SerializeField] float minSpawnTime = 0.8f;
    [SerializeField] float maxSpawnTime = 1.2f;
    
    bool playing = true;
    float posx;

	void Start ()
    {
        StartCoroutine(SpawnObjects());
        var pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        posx = pos.x +1.5f;
    }

    IEnumerator SpawnObjects()
    {
        while (playing)
        {
            GameObject objectToCreate = objectToThrow[Random.Range(0, objectToThrow.Length)];
            GameObject myObject = Instantiate(objectToCreate) as GameObject;
            myObject.transform.parent = transform;
            myObject.transform.position = new Vector2(Random.Range(posx, posx  * -1), 40f);
            myObject.GetComponent<Rigidbody2D>().AddForce(throwForce, ForceMode2D.Impulse);
            yield return new WaitForSecondsRealtime(Random.Range(maxSpawnTime, maxSpawnTime));
        }
    }
}
