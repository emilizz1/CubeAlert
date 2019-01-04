using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCross : MonoBehaviour
{
    [SerializeField] int starLivesToRemove = 3;
    [SerializeField] ParticleSystem clashWithStar;
    [SerializeField] AudioClip[] supernovaHit;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;

    float rotationSpeed;

    private void Start()
    {
        SetRandomRotation();
    }

    void SetRandomRotation()
    {
        rotationSpeed = Random.Range(-50, 50f);
        if(Mathf.Abs(rotationSpeed) < 20f)
        {
            SetRandomRotation();
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
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
