using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    [SerializeField] GameObject upgrade;
    [SerializeField] Sprite[] upgradeSprites;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float spawnX = 26f;
    [SerializeField] float spawnY = 40f;
    [SerializeField] float force;

    bool playing = true;

    void Start ()
    {
        StartCoroutine(SpawnUpgrades());		
	}

    IEnumerator SpawnUpgrades()
    {
        while (playing)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            var myUpgrade = Instantiate(upgrade, SpawnPos(Random.Range(0, 1)), Quaternion.identity, transform);
            myUpgrade.GetComponent<SpriteRenderer>().sprite = upgradeSprites[Random.Range(0, upgradeSprites.Length)];
            myUpgrade.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            myUpgrade.GetComponent<Rigidbody2D>().AddForce((Vector3.zero - myUpgrade.transform.position) * force, ForceMode2D.Impulse);
        }
    }

    Vector3 SpawnPos(int num)
    {
        if (num == 0)
        {
            return SpawnPointA(Random.Range(0, 1));
        }
        else
        {
            return SpawnPointB(Random.Range(0, 1));
        }
    }
    
    Vector3 SpawnPointA(int num)
    {
        if(num == 0)
        {
            return new Vector3(-spawnX, Random.Range(-spawnY, spawnY), 0f);
        }
        else
        {
            return new Vector3(spawnX, Random.Range(-spawnY, spawnY), 0f);
        }
    }

    Vector3 SpawnPointB(int num)
    {
        if(num == 0)
        {
            return new Vector3(Random.Range(-spawnX, spawnX), -spawnY, 0f);
        }
        else
        {
            return new Vector3(Random.Range(-spawnX, spawnX), spawnY, 0f);
        }
    }
}
