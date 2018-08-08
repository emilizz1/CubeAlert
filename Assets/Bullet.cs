using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.8f);
            if(Vector2.Distance(transform.position, target.transform.position) < 2.5f)
            {
                GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position) * 0.8f, ForceMode2D.Impulse);
                target = null;
            }
        }
    }

    public void GetTarget(Transform trg)
    {
        target = trg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            collision.gameObject.GetComponent<Figure>().RemoveAmmo();
        }
    }
}
