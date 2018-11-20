using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;
    [SerializeField] GameObject tapedParticles;
    [SerializeField] AudioClip upgradeClip;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;
    [SerializeField] int extraTime;
    [SerializeField] int extraDamage;
    [SerializeField] int extraTaps;

    void Start()
    {
        transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayParticlesAndSound(deathParticles);
        Destroy(gameObject);
    }

    public void tapped()
    {
        PlayParticlesAndSound(tapedParticles);
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

    void PlayParticlesAndSound(GameObject particlesToPlay)
    {
        AudioSource.PlayClipAtPoint(upgradeClip, Camera.main.transform.position, soundVolume);
        var particles = Instantiate(particlesToPlay, transform.position, Quaternion.identity, transform.parent);
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
}
