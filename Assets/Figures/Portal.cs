using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class Portal : MonoBehaviour
{
    float minX = -12.5f;
    float maxX = 12.5f;
    float currentlyMoving;
    Ammo ammo;
    DreamStarGen.DreamStarGenerator shape;
    bool shouldAbsorbAllBullets = false;
    Vector3 pos;
    CircleCollider2D circleCollider;

    void Start()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        shape = GetComponent<DreamStarGen.DreamStarGenerator>();
        ammo = FindObjectOfType<Ammo>();
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = 3.2f;
        SpawnAtRandomPosition();
    }

    void Update()
    {
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

    public void SpawnAtRandomPosition()
    {
        float minX = pos.x * 0.3f;
        float maxX = pos.x * 0.7f;
        float minY = pos.y * 0.3f;
        float maxY = pos.y * 0.7f;
        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
