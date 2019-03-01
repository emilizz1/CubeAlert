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
        SpawnStar();
    }

    public void SpawnStar()
    {
        print("Spawning");
        if (starsToSpawn != 0)
        {
            GameObject myObject = Instantiate(stars[Random.Range(0, stars.Length)]) as GameObject;
            myObject.GetComponentInChildren<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
            myObject.GetComponentInChildren<DreamStarGen.DreamStarGenerator>()._GenerateStar();
            myObject.transform.parent = transform;
            myObject.transform.position = transform.position;
            myObject.GetComponent<Star>().GiveBulletsAmount(lifeAmount);
            starsToSpawn--;
        }
    }

    public int GetLifeLeftToSpawn()
    {
        return starsToSpawn * lifeAmount;
    }
}
