using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSprite : MonoBehaviour
{
    LifePoints lifePoints;
    HeadShooter headShooter;

    void Start()
    {
        lifePoints = FindObjectOfType<LifePoints>();
        headShooter = FindObjectOfType<HeadShooter>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            lifePoints.RemoveLife();
            collision.gameObject.GetComponent<Figure>().DestroyFigure(true);
        }
    }
}
