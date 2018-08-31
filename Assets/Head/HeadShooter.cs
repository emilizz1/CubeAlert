using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShooter : MonoBehaviour
{
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float explosionRadius = 5f;

    Ammo ammo;

    void Start()
    {
        ammo = FindObjectOfType<Ammo>();
    }

    public void Explode()
    {
        if (ammo.IsThereAmmo())
        {
            foreach (Figure figure in GetFiguresInRange())
            {
                Rigidbody2D rb = figure.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce((rb.transform.position - transform.position) * explosionForce, ForceMode2D.Impulse);
                    figure.RemoveAmmo();
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
}
