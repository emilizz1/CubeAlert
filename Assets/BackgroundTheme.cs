using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTheme : MonoBehaviour
{
    [SerializeField] AudioClip[] themeSFX;

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
        audioSource.volume = 0f;
        audioSource.clip = themeSFX[Random.Range(0, themeSFX.Length)];
        audioSource.Play();
        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        while(Time.time - startTime + 3f < audioSource.clip.length)
        {
            yield return new WaitForSeconds(1f);
        }
        while(audioSource.volume > 0f)
        {
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(PlayTheme(themeSFX[Random.Range(0, themeSFX.Length)]));
    }
}
