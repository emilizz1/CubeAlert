using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void Start()
    {
        var pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        if(transform.position.x > 0)
        {
            transform.position = new Vector3((pos.x * -1) + 1.5f, transform.position.y);
        }
        else
        {
            transform.position = new Vector3(pos.x - 1.5f, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(collision.gameObject);
        }
    }
}
