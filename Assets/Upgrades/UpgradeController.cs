using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] float growthSpeed = 1f;
    [SerializeField] GameObject upgradeDeathParticles;

    bool alive = true;
    bool fading = true;
    bool growing = false;

    int extraTime, extraDamage, extraTaps;
    float rotation;
    SpriteRenderer mySpriteRenderer;

    void Start()
    {
        transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        rotation = Random.Range(-rotationSpeed, rotationSpeed);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        StartCoroutine(FadeTimer());
        Fade();
        Rotate();
        Grow();
    }

    void Fade()
    {
        if (fading)
        {
            mySpriteRenderer.color = new Color(1f, 1f, 1f, mySpriteRenderer.color.a - (Time.deltaTime * fadeSpeed));
        }
        else
        {
            mySpriteRenderer.color = new Color(1f, 1f, 1f, mySpriteRenderer.color.a + (Time.deltaTime * fadeSpeed));
        }
    }

    IEnumerator FadeTimer()
    {
        while (alive)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            fading = !fading;
            growing = !growing;
        }
    }

    void Grow()
    {
        float growthAmount = Time.deltaTime * growthSpeed;
        if (growing)
        {
            transform.localScale += new Vector3(growthAmount, growthAmount, growthAmount);
        }
        else
        {
            transform.localScale -= new Vector3(growthAmount, growthAmount, growthAmount);
        }
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * rotation));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayParticles();
        Destroy(gameObject);
    }

    public void tapped()
    {
        PlayParticles();
        GiveBonus();
        Destroy(gameObject);
    }

    void GiveBonus()
    {
        if (extraTime > 0)
        {
            FindObjectOfType<Timer>().AddTime(extraTime);
        }
        else if (extraDamage > 0)
        {
            FindObjectOfType<Ammo>().DamageDealt(-extraDamage);
        }
        else if (extraTaps > 0)
        {
            FindObjectOfType<TapNumber>().AddTaps(extraTaps);
        }
    }

    public void AssignBonuses(int extraTime, int extraDamage, int extraTaps)
    {
        this.extraTime = extraTime;
        this.extraDamage = extraDamage;
        this.extraTaps = extraTaps;
    }

    void PlayParticles()
    {
        var particles = Instantiate(upgradeDeathParticles, transform.position, Quaternion.identity, transform.parent);
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
}
