using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamStarGen.Algorithms;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject objectToThrow;
    [SerializeField] Material[] materials;
    [SerializeField] [Range(-100, 100)] float minThrowForce_x =0f;
    [SerializeField] [Range(-100, 100)] float maxThrowForce_x =0f;
    [SerializeField] [Range(-100, 100)] float minThrowForce_y=0f;
    [SerializeField] [Range(-100, 100)] float maxThrowForce_y=0f; 
    [SerializeField] float minSpawnTime = 0.8f;
    [SerializeField] float maxSpawnTime = 1.6f;
    [SerializeField] float minStartingPosX;
    [SerializeField] float maxStartingPosX;
    [SerializeField] float minStartingPosY;
    [SerializeField] float maxStartingPosY;
    [SerializeField] int minBulletAmount = 3;
    [SerializeField] int maxBulletAmount = 12;

    bool playing = true;
    Ammo ammo;

	void Start ()
    {
        
        var pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        ammo = FindObjectOfType<Ammo>();
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (playing)
        {
            int bulletAmount = Random.Range(minBulletAmount, maxBulletAmount);
            if (ammo.IsThereLevelAmmo(bulletAmount))
            {
                GameObject myObject = Instantiate(objectToThrow) as GameObject;
                var shape = GenerateStar(myObject.GetComponent<DreamStarGen.DreamStarGenerator>());
                myObject.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
                shape._GenerateStar();
                myObject.GetComponent<CircleCollider2D>().radius = shape.Radius;
                myObject.transform.parent = transform;
                myObject.transform.position = new Vector2(Random.Range(minStartingPosX, maxStartingPosX), Random.Range(minStartingPosY, maxStartingPosY));
                myObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(minThrowForce_x, maxThrowForce_x), Random.Range(minThrowForce_y, maxThrowForce_y)), ForceMode2D.Impulse);
                GameObject figureNumber = FindObjectOfType<FigureNumbers>().GetFigureNumber();
                myObject.GetComponent<Figure>().GiveBulletsAmount(bulletAmount);
            }
            yield return new WaitForSecondsRealtime(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    DreamStarGen.DreamStarGenerator GenerateStar(DreamStarGen.DreamStarGenerator shape)
    {
        int randomaizer = Random.Range(0, 4);
        switch (randomaizer) {
            case 0:
                shape.Radius = Random.Range(2f, 3f);
                shape.Width = Random.Range(0.2f, 4);
                shape.Density = 4f;
                shape.a = Random.Range(1f, 44f);
                break;
            case 1:
                shape.Radius = Random.Range(2f, 3f);
                shape.Width = Random.Range(0.2f, 4);
                shape.Density = 8f;
                shape.a = Random.Range(1f, 21f);
                break;
            case 2:
                shape.Radius = Random.Range(2f, 3f);
                shape.Width = Random.Range(0.2f, 4);
                shape.Density = 12f;
                shape.a = Random.Range(1f, 14f);
                break;
            case 3:
                shape.Radius = Random.Range(2f, 3f);
                shape.Width = Random.Range(0.2f, 4);
                shape.Density = 16f;
                shape.a = Random.Range(1f, 10.5f);
                break;
            case 4:
                shape.Radius = Random.Range(2f, 3f);
                shape.Width = Random.Range(0.2f, 4);
                shape.Density = 20f;
                shape.a = Random.Range(1f, 8.5f);
                break;
        }
        shape.b = 0f;
        shape.c = 0f;
        shape.d = 0f;
        shape.e = 0f;
        return shape;
    }
}
