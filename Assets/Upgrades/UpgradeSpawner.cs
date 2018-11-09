using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    [SerializeField] GameObject upgrade;
    [SerializeField] Sprite[] timeUpgradeSprites;
    [SerializeField] Sprite[] damageUpgradeSprites;
    [SerializeField] Sprite[] tapUpgradeSprites;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float spawnX = 26f;
    [SerializeField] float spawnY = 40f;
    [SerializeField] float force;
    [SerializeField] int bonusAmount = 2;

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
            var myUpgrade = Instantiate(upgrade, SpawnPos(Random.Range(0, 2)), Quaternion.identity, transform);
            AddSpriteAndType(myUpgrade);
            myUpgrade.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, startingAlpha);
            myUpgrade.GetComponent<Rigidbody2D>().AddForce((Vector3.zero - myUpgrade.transform.position) * force, ForceMode2D.Impulse);
        }
    }

    Vector3 SpawnPos(int num)
    {
        if (num == 0)
        {
            return SpawnPointA(Random.Range(0, 2));
        }
        else
        {
            return SpawnPointB(Random.Range(0, 2));
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
    // TODO renew
    //void AddSpriteAndType(GameObject upgrade)
    //{
    //    switch (Random.Range(0, 3))
    //    {
    //        case (0):
    //            upgrade.GetComponent<SpriteRenderer>().sprite = timeUpgradeSprites[Random.Range(0, timeUpgradeSprites.Length)];
    //            upgrade.GetComponent<UpgradeController>().AssignBonuses(bonusAmount, 0, 0);
    //            break;
    //        case (1):
    //            upgrade.GetComponent<SpriteRenderer>().sprite = damageUpgradeSprites[Random.Range(0, damageUpgradeSprites.Length)];
    //            upgrade.GetComponent<UpgradeController>().AssignBonuses(0, bonusAmount, 0);
    //            break;
    //        case (2):
    //            upgrade.GetComponent<SpriteRenderer>().sprite = tapUpgradeSprites[Random.Range(0, tapUpgradeSprites.Length)];
    //            upgrade.GetComponent<UpgradeController>().AssignBonuses(0, 0, bonusAmount - 1);
    //            break;
    //    }
    //}
}
