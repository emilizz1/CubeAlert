using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comet : MonoBehaviour
{
    [SerializeField] float flightSpeedMax;
    [SerializeField] float flightSpeed;
    [SerializeField] GameObject explosionOnHit;
    [SerializeField] float loopingTimer = 0.5f;
    [SerializeField] int damage = 1;

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
        rb.AddForce(transform.up * Time.deltaTime * flightSpeed, ForceMode2D.Impulse);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -flightSpeedMax, flightSpeedMax), Mathf.Clamp(rb.velocity.y, -flightSpeedMax, flightSpeedMax));
        transform.rotation = startRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<UpgradeController>()) // destroys itself when colliding with everything except Upgrade
        {
            if (collision.gameObject.GetComponent<BlackHole>())
            {
                DisplayDamageNumber(true, collision);
            }
            else if (collision.gameObject.GetComponent<Star>())
            {
                DisplayDamageNumber(false, collision);
            }
            DestroyComet();
        }
    }

    public void DestroyComet()
    {
        Instantiate(explosionOnHit, transform.position, Quaternion.identity, gameObject.transform.parent);
        Destroy(gameObject); 
    }

    void DisplayDamageNumber(bool fullDamage, Collision2D collision)
    {
        BlackholeDamageNumber damageNumber = FindObjectOfType<BlackholeDamageNumber>();
        GameObject numberInstance = Instantiate(damageNumber.GetNumber(), collision.GetContact(0).point, Quaternion.identity, damageNumber.transform);
        if (fullDamage)
        {
            numberInstance.GetComponent<Text>().text = "+" + damage.ToString();
        }
        else
        {
            numberInstance.GetComponent<Text>().text = "-" + (damage/2).ToString();
        }
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

    public int GetDamageDone()
    {
        return damage;
    }
}
