using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapExplosion : MonoBehaviour
{
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] GameObject[] explosions;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 60f));
            transform.position = touchPos;
            Explode();
        }
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosions[Random.Range(0, explosions.Length)], transform.position, Quaternion.identity, transform);
        Destroy(explosion);
        FindObjectOfType<TapNumber>().RemoveATap();
        foreach (Star figure in GetFiguresInRange())
        {
            Rigidbody2D rb = figure.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce((rb.transform.position - transform.position) * explosionForce, ForceMode2D.Impulse);
            }
        }
        foreach (Comet rocket in GetRocketsInRange())
        {
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rocket.CometHit();
            }
        }
    }

    List<Star> GetFiguresInRange()
    {
        List<Star> figures = new List<Star>();
        foreach(Star figure in FindObjectsOfType<Star>())
        {
            if (Vector2.Distance(figure.transform.position, transform.position) <= explosionRadius)
            {
                figures.Add(figure);
            }            
        }
        return figures;
    }

    List<Comet> GetRocketsInRange()
    {
        List<Comet> rockets = new List<Comet>();
        foreach (Comet rocket in FindObjectsOfType<Comet>())
        {
            if (Vector2.Distance(rocket.transform.position, transform.position) <= explosionRadius)
            {
                rockets.Add(rocket);
            }
        }
        return rockets;
    }
}
