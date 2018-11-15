using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    [SerializeField] float flightSpeedMax;
    [SerializeField] float flightSpeed;
    [SerializeField] GameObject explosionOnHit;
    [SerializeField] float loopingTimer = 0.5f;
    [SerializeField] int healingToBlackhole = 1; 

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
            rb.AddForce(transform.up * Time.deltaTime * flightSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -flightSpeedMax, flightSpeedMax), Mathf.Clamp(rb.velocity.y, -flightSpeedMax, flightSpeedMax));
            transform.rotation = startRotation;
        }
    }

    public void CometHit()
    {
        Instantiate(explosionOnHit, transform.position, Quaternion.identity, gameObject.transform.parent);
        //Destroy(GetComponent<CometIndicator>().GetIndicator());
        Destroy(gameObject);
    }

    public bool ShouldItLoop()
    {
        StartCoroutine(ResetTrails());
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

    IEnumerator ResetTrails()
    {
        var trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
        yield return new WaitForSecondsRealtime(0.15f);
        trail.enabled = true;
    }

    public int GetHealing()
    {
        return healingToBlackhole;
    }
}
