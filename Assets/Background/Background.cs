﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] ParticleSystem stars;
    [SerializeField] Camera mainCamera;

    bool playing = true;

	void Start ()
    {

        StartCoroutine(ChangingColors());
        mainCamera.backgroundColor = new Color(Random.Range(0.1f, 0.15f), Random.Range(0.1f, 0.15f), Random.Range(0.1f, 0.15f));
	}

    IEnumerator ChangingColors()
    {
        var starPs = stars.main;
        float combining = 0f;
        Color startingColor = GetRandomColor();
        Color endingColor = GetRandomColor();
        while (playing)
        {
            if(combining == 1f)
            {
                startingColor = endingColor;
                endingColor = GetRandomColor();
                combining = 0f;
            }
            starPs.startColor = Color.Lerp(startingColor, endingColor, combining);
            combining += 0.02f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    Color GetRandomColor()
    {
        switch (Random.Range(0, 7))
        {
            case (0):
                return Color.blue;
            case (1):
                return Color.cyan;
            case (2):
                return Color.gray;
            case (3):
                return Color.green;
            case (4):
                return Color.magenta;
            case (5):
                return Color.red;
            case (6):
                return Color.white;
            case (7):
                return Color.yellow;
        }
        return Color.black;
    }
}
