using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDamager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            StartCoroutine(collision.gameObject.GetComponent<Star>().RemoveStarLife(1, true));
        }
    }
}
