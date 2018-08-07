using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    float minX = -12.5f;
    float maxX = 12.5f;
    float currentlyMoving;
    Ammo ammo;

    void Start()
    {
        currentlyMoving = minX;
        ammo = FindObjectOfType<Ammo>();
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            var figure = collision.gameObject.GetComponent<Figure>();
            ammo.AddAmmo(figure.GetBulletAmount());
            figure.DestroyFigure();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    
}
