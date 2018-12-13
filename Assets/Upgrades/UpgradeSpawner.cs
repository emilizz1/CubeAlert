using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSpawner : MonoBehaviour
{
    [SerializeField] GameObject timeUpgrade;
    [SerializeField] GameObject tapUpgrade;
    [SerializeField] GameObject damageUpgrade;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float spawnX = 26f;
    [SerializeField] float spawnY = 40f;
    [SerializeField] float force;

    float spawningMinValue = .5f;
    float spawningMaxValue = .8f;
    List<GameObject> activeUpgrades = new List<GameObject>();
    bool playing = false;
    float currentSpawningTime = 0;
    float currentlyLowestStat = 0;
    bool spawning = false;

    void Update()
    {
        CheckTimeUpgrade();
        CheckTapUpgrade();
        CheckDamageUpgrade();
        if (playing == false && currentlyLowestStat >= spawningMinValue)
        {
            playing = true;
            StartCoroutine(SpawnUpgrades());
        }
    }

    IEnumerator SpawnUpgrades()
    {
        while (playing)
        {
            yield return new WaitForSeconds(currentSpawningTime);
            if (activeUpgrades.Count > 0)
            {
                var myUpgrade = Instantiate(activeUpgrades[Random.Range(0, activeUpgrades.Count)], SpawnPos(Random.Range(0, 2)), Quaternion.identity, transform);
                myUpgrade.GetComponent<Rigidbody2D>().AddForce((Vector3.zero - myUpgrade.transform.position) * force, ForceMode2D.Impulse);
            }
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

    void CheckTimeUpgrade()
    {
        float timeFillAmount = FindObjectOfType<Timer>().GetComponent<Image>().fillAmount;
        CheckLowestStat(timeFillAmount);
        if (timeFillAmount > spawningMinValue)
        {
            if (!activeUpgrades.Contains(timeUpgrade))
            {
                activeUpgrades.Add(timeUpgrade);
            }
        }
        else
        {
            if (activeUpgrades.Contains(timeUpgrade))
            {
                activeUpgrades.Remove(timeUpgrade);
            }
        }
    }

    void CheckTapUpgrade()
    {
        float tapFillAmount = FindObjectOfType<TapNumber>().GetComponent<Image>().fillAmount;
        CheckLowestStat(tapFillAmount);
        if (tapFillAmount > spawningMinValue)
        {
            if (!activeUpgrades.Contains(tapUpgrade))
            {
                activeUpgrades.Add(tapUpgrade);
            }
        }
        else
        {
            if (activeUpgrades.Contains(tapUpgrade))
            {
                activeUpgrades.Remove(tapUpgrade);
            }
        }
    }

    void CheckDamageUpgrade()
    {
        float damageFillAmount = FindObjectOfType<Ammo>().GetComponent<Image>().fillAmount;
        CheckLowestStat(damageFillAmount);
        if (damageFillAmount > spawningMinValue)
        {
            if (!activeUpgrades.Contains(damageUpgrade))
            {
                activeUpgrades.Add(damageUpgrade);
            }
        }
        else
        {
            if (activeUpgrades.Contains(damageUpgrade))
            {
                activeUpgrades.Remove(damageUpgrade);
            }
        }
    }

    void CheckLowestStat(float stat)
    {
        if(currentlyLowestStat < stat)
        {
            float spawningProc = ((stat - spawningMinValue) * 100) / (spawningMaxValue - spawningMinValue);
            float spawnRateValue = ((maxSpawnTime - minSpawnTime) * spawningProc) / 100;
            currentSpawningTime = spawnRateValue + minSpawnTime;
            currentlyLowestStat = stat;
        }
    }
}
