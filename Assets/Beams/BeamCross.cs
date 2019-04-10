using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCross : MonoBehaviour
{
    float rotationSpeed;

    private void Start()
    {
        SetRandomRotation();
    }

    void SetRandomRotation()
    {
        if(Mathf.Abs(rotationSpeed) < 20f)
        {
            SetRandomRotation();
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
