using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minDistance;
    [SerializeField] ParticleSystem absorbingStar;
    [SerializeField] bool tutorial = false;
    [SerializeField] AudioClip[] cometImpact;
    [SerializeField] AudioClip starEaten;
    [SerializeField] GameObject[] particles;

    bool alive = true;

    Vector3 pos;
    CameraShaker cameraShaker;
    LifePoints lifePoints;
    Vector3 targetPos;
    CircleCollider2D circleCollider;
    BlackholeDamageNumber damageNumber;
    AudioSource audioSource;
    

    void Start()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        lifePoints = GetComponent<LifePoints>();
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        cameraShaker = FindObjectOfType<CameraShaker>();
        damageNumber = FindObjectOfType<BlackholeDamageNumber>();
        audioSource = GetComponent<AudioSource>();
        Instantiate(particles[Random.Range(0, particles.Length)], transform.position, Quaternion.identity, transform);
        GetNewTargetPos();
    }

    void Update()
    {
        if (alive)
        {

            if (!CheckIfCollidingWithSupernova() && !tutorial)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
            }
            if (Vector3.Distance(transform.position, targetPos) < minDistance && !tutorial)
            {
                GetNewTargetPos();
            }
                UpdateSizeFromLifePoints();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            audioSource.clip = starEaten;
            audioSource.Play();
            var figurePS = Instantiate(absorbingStar, collision.GetContact(0).point, Quaternion.identity, collision.gameObject.transform);
            var figure = collision.gameObject.GetComponent<Star>();
            StartCoroutine(AbsorbingFigure(figure));
            figure.DestroyFigure(false);
        }
        else if (collision.gameObject.GetComponent<Comet>())
        {
            audioSource.clip = cometImpact[Random.Range(0, cometImpact.Length)];
            audioSource.Play();
            GameObject numberInstance = Instantiate(damageNumber.GetNumber(), collision.GetContact(0).point, Quaternion.identity, damageNumber.transform);
            int healing = collision.gameObject.GetComponent<Comet>().GetHealing();
            numberInstance.GetComponent<Text>().text = "+" + healing.ToString();
            lifePoints.RemoveLife(-healing);
            cameraShaker.AddShakeDuration(0.2f);
            collision.gameObject.GetComponent<Comet>().CometHitBlackhole();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AbsorbingFigure(Star figure)
    {
        int absorbedBullets = figure.GetBulletAmount();
        while (figure)
        {
            figure.transform.localPosition = Vector2.MoveTowards(figure.transform.localPosition, transform.position, 0.5f);
            if (absorbedBullets > 0)
            {
                lifePoints.RemoveLife();
                absorbedBullets--;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void UpdateSizeFromLifePoints()
    {
        circleCollider.radius = 1.35f + (lifePoints.GetCurrentLifePoints() * 0.025f);
        foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>())
        {
            var particles = particle.main;
            particles.startSizeMultiplier = circleCollider.radius * 2f + 2f;
        }
    }

    void GetNewTargetPos()
    {
        float minX = pos.x * 0.3f;
        float maxX = pos.x * -0.7f;
        float minY = pos.y * 0.3f;
        float maxY = pos.y * -0.7f;
        targetPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    public void BlackholeDied()
    {
        alive = false;
    }

    bool CheckIfCollidingWithSupernova()
    {
        if (FindObjectOfType<Supernova>())
        {
            foreach (Supernova supernova in FindObjectsOfType<Supernova>())
            {
                float extraDistance = supernova.GetMaxRadius() + circleCollider.radius;
                if (Vector2.Distance(transform.position, supernova.transform.position) - extraDistance  < 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
