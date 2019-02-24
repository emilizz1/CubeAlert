using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawnerPuzzle : MonoBehaviour
{
    [SerializeField] int lifeAmount = 7, starsToSpawn;
    [SerializeField] GameObject[] stars;
    [SerializeField] Material[] materials;

    bool playing = true;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (starsToSpawn > 0)
        {
            if (FindObjectsOfType<Star>().Length == 0)
            {
                GameObject myObject = Instantiate(stars[Random.Range(0, stars.Length)]) as GameObject;
                myObject.GetComponentInChildren<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
                myObject.GetComponentInChildren<DreamStarGen.DreamStarGenerator>()._GenerateStar();
                myObject.transform.parent = transform;
                myObject.transform.position = transform.position;
                myObject.GetComponent<Star>().GiveBulletsAmount(lifeAmount);
                starsToSpawn--;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    public int GetLifeLeftToSpawn()
    {
        return starsToSpawn * lifeAmount;
    }
}
