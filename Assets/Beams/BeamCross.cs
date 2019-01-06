using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCross : MonoBehaviour
{
    [SerializeField] int starLivesToRemove = 3;
    [SerializeField] ParticleSystem clashWithStar;
    [SerializeField] AudioClip[] supernovaHit;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;

    float rotationSpeed;

    private void Start()
    {
        SetRandomRotation();
    }

    void SetRandomRotation()
    {
        rotationSpeed = Random.Range(-50, 50f);
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
