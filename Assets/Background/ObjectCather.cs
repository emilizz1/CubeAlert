using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCather : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Comet>())
        {
            collision.GetComponent<Comet>().CometHit();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
