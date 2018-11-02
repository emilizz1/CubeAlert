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
    [SerializeField] ParticleSystem clashWithComet;
    [SerializeField] bool tutorial = false;
    [SerializeField] AudioClip[] cometImpact;
    [SerializeField] AudioClip[] starEaten;
    [SerializeField] GameObject[] particles;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;

    bool alive = true;

    Vector3 pos;
    CameraShaker cameraShaker;
    LifePoints lifePoints;
    Vector3 targetPos;
    CircleCollider2D circleCollider;
    BlackholeDamageNumber damageNumber;
    GameObject myParticle;
    float waitForNewPos = 1f;

    void Start()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        lifePoints = GetComponent<LifePoints>();
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        cameraShaker = FindObjectOfType<CameraShaker>();
        damageNumber = FindObjectOfType<BlackholeDamageNumber>();
        myParticle = Instantiate(particles[Random.Range(0, particles.Length)], transform.position, Quaternion.identity, transform);
        GetNewTargetPos();
    }

    void Update()
    {
        if (alive)
        {
            if (!tutorial)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
            }
            if (Vector3.Distance(transform.position, targetPos) < minDistance && !tutorial)
            {
                GetNewTargetPos();
            }
            CheckIfCollidingWithSupernova();
            UpdateSizeFromLifePoints();
        }
        waitForNewPos -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            StarCollided(collision);
        }
        else if (collision.gameObject.GetComponent<Comet>())
        {
            CometCollided(collision);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    private void StarCollided(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(starEaten[Random.Range(0, starEaten.Length)], Camera.main.transform.position, soundVolume);
        Instantiate(absorbingStar, collision.GetContact(0).point, Quaternion.identity, collision.gameObject.transform);
        var figure = collision.gameObject.GetComponent<Star>();
        StartCoroutine(AbsorbingFigure(figure));
        figure.DestroyFigure(false);
    }

    private void CometCollided(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(cometImpact[Random.Range(0, cometImpact.Length)], Camera.main.transform.position, soundVolume);
        Destroy(Instantiate(clashWithComet, collision.GetContact(0).point, Quaternion.identity), clashWithComet.main.duration);
        GetHealed(collision);
        cameraShaker.AddShakeDuration(0.2f);
        collision.gameObject.GetComponent<Comet>().CometHit();
    }

    private void GetHealed(Collision2D collision)
    {
        GameObject numberInstance = Instantiate(damageNumber.GetNumber(), collision.GetContact(0).point, Quaternion.identity, damageNumber.transform);
        int healing = collision.gameObject.GetComponent<Comet>().GetHealing();
        numberInstance.GetComponent<Text>().text = "+" + healing.ToString();
        lifePoints.RemoveLife(-healing);
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
        if (waitForNewPos <= 0)
        {
            float minX = pos.x * 0.3f;
            float maxX = pos.x * -0.7f;
            float minY = pos.y * 0.3f;
            float maxY = pos.y * -0.7f;
            targetPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            waitForNewPos = 3f;
        }
    }

    public void BlackholeDied()
    {
        alive = false;
        Destroy(myParticle);
    }

    void CheckIfCollidingWithSupernova()
    {
        if (FindObjectOfType<Supernova>())
        {
            foreach (Supernova supernova in FindObjectsOfType<Supernova>())
            {
                float extraDistance = supernova.GetRadius() + circleCollider.radius;
                if (Vector2.Distance(transform.position, supernova.transform.position) - extraDistance  < 0)
                {
                    GetNewTargetPosFromSupernova(supernova.transform.position);
                }
            }
        }
    }

    void GetNewTargetPosFromSupernova(Vector3 supernovaPos)
    {
        if (waitForNewPos <= 0)
        {
            targetPos = new Vector2(-supernovaPos.x, -supernovaPos.y);
            waitForNewPos = 3f;
        }
    }

    public void SetAlive(bool set)
    {
        alive = set;
    }
}
