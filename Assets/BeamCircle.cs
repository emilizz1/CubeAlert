using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCircle : MonoBehaviour
{
    bool shrinking = true;

    float shrinkingSpeed;
    float rotationSpeed;
    Transform particles;

    void Start()
    {
        particles = transform.GetChild(0);
        particles.localPosition = new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 0f); 
        shrinkingSpeed = Random.Range(0.0045f, 0.006f);
        rotationSpeed = Random.Range(-30f, 30f);
    }
    
    void Update()
    {
        if (shrinking)
        {
            transform.localScale = new Vector3(transform.localScale.x + shrinkingSpeed, transform.localScale.y + shrinkingSpeed, transform.localScale.z + shrinkingSpeed);
            if(transform.localScale.x >= 4.5f || transform.localScale.x <= 1.5f)
            {
                shrinkingSpeed = shrinkingSpeed * -1;
            }
        }
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
