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
        LookAtPortal();
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

    void LookAtPortal()
    {
        Vector3 targ = portal.transform.position;
        Vector3 myPos = transform.position;
        targ.z = 0f;
        targ.x = targ.x - myPos.x;
        targ.y = targ.y - myPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        startRotation = transform.rotation;
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
}
