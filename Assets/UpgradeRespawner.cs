using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRespawner : MonoBehaviour
{
    [SerializeField] GameObject[] upgrades;

    public void SpawnUpgrade()
    {
        Instantiate(upgrades[Random.Range(0, upgrades.Length)], transform.position, Quaternion.identity, transform);
    }
}
