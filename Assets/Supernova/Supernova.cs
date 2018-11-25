﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Supernova : MonoBehaviour
{
    [SerializeField] float expandRate;
    [SerializeField] float minRadius;
    [SerializeField] float maxRadius;
    [SerializeField] int starLivesToRemove = 3;
    [SerializeField] float waitBeforeExploding = 5f;
    [SerializeField] ParticleSystem clashWithComet;
    [SerializeField] ParticleSystem clashWithStar;
    [SerializeField] AudioClip[] supernovaHit;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;
    [SerializeField] AudioClip supernovaClip;

    bool playing = true;
    bool fullyExpanded = false;

    ParticleSystem supernovaPS;
    CircleCollider2D collider;
    float myRadius;
     
	void Start ()
    {
        supernovaPS = GetComponentInChildren<ParticleSystem>();
        collider = GetComponent<CircleCollider2D>();
        myRadius = Random.Range(minRadius, maxRadius);
        AudioSource.PlayClipAtPoint(supernovaClip, Camera.main.transform.position, soundVolume);
    }
	
	void Update ()
    {
        if (playing)
        {
            ExpandSupernova();
        }
	}

    void ExpandSupernova()
    {
        if (collider.radius < myRadius)
        {
            collider.radius += expandRate * Time.deltaTime;
        }
        else
        {
            fullyExpanded = true;
            StartCoroutine(Explode());
        }
        var supernovaMain = supernovaPS.main;
        if (supernovaMain.startSize.constantMax <= (collider.radius * 1.5f))
        {
            supernovaMain.startSizeMultiplier += Time.deltaTime * expandRate;
        }
    }

    IEnumerator ShrinkSupernova()
    {
        if(collider.radius > 0)
        {
            collider.radius -= expandRate * Time.deltaTime;
        }
        var supernovaMain = supernovaPS.main;
        supernovaMain.startSizeMultiplier -= Time.deltaTime * expandRate;
        yield return new WaitForEndOfFrame();
        if(supernovaMain.startSizeMultiplier <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(waitBeforeExploding);
        playing = false;
        StartCoroutine(ShrinkSupernova());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            if (fullyExpanded)
            {
                StarCollided(collision);
            }
        }
        else if (collision.gameObject.GetComponent<Comet>())
        {

            CometCollided(collision);

        }
    }

    private void CometCollided(Collision2D collision)
    {
        Destroy(Instantiate(clashWithComet, collision.GetContact(0).point, Quaternion.identity, transform), clashWithComet.main.duration);
        AudioSource.PlayClipAtPoint(supernovaHit[Random.Range(0, supernovaHit.Length)], Camera.main.transform.position, soundVolume);
        collision.gameObject.GetComponent<Comet>().CometHit();
    }

    private void StarCollided(Collision2D collision)
    {
        ShowDamage(collision);
        Destroy(Instantiate(clashWithStar, collision.GetContact(0).point, Quaternion.identity, transform), clashWithStar.main.duration);
        AudioSource.PlayClipAtPoint(supernovaHit[Random.Range(0, supernovaHit.Length)], Camera.main.transform.position, soundVolume);
        StartCoroutine(RemoveStarLife(collision.gameObject.GetComponent<Star>()));
        FindObjectOfType<Ammo>().DamageDealt(starLivesToRemove);
    }

    private void ShowDamage(Collision2D collision)
    {
        var damageNumber = FindObjectOfType<BlackholeDamageNumber>();
        var numberInstance = Instantiate(damageNumber.GetNumber(), collision.GetContact(0).point, Quaternion.identity, damageNumber.transform);
        numberInstance.GetComponent<Text>().text = "-" + starLivesToRemove.ToString();
    }

    IEnumerator RemoveStarLife(Star star)
    {
        for (int i = 0; i < starLivesToRemove; i++)
        {
            star.RemoveAmmo();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public float GetRadius()
    {
        return myRadius;
    }

    public float GetMaxRadius()
    {
        return maxRadius;
    }
}
