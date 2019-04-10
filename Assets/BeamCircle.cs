using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCircle : MonoBehaviour
{
    bool shrinking = true;

    float shrinkingSpeed;

    void Start()
    {
        shrinkingSpeed = Random.Range(0.003f, 0.004f);
    }
    
    void Update()
    {
        if (shrinking)
        {
            transform.localScale = new Vector3(transform.localScale.x + shrinkingSpeed, transform.localScale.y + shrinkingSpeed, transform.localScale.z + shrinkingSpeed);
            if(transform.localScale.x >= 2.5f || transform.localScale.x <= 1f)
            {
                shrinkingSpeed = shrinkingSpeed * -1;
            }
        }
    }
}
