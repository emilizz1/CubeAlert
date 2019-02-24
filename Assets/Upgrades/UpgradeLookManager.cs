using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLookManager : MonoBehaviour
{
    [SerializeField] Sprite[] mask;
    [SerializeField] float rotationSpeed = 55f;
    [SerializeField] float growthSpeed = 0.65f;
    
    bool growing = false;
    
    float rotation;

    void Start ()
    {
        rotation = Random.Range(-rotationSpeed, rotationSpeed);
        GetComponent<SpriteRenderer>().sprite = mask[Random.Range(0, mask.Length)];
        gameObject.AddComponent<BoxCollider2D>();
    }
	
	void Update ()
    {
        Rotate();
        Grow();
    }

    void Grow()
    {
        float growthAmount = Time.deltaTime * growthSpeed;
        if (growing)
        {
            transform.localScale += new Vector3(growthAmount, growthAmount, growthAmount);
        }
        else
        {
            transform.localScale -= new Vector3(growthAmount, growthAmount, growthAmount);
        }
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * rotation));
    }
}
