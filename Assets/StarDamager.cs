using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDamager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            collision.gameObject.GetComponent<Star>().RemoveStarLife(1);
        }
    }
}
