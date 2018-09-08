using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Vector2 multipliyier;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            Destroy(other.gameObject);
        }
        else
        {
            print("happened");
            var pos = other.gameObject.transform.position;
            pos = pos * multipliyier;
            other.gameObject.transform.position = pos;
        }
    }

}
