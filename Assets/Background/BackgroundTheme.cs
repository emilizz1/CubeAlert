﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTheme : MonoBehaviour
{
    [SerializeField] AudioClip[] themeSFX;

    bool sameTheme = false;

    AudioSource audioSource;
    float maxVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxVolume = audioSource.volume;
        StartCoroutine(PlayTheme(themeSFX[Random.Range(0, themeSFX.Length)]));
    }

    IEnumerator PlayTheme(AudioClip theme)
    {
        float startTime = Time.time;
        audioSource.clip = theme;
        audioSource.Play();
        AudioClip nextTheme = themeSFX[Random.Range(0, themeSFX.Length)];
        if (!sameTheme)
        {
            audioSource.volume = 0f;
            while (audioSource.volume < maxVolume)
            {
                audioSource.volume += 0.01f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if(nextTheme == theme)
        {
            sameTheme = true;
        }
        else
        {
            sameTheme = false;
        }
        if (sameTheme)
        {
            while (Time.time - startTime < audioSource.clip.length)
            {
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            while (Time.time - startTime + 3f < audioSource.clip.length)
            {
                yield return new WaitForSeconds(1f);
            }
            while (audioSource.volume > 0f)
            {
                audioSource.volume -= 0.01f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        StartCoroutine(PlayTheme(nextTheme));
    }
}
