using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour
{
    [SerializeField] float expandRate;
    [SerializeField] float maxRadius;
    [SerializeField] float starLivesToRemove = 3f;
    [SerializeField] float waitTimeBetweenLifeRemove = 0.1f;
    [SerializeField] float waitBeforeExploding = 5f;

    [SerializeField] float startingRadius = 0.1f;
    [SerializeField] float startingSizeMin = 0.2f;
    [SerializeField] float startingSizeMax = 0.6f;
    
    [SerializeField] ParticleSystem supernovaPS;
    [SerializeField] ParticleSystem supernovaExplosion;

    [SerializeField] AudioClip[] supernovaHit;

    bool playing = true;
    
    CircleCollider2D collider;
     
	void Start ()
    {
        collider = GetComponent<CircleCollider2D>();
        SetStartingStats();
	}
	
	void Update ()
    {
        if (playing)
        {
            if (supernovaPS.shape.radius < maxRadius)
            {
                ExpandSupernova();
                Explode();
            }
            else
            {
                StartCoroutine(Explode());
            }
        }
	}

    void ExpandSupernova()
    {
        var supernovaShape = supernovaPS.shape;
        supernovaShape.radius += expandRate * Time.deltaTime;
        collider.radius = supernovaShape.radius;
        var supernovaSize = supernovaPS.main.startSize;
        if (supernovaSize.constantMin < startingSizeMin * 10)
        {
            supernovaSize.constantMin = startingSizeMin;
        }
        if (supernovaSize.constantMax < startingSizeMax * 10)
        {
            supernovaSize.constantMax = startingSizeMax;
        }
    }

    IEnumerator ShrinkSupernova()
    {
        var supernovaExplosionMain = supernovaExplosion.main;
        supernovaExplosionMain.startSpeedMultiplier -= expandRate * Time.deltaTime * 5f;
        yield return new WaitForEndOfFrame();
    }

    void SetStartingStats()
    {
        var supernovaShape = supernovaPS.shape;
        supernovaShape.radius = startingRadius;
        var supernovaSize = supernovaPS.main.startSize;
        supernovaSize.constantMin = startingSizeMin;
        supernovaSize.constantMax = startingSizeMax;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(waitBeforeExploding);
        StartCoroutine(ShrinkSupernova());
        playing = false;
        supernovaExplosion.Play();
        Destroy(supernovaExplosion, supernovaExplosion.main.duration *2f);
        Destroy(supernovaPS);
        Destroy(gameObject, supernovaExplosion.main.duration * 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            GetComponent<AudioSource>().clip = supernovaHit[Random.Range(0, supernovaHit.Length)];
            GetComponent<AudioSource>().Play();
            supernovaExplosion.Play();
            StartCoroutine( RemoveStarLife(collision.gameObject.GetComponent<Star>()));
        }
    }

    IEnumerator RemoveStarLife(Star star)
    {
        for (int i = 0; i < starLivesToRemove; i++)
        {
            star.RemoveAmmo();
            yield return new WaitForSeconds(waitTimeBetweenLifeRemove);
        }
    }

    public float GetMaxRadius()
    {
        return maxRadius;
    }
}
