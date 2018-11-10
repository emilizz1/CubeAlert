using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minSpeed = 1f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] ParticleSystem absorbingStar;
    [SerializeField] ParticleSystem clashWithComet;
    [SerializeField] bool tutorial = false;
    [SerializeField] AudioClip[] cometImpact;
    [SerializeField] AudioClip starEaten;
    [SerializeField] GameObject particle;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;

    bool alive = true;

    Vector3 pos;
    CameraShaker cameraShaker;
    LifePoints lifePoints;
    CircleCollider2D circleCollider;
    Rigidbody2D myRigidbody;
    BlackholeDamageNumber damageNumber;

    void Start()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        lifePoints = GetComponent<LifePoints>();
        circleCollider = GetComponent<CircleCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        cameraShaker = FindObjectOfType<CameraShaker>();
        damageNumber = FindObjectOfType<BlackholeDamageNumber>();
        particle = Instantiate(particle, transform.position, Quaternion.identity, transform);
        SetNewParticleColor();
        if (!tutorial)
        {
            GetNewTargetPos();
        }
    }

    void Update()
    {
        if (alive)
        {
            if (!tutorial && checkIfoutOfBounds() || !tutorial && myRigidbody.velocity.magnitude< minSpeed)
            {
                GetNewTargetPos();
            }
            UpdateSizeFromLifePoints();
            if(myRigidbody.velocity.magnitude > maxSpeed)
            {
                myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxSpeed);
            }
        }

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
    }

    private void StarCollided(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(starEaten, Camera.main.transform.position, soundVolume);
        Instantiate(absorbingStar, collision.GetContact(0).point, Quaternion.identity, collision.gameObject.transform);
        var figure = collision.gameObject.GetComponent<Star>();
        StartCoroutine(AbsorbingFigure(figure));
        figure.DestroyFigure(false);
        SetNewParticleColor();
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
        FindObjectOfType<Ammo>().DamageDealt(healing);
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
        circleCollider.radius = 1.35f + (lifePoints.GetCurrentLifePoints() * 0.0275f);
        var particles = particle.GetComponent<ParticleSystem>().main;
        particles.startSizeMultiplier = circleCollider.radius * 4f;
    }

    void GetNewTargetPos()
    {
        float minX = pos.x * 0.3f;
        float maxX = pos.x * -0.7f;
        float minY = pos.y * 0.3f;
        float maxY = pos.y * -0.7f;
        var targetPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        myRigidbody.AddForce((targetPos - transform.position) * moveSpeed, ForceMode2D.Impulse);
    }

    public void BlackholeDied()
    {
        alive = false;
        Destroy(particle);
    }

    public void SetAlive(bool set)
    {
        alive = set;
    }

    void SetNewParticleColor()
    {
        var mainParticle = particle.GetComponent<ParticleSystem>().main;
        switch (Random.Range(0, 7))
        {
            case (0):
                mainParticle.startColor = Color.blue;
                break;
            case (1):
                mainParticle.startColor = Color.cyan;
                break;
            case (2):
                mainParticle.startColor = Color.green;
                break;
            case (3):
                mainParticle.startColor = Color.magenta;
                break;
            case (4):
                mainParticle.startColor = new Color(0.5f, 0f, 1f);
                break;
            case (5):
                mainParticle.startColor = new Color(0f, 0.5f, 1f);
                break;
            case (6):
                mainParticle.startColor = new Color(0f, 1f, 0.5f);
                break;
        }
    }

    bool checkIfoutOfBounds()
    {
        if (transform.position.x < pos.x * 0.3f || transform.position.x > pos.x * -0.7f || transform.position.y < pos.y * 0.3f || transform.position.y > pos.y * -0.7f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}