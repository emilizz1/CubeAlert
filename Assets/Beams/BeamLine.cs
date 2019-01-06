using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLine : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 endPos;
    [SerializeField] int starLivesToRemove = 3;
    [SerializeField] ParticleSystem clashWithStar;
    [SerializeField] AudioClip[] supernovaHit;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;
    [SerializeField] bool rotating = true;
    
    float rotationSpeed;
    Vector2 startPos;
    Vector2 currentlyMovingTo;

    private void Start()
    {
        rotationSpeed = Random.Range(-40, 40f);
        startPos = transform.position;
        currentlyMovingTo = endPos;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentlyMovingTo, moveSpeed);
        if (rotating)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (transform.position == new Vector3(endPos.x, endPos.y, 0f))
        {
            currentlyMovingTo = startPos;
        }
        else if(transform.position == new Vector3(startPos.x, startPos.y, 0f))
        {
            currentlyMovingTo = endPos;
        }
    }
}
