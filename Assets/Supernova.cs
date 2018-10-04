using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour
{
    [SerializeField] float expandRate;
    [SerializeField] float maxRadius;
    [SerializeField] float starLivesToRemove = 5f;
    [SerializeField] float waitTimeBetweenLifeRemove = 0.1f;

    [SerializeField] float startingRadius = 0.1f;
    [SerializeField] float startingSizeMin = 0.2f;
    [SerializeField] float startingSizeMax = 0.6f;

    [SerializeField] ParticleSystem Range;
    [SerializeField] ParticleSystem supernovaPS;
    [SerializeField] ParticleSystem supernovaExplosion;

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
            var supernovaShape = supernovaPS.shape;
            ExpandSupernova();
            if (supernovaShape.radius > maxRadius)
            {
               Explode();
            }
        }
	}

    void ExpandSupernova()
    {
        var supernovaShape = supernovaPS.shape;
        var rangeShape = Range.shape;
        supernovaShape.radius += expandRate * Time.deltaTime;
        var supernovaSize = supernovaPS.main.startSize;
        if (supernovaSize.constantMin < startingSizeMin * 10)
        {
            supernovaSize.constantMin = startingSizeMin;
        }
        if (supernovaSize.constantMax < startingSizeMax * 10)
        {
            supernovaSize.constantMax = startingSizeMax;
        }
        if (rangeShape.radius < maxRadius)
        {
            rangeShape.radius += expandRate * Time.deltaTime * 8f;
            collider.radius = rangeShape.radius;
        }
    }

    void SetStartingStats()
    {
        var supernovaShape = supernovaPS.shape;
        var rangeShape = Range.shape;
        supernovaShape.radius = startingRadius;
        var supernovaSize = supernovaPS.main.startSize;
        supernovaSize.constantMin = startingSizeMin;
        supernovaSize.constantMax = startingSizeMax;
        rangeShape.radius = 0f;
        collider.radius = rangeShape.radius;
    }

    void Explode()
    {
        playing = false;
        foreach (Star star in FindObjectsOfType<Star>())
        {
            if(Vector2.Distance(star.transform.position, transform.position) <= maxRadius)
            {
                StartCoroutine(RemoveStarLife(star));
            }
        }
        supernovaExplosion.gameObject.SetActive(true);
        Destroy(supernovaExplosion, supernovaExplosion.main.duration *2f);
        Destroy(supernovaPS);
        Destroy(Range);
        Destroy(gameObject, starLivesToRemove * waitTimeBetweenLifeRemove);
    }

    IEnumerator RemoveStarLife(Star star)
    {
        for (int i = 0; i < starLivesToRemove; i++)
        {
            star.RemoveAmmo();
            yield return new WaitForSeconds(waitTimeBetweenLifeRemove);
        }
    }


}
