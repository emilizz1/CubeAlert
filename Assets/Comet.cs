using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    [SerializeField] float flightSpeedMin;
    [SerializeField] float flightSpeedMax;
    [SerializeField] float maxVelocity;
    [SerializeField] GameObject explosion;
    [SerializeField] float loopingTimer = 0.5f;

    Portal portal;
    Rigidbody2D rb;
    ParticleSystem ps;
    Quaternion startRotation;
    float flightSpeed;
    bool itPassed = false;
    float lastTimeLooped = 0f;
    bool working = true;

	void Start ()
    {
        portal = FindObjectOfType<Portal>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        flightSpeed = Random.Range(flightSpeedMin, flightSpeedMax);
    }

    void Update()
    {
        if (working)
        {
            rb.AddForce(transform.up * flightSpeed, ForceMode2D.Force);
            rb.velocity = new Vector2( Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity));
            transform.rotation = startRotation;
        }
    }

    public void RocketHit()
    {
        Instantiate(explosion, transform.position, Quaternion.identity, gameObject.transform.parent);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            collision.gameObject.GetComponent<Figure>().RemoveAmmo();
        }
        startRotation = transform.rotation;
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
