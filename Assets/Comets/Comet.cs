using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    [SerializeField] float flightSpeedMax;
    [SerializeField] float flightSpeed;
    [SerializeField] GameObject explosion;
    [SerializeField] float loopingTimer = 0.5f;

    bool working = true;
    bool itPassed = false;

    Rigidbody2D rb;
    Quaternion startRotation;
    float lastTimeLooped = 0f;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (working)
        {
            print(transform.up + "   " + Time.deltaTime + "   " + flightSpeed + "    " + transform.up * Time.deltaTime * flightSpeed);
            rb.AddForce(transform.up * Time.deltaTime * flightSpeed);
            transform.rotation = startRotation;
        }
    }

    public void RocketHit()
    {
        Instantiate(explosion, transform.position, Quaternion.identity, gameObject.transform.parent);
        Destroy(GetComponent<CometIndicator>().GetIndicator());
        Destroy(gameObject);
    }

    public bool ShouldItLoop()
    {
        if (Time.time - lastTimeLooped >= loopingTimer)
        {
            lastTimeLooped = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DidItPass()
    {
        if (itPassed)
        {
            return false;
        }
        else
        {
            itPassed = true;
            return true;
        }
    }

    public void GiveStartingRotation(Quaternion startingRot)
    {
        startRotation = startingRot;
    }
}
