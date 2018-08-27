using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class Portal : MonoBehaviour
{
    [SerializeField] Material[] materials;

    float minX = -12.5f;
    float maxX = 12.5f;
    float currentlyMoving;
    Ammo ammo;
    DreamStarGen.DreamStarGenerator shape;
    bool shouldAbsorbAllBullets = false;

    void Start()
    {
        shape = GetComponent<DreamStarGen.DreamStarGenerator>();
        GenerateAStar();
        currentlyMoving = minX;
        ammo = FindObjectOfType<Ammo>();
        gameObject.AddComponent<CircleCollider2D>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentlyMoving, transform.position.y, transform.position.z), .015f);
        if (transform.position.x == currentlyMoving)
        {
            if (currentlyMoving == minX)
            {
                currentlyMoving = maxX;
            }
            else
            {
                currentlyMoving = minX;
            }
        }
        UpdateSizeFromAmmo();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            var figure = collision.gameObject.GetComponent<Figure>();
            StartCoroutine(AbsorbingFigure(figure));
            ammo.AddAmmo(figure.GetBulletAmount());
            figure.DestroyFigure(false);
            shape.Width += 0.01f;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AbsorbingFigure(Figure figure)
    {
        int absorbedBullets = figure.GetBulletAmount();
        while (figure)
        {
            figure.transform.localPosition = Vector2.MoveTowards(figure.transform.localPosition, transform.position, 0.5f);
            if(absorbedBullets > 0)
            {
                ammo.AddAmmo(1);
                absorbedBullets--;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void UpdateSizeFromAmmo()
    {
        float ammoPer = ammo.GetAmmoPercantage();
        float currentSize = 1f + (ammoPer * 0.4f);
        transform.localScale = new Vector2(currentSize, currentSize);
    }

    void GenerateAStar()
    {
        int randomaizer = Random.Range(0, 2);
        switch (randomaizer)
        {
            case 0:
                var circle_0 = gameObject.AddComponent<Circle>();
                circle_0.Impact = 1f;
                circle_0.Angle_MP = Random.Range(1f, 3.5f);
                var curved_0 = gameObject.AddComponent<Curved>();
                curved_0.Impact = 1f;
                curved_0.Angle_MP = Random.Range(1.2f, 17.5f);
                shape.Radius = 4;
                shape.Width = Random.Range(0.1f, 0.4f);
                shape.Density = Random.Range(0.1f, 1.3f);
                shape.a = Random.Range(1f, 20f);
                shape.b = Random.Range(0f, 20f); ;
                shape.c = 0f;
                shape.d = 0f;
                shape.e = 0f;
                break;
            case 1:
                var circle_10 = gameObject.AddComponent<Circle>();
                circle_10.Impact = 1f;
                circle_10.Angle_MP = Random.Range(1f, 11f);
                var circle_11 = gameObject.AddComponent<Curved>();
                circle_11.Impact = 1f;
                circle_11.Angle_MP = Random.Range(10f, 40f);
                shape.Radius = 4;
                shape.Width = Random.Range(0.1f, 0.4f);
                shape.Density = Random.Range(0.1f, 10f);
                shape.a = 1f;
                shape.b = 0f;
                shape.c = 0f;
                shape.d = 0f;
                shape.e = 0f;
                break;
            case 2:
                var circle_20 = gameObject.AddComponent<Circle>();
                circle_20.Impact = 1f;
                circle_20.Angle_MP = 1f;
                var circle_21 = gameObject.AddComponent<Curved>();
                circle_21.Impact = 0.5f;
                circle_21.Angle_MP = 5f;
                var circle_22 = gameObject.AddComponent<Curved>();
                circle_22.Impact = 0.25f;
                circle_22.Angle_MP = Random.Range(0f, 13f);
                var circle_23 = gameObject.AddComponent<Curved>();
                circle_23.Impact = 0.1f;
                circle_23.Angle_MP = Random.Range(100f, 158f);
                shape.Radius = 4;
                shape.Width = Random.Range(0.1f, 0.4f);
                shape.Density = 0.1f;
                shape.a = 1f;
                shape.b = 0f;
                shape.c = 0f;
                shape.d = 0f;
                shape.e = 0f;
                break;
        }
        GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
        shape._GenerateStar();
    }
}
