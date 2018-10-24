﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    [SerializeField] int minLifePoints = 10;
    [SerializeField] int maxLifePoints = 15;
    [SerializeField] float shrinkingSpeed = 1f;
    [SerializeField] ParticleSystem[] blackHoleDeath;
    [SerializeField] AudioClip[] blackholeCompleted;
    [Range(0f, 1f)] [SerializeField] float soundVolume = 0.5f;

    bool alive = true;
    
    int currentLife;
    GameObject lifeNumber;
    Text lifeNumberText;
    PointRipple pointRipple;

    void Start()
    {
        currentLife = Random.Range(minLifePoints, maxLifePoints) + 10 * FindObjectOfType<LevelHolder>().currentLevel;
        FindObjectOfType<Ammo>().AddMaxPortalAmmo(currentLife);
        lifeNumber = FindObjectOfType<BlackholeNumber>().GetNumber();
        pointRipple = FindObjectOfType<PointRipple>();
        lifeNumberText = lifeNumber.GetComponent<Text>();
    }

    void Update()
    {
            UpdateLife();
    }

    public void RemoveLife(int amount = 1)
    {
        currentLife -= amount;
        UpdateLife();
        if (currentLife <= 0)
        {
            alive = false;
            Destroy(lifeNumber);
            StartCoroutine(BlackHoleDeath());
        }
    }

    public int GetCurrentLifePoints()
    {
        return currentLife;
    }

    void UpdateLife()
    {
        if (alive)
        {
            lifeNumberText.text = currentLife.ToString();
            lifeNumber.transform.position = transform.position;
        }
    }

    IEnumerator BlackHoleDeath()
    {
        var clipToPlay = blackholeCompleted[Random.Range(0, blackholeCompleted.Length)];
        AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position, soundVolume);
        float deathTime = clipToPlay.length;
        GetComponent<BlackHole>().BlackholeDied();
        Destroy(GetComponent<CircleCollider2D>());
        foreach (ParticleSystem particle in blackHoleDeath)
        {
            particle.Play();
        }
        while (deathTime >= 0)
        {
            deathTime -= Time.deltaTime;
            transform.localScale -= new Vector3(shrinkingSpeed * Time.deltaTime, shrinkingSpeed * Time.deltaTime, 0f);
            pointRipple.AddRipples(transform.position);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}