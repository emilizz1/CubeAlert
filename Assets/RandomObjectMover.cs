using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectMover : MonoBehaviour
{
    [SerializeField] float moveSpeed, roationSpeed, minChangeDirectionTime, maxChangeDirectionTime;

    Rigidbody2D myRigidbody;
    float minX, maxX, minY, maxY, waitingFrom, timeToWait;
    Vector3 targetPos;
    Quaternion targetRot;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        minX = transform.position.x - 7.5f;
        maxX = transform.position.x + 7.5f;
        minY = transform.position.y - 6;
        maxY = transform.position.y + 6;
        GetNewTargetPos();
    }
    
    void Update()
    {
        if(Time.time > waitingFrom + timeToWait)
        {
            GetNewTargetPos();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, roationSpeed);
        }
    }

    void GetNewTargetPos()
    {
        targetPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        LookAtTargetPos();
        waitingFrom = Time.time;
        timeToWait = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
    }

    void LookAtTargetPos()
    {
        Vector3 diff = targetPos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        targetRot = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
