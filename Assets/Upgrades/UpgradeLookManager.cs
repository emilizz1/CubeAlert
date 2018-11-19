using System.Collections;
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
    
    bool growing = false;

    int mainSR = 0;
    int sideSR = 1;
    float rotation;
    SpriteRenderer[] mySpriteRenderers;
    int currentFrame = 0;

    void Start ()
    {
        rotation = Random.Range(-rotationSpeed, rotationSpeed);
        SetStartingAlphaValues();
        ChangeFrame();
    }
	
	void Update ()
    {
        Rotate();
        Grow();
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

    void SetStartingAlphaValues()
    {
        mySpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        mySpriteRenderers[mainSR].color = new Color(1f, 1f, 1f, maxAlpha);
        mySpriteRenderers[sideSR].color = new Color(1f, 1f, 1f, minAlpha);
    }

    void ChangeFrame()
    {
        mySpriteRenderers[sideSR].sprite = frames[currentFrame++];
        // Resets frames to start over
        if(currentFrame == frames.Length)
        {
            currentFrame = 0;
        }
        // Swaps main and side Sr
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
        growing = !growing;
        ChangeFrame();
    }
}
