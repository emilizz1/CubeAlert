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

    [SerializeField] GameObject Range;

    bool playing = true;

    ParticleSystem supernovaPS;

	void Start ()
    {
        supernovaPS = GetComponentInChildren<ParticleSystem>();
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
        if (Range.transform.localScale.x < maxRadius * 2)
        {
            Range.transform.localScale += new Vector3(expandRate * Time.deltaTime * 8f, expandRate * Time.deltaTime * 8f, 0f);
        }
    }

    void SetStartingStats()
    {
        var supernovaShape = supernovaPS.shape;
        supernovaShape.radius = startingRadius;
        var supernovaSize = supernovaPS.main.startSize;
        supernovaSize.constantMin = startingSizeMin;
        supernovaSize.constantMax = startingSizeMax;
        Range.transform.localScale = new Vector3(0f, 0f, 1f);
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
