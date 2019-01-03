using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TunnelEffect;

public class TunnelRandomizer : MonoBehaviour
{
    bool playing = true;

    TunnelFX2 myTunnel;
    float minColor, maxColor;

    void Start()
    {
        myTunnel = GetComponent<TunnelFX2>();
        RandomizeTunnel();
        StartCoroutine(ChangingBackgroundColors());
        StartCoroutine(ChangingTintColors());
    }

    void RandomizeTunnel()
    {
        switch (Random.Range(0, 2))
        {
            case (0):
                myTunnel.preset = TUNNEL_PRESET.SpaceTravel;
                myTunnel.hyperSpeed = Random.Range(0f, 0.6f);
                minColor = 0.4f;
                maxColor = 1f;
                break;
            case (1):
                myTunnel.preset = TUNNEL_PRESET.CloudAscension;
                myTunnel.hyperSpeed = Random.Range(0f, 0.4f);
                minColor = 0.6f;
                maxColor = 1f;
                break;
        }
        myTunnel.animationAmplitude = 0.1f;
        myTunnel.globalAlpha = Random.Range(0.5f, 0.8f);
        myTunnel.fallOff = Random.Range(0.3f, 1f);
    }

    Color GetBackgroundColor()
    {
        switch (Random.Range(0, 3))
        {
            case (0):
                return new Color(1f, Random.Range(minColor, maxColor), Random.Range(minColor, maxColor));
            case (1):
                return new Color(Random.Range(minColor, maxColor), 1f, Random.Range(minColor, maxColor));
            case (2):
                return new Color(Random.Range(minColor, maxColor), Random.Range(minColor, maxColor), 1f);
            case (3):
                break;
        }
        return Color.white;
    }

    IEnumerator ChangingBackgroundColors()
    {
        float combining = 0f;
        Color startingColor = GetBackgroundColor();
        Color endingColor = GetBackgroundColor();
        while (playing)
        {
            if (combining >= 1f)
            {
                startingColor = endingColor;
                endingColor = GetBackgroundColor();
                combining = 0f;
            }
            myTunnel.backgroundColor = Color.Lerp(startingColor, endingColor, combining);
            combining += 0.03f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ChangingTintColors()
    {
        float combining = 0f;
        Color startingColor = GetBackgroundColor();
        Color endingColor = GetBackgroundColor();
        while (playing)
        {
            if (combining >= 1f)
            {
                startingColor = endingColor;
                endingColor = GetBackgroundColor();
                combining = 0f;
            }
            myTunnel.tintColor = Color.Lerp(startingColor, endingColor, combining);
            combining += 0.03f;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
