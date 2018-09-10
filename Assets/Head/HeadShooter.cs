using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShooter : MonoBehaviour
{
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float explosionRadius = 5f;

    Ammo ammo;
    ParticleSystem ps;

    void Start()
    {
        ammo = FindObjectOfType<Ammo>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void Explode()
    {
        if (ammo.IsThereAmmo())
        {
            ps.Play();
            foreach (Figure figure in GetFiguresInRange())
            {
                Rigidbody2D rb = figure.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce((rb.transform.position - transform.position) * explosionForce, ForceMode2D.Impulse);
                    figure.RemoveAmmo();
                }
            }
            foreach(Comet rocket in GetRocketsInRange())
            {
                Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rocket.RocketHit();
                }
            }
        }
    }

    List<Figure> GetFiguresInRange()
    {
        List<Figure> figures = new List<Figure>();
        foreach(Figure figure in FindObjectsOfType<Figure>())
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
