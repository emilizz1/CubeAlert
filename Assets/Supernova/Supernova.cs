using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour
{
    [SerializeField] float expandRate;
    [SerializeField] float minRadius;
    [SerializeField] float maxRadius;
    [SerializeField] float starLivesToRemove = 3f;
    [SerializeField] float waitTimeBetweenLifeRemove = 0.1f;
    [SerializeField] float waitBeforeExploding = 5f;

    [SerializeField] float startingRadius = 0.1f;
    [SerializeField] float startingSizeMin = 0.2f;
    [SerializeField] float startingSizeMax = 0.6f;

    [SerializeField] AudioClip[] supernovaHit;

    bool playing = true;

    ParticleSystem supernovaPS;
    CircleCollider2D collider;
    float myRadius;
     
	void Start ()
    {
        supernovaPS = GetComponentInChildren<ParticleSystem>();
        collider = GetComponent<CircleCollider2D>();
        myRadius = Random.Range(minRadius, maxRadius);
        SetStartingStats();
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
            StartCoroutine(Explode());
        }
        var supernovaMain = supernovaPS.main;
        if (supernovaMain.startSize.constantMax <= (collider.radius * 2f))
        {
            supernovaMain.startSizeMultiplier += Time.deltaTime * expandRate;
        }
    }

    IEnumerator ShrinkSupernova()
    {
        var supernovaMain = supernovaPS.main;
        supernovaMain.startSizeMultiplier -= Time.deltaTime * expandRate;
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
        playing = false;
        StartCoroutine(ShrinkSupernova());
        Destroy(gameObject, waitBeforeExploding);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            GetComponent<AudioSource>().clip = supernovaHit[Random.Range(0, supernovaHit.Length)];
            GetComponent<AudioSource>().Play();
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

    public float GetRadius()
    {
        return myRadius;
    }
}
