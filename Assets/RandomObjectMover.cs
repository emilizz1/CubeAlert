using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectMover : MonoBehaviour
{
    [SerializeField] float moveSpeed, maxSpeed, minChangeDirectionTime, maxChangeDirectionTime;

    Rigidbody2D myRigidbody;
    float minX, maxX, minY, maxY, waitingFrom, timeToWait;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        minX = transform.position.x - 7.5f;
        maxX = transform.position.x + 7.5f;
        minY = transform.position.y - 5;
        maxY = transform.position.y + 5;
        GetNewTargetPos();
    }
    
    void Update()
    {
        if (myRigidbody.velocity.magnitude > maxSpeed)
        {
            myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxSpeed);
        }
        if(Time.time > waitingFrom + timeToWait || checkIfOutOfBounds())
        {
            print(Time.time + " to " + waitingFrom + timeToWait + "   " + timeToWait + "   " + checkIfOutOfBounds());
            GetNewTargetPos();
        }
    }

    void GetNewTargetPos()
    {
        var targetPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        myRigidbody.AddForce((targetPos - transform.position) * moveSpeed, ForceMode2D.Impulse);
        waitingFrom = Time.time;
        timeToWait = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
        LookAtTargetPos(targetPos);
    }

    void LookAtTargetPos(Vector3 targetPos)
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(targetPos) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    bool checkIfOutOfBounds()
    {
        if (transform.position.x > maxX ||
            transform.position.x < minX ||
            transform.position.y > maxY ||
            transform.position.y < minY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
