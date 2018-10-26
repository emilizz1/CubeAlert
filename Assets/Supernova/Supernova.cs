using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour
{
    [SerializeField] float expandRate;
    [SerializeField] float minRadius;
    [SerializeField] float maxRadius;
    [SerializeField] float starLivesToRemove = 3f;
    [SerializeField] float waitBeforeExploding = 5f;
    [SerializeField] ParticleSystem clashWithComet;
    [SerializeField] ParticleSystem clashWithStar;
    [SerializeField] AudioClip[] supernovaHit;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;

    bool playing = true;

    ParticleSystem supernovaPS;
    CircleCollider2D collider;
    float myRadius;
     
	void Start ()
    {
        supernovaPS = GetComponentInChildren<ParticleSystem>();
        collider = GetComponent<CircleCollider2D>();
        myRadius = Random.Range(minRadius, maxRadius);
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
        if(collider.radius > 0)
        {
            collider.radius -= expandRate * Time.deltaTime;
        }
        var supernovaMain = supernovaPS.main;
        supernovaMain.startSizeMultiplier -= Time.deltaTime * expandRate;
        yield return new WaitForEndOfFrame();
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
            Destroy(Instantiate(clashWithStar, collision.GetContact(0).point, Quaternion.identity, transform), clashWithStar.main.duration);
            AudioSource.PlayClipAtPoint(supernovaHit[Random.Range(0, supernovaHit.Length)], Camera.main.transform.position, soundVolume);
            StartCoroutine( RemoveStarLife(collision.gameObject.GetComponent<Star>()));
        }
        else if (collision.gameObject.GetComponent<Comet>())
        {
            Destroy(Instantiate(clashWithComet, collision.GetContact(0).point, Quaternion.identity, transform), clashWithComet.main.duration);
            AudioSource.PlayClipAtPoint(supernovaHit[Random.Range(0, supernovaHit.Length)], Camera.main.transform.position, soundVolume);
            collision.gameObject.GetComponent<Comet>().CometHit();
        }
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
