using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPush : MonoBehaviour
{
    [SerializeField] float explosionForce = 100f;
    [SerializeField] float explosionRadius = 50f;
    [SerializeField] ParticleSystem dustParticle;

    public void Explode()
    {
        dustParticle.Play();
        foreach (Star figure in GetFiguresInRange())
        {
            Rigidbody2D rb = figure.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce((rb.transform.position - transform.position) * explosionForce, ForceMode2D.Impulse);
                figure.RemoveAmmo();
            }
        }
        foreach (Comet rocket in GetRocketsInRange())
        {
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rocket.RocketHit();
            }
        }
    }

    List<Star> GetFiguresInRange()
    {
        List<Star> figures = new List<Star>();
        foreach (Star figure in FindObjectsOfType<Star>())
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
