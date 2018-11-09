using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] GameObject upgradeDeathParticles;
    [SerializeField] AudioClip upgradeClip;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;


    int extraTime, extraDamage, extraTaps;

    void Start()
    {
        transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayParticlesAndSound();
        Destroy(gameObject);
    }

    public void tapped()
    {
        PlayParticlesAndSound();
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

    void PlayParticlesAndSound()
    {
        AudioSource.PlayClipAtPoint(upgradeClip, Camera.main.transform.position, soundVolume);
        var particles = Instantiate(upgradeDeathParticles, transform.position, Quaternion.identity, transform.parent);
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
}
