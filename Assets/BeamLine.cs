using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLine : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 endPos;
    [SerializeField] int starLivesToRemove = 3;
    [SerializeField] ParticleSystem clashWithStar;
    [SerializeField] AudioClip[] supernovaHit;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;
    [SerializeField] bool rotating = true;
    
    float rotationSpeed;
    Vector2 startPos;
    Vector2 currentlyMovingTo;

    private void Start()
    {
        rotationSpeed = Random.Range(-40, 40f);
        startPos = transform.position;
        currentlyMovingTo = endPos;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentlyMovingTo, moveSpeed);
        if (rotating)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (transform.position == new Vector3(endPos.x, endPos.y, 0f))
        {
            currentlyMovingTo = startPos;
        }
        else if(transform.position == new Vector3(startPos.x, startPos.y, 0f))
        {
            currentlyMovingTo = endPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            StarCollided(collision);
        }
    }

    private void StarCollided(Collision2D collision)
    {
        Destroy(Instantiate(clashWithStar, collision.GetContact(0).point, Quaternion.identity, transform), clashWithStar.main.duration);
        AudioSource.PlayClipAtPoint(supernovaHit[Random.Range(0, supernovaHit.Length)], Camera.main.transform.position, soundVolume);
        StartCoroutine(RemoveStarLife(collision.gameObject.GetComponent<Star>()));
        FindObjectOfType<Ammo>().DamageDealt(starLivesToRemove);
    }

    IEnumerator RemoveStarLife(Star star)
    {
        for (int i = 0; i < starLivesToRemove; i++)
        {
            star.RemoveAmmo();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
