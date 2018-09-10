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
        else if(other.gameObject.GetComponent<Figure>())
        {
            
            if (other.gameObject.GetComponent<Figure>().ShouldItLoop())
            {
                var pos = other.gameObject.transform.position;
                pos = pos * multipliyier;
                other.gameObject.transform.position = pos;
            }
        }
        else
        {
            if (other.gameObject.GetComponent<Comet>().ShouldItLoop() || other.gameObject.GetComponent<Comet>().DidItPass())
            {
                var pos = other.gameObject.transform.position;
                pos = pos * multipliyier;
                other.gameObject.transform.position = pos;

            }
        }
    }
}
