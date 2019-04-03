using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLine : MonoBehaviour
{
    [SerializeField] bool rotating = true; 
    
    float rotationSpeed;

    private void Start()
    {
        rotationSpeed = Random.Range(-40, 40f);
    }

    void Update()
    {
        if (rotating)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    public void Rotate(bool shouldItRotate)
    {
        rotating = shouldItRotate;
    }
}
