﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLookManager : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    [SerializeField] float maxAlpha = 0.7f;
    [SerializeField] float minAlpha = 0.25f;
    [SerializeField] float rotationSpeed = 55f;
    [SerializeField] float fadeSpeed = 0.3f;
    [SerializeField] float growthSpeed = 0.65f;
    [SerializeField] float transitionMinValue = 0.5f;
    [SerializeField] float transitionMaxValue = 2f;

    bool alive = true;
    bool growing = false;

    int mainSR, sideSR;
    float rotation;
    SpriteRenderer[] mySpriteRenderers;
    int currentFrame = 0;

    void Start ()
    {
        mySpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        mySpriteRenderers[mainSR].color = new Color(1f, 1f, 1f, maxAlpha);
        mySpriteRenderers[sideSR].color = new Color(1f, 1f, 1f, minAlpha);
        mainSR = 0;
        sideSR = 1;
        rotation = Random.Range(-rotationSpeed, rotationSpeed);
        ChangeFrame();
    }
	
	void Update ()
    {
        StartCoroutine(Timer());
        Rotate();
        Grow();
    }

    IEnumerator Timer()
    {
        while (alive)
        {
            yield return new WaitForSeconds(Random.Range(transitionMinValue, transitionMaxValue));
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

    void ChangeFrame()
    {
        mySpriteRenderers[sideSR].sprite = frames[currentFrame++];
        if(currentFrame == frames.Length)
        {
            currentFrame = 0;
        }
        if(mainSR == 0)
        {
            mainSR = 1;
            sideSR = 0;
        }
        else
        {
            mainSR = 0;
            sideSR = 1;
        }
        StartCoroutine(ChangingFrame());
    }

    IEnumerator ChangingFrame()
    {
        while(mySpriteRenderers[mainSR].color.a < maxAlpha)
        {
            float alphaChange = Time.deltaTime * fadeSpeed;
            mySpriteRenderers[mainSR].color = new Color(1f, 1f, 1f, mySpriteRenderers[mainSR].color.a + alphaChange);
            mySpriteRenderers[sideSR].color = new Color(1f, 1f, 1f, mySpriteRenderers[sideSR].color.a - alphaChange);
            yield return new WaitForEndOfFrame();
        }
        ChangeFrame();
    }
}
