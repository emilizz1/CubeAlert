using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour
{
    [SerializeField] float expandRate;
    [SerializeField] float maxRadius;
    [SerializeField] float starLivesToRemove = 5f;
    [SerializeField] float waitTimeBetweenLifeRemove = 0.3f;

    ParticleSystem supernovaPS;

	void Start ()
    {
        supernovaPS = GetComponentInChildren<ParticleSystem>();		
	}
	
	void Update ()
    {
        var supernovaShape = supernovaPS.shape;
        supernovaShape.radius += expandRate * Time.deltaTime;
        if(supernovaShape.radius > maxRadius)
        {
            Explode();
        }
	}

    void Explode()
    {
        foreach(Star star in FindObjectsOfType<Star>())
        {
            if(Vector2.Distance(star.transform.position, transform.position) <= maxRadius)
            {
                StartCoroutine(RemoveStarLife(star));
            }
        }
        Destroy(supernovaPS);
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
