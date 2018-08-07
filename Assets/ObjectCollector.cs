using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    LifePoints lifePoints;

    void Start()
    {
        lifePoints = FindObjectOfType<LifePoints>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            lifePoints.RemoveLife();
            collision.gameObject.GetComponent<Figure>().DestroyFigureNumber();
        }
        Destroy(collision.gameObject);
    }
}
