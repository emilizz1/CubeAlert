using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TunnelEffect;

public class TunnelRandomizer : MonoBehaviour
{
    TunnelFX2 myTunnel;
    
	void Start ()
    {
        myTunnel = GetComponent<TunnelFX2>();
        RandomizeTunnel();
    }
	
	void RandomizeTunnel()
    {
        myTunnel.animationAmplitude = 0.1f;
        myTunnel.hyperSpeed = Random.Range(0f, 0.6f);
        myTunnel.globalAlpha = Random.Range(0.5f, 0.8f);
        myTunnel.fallOff = Random.Range(0.3f, 1f);
        TintColor();
        BackgroundColor();
    }

    void TintColor()
    {
        switch (Random.Range(0, 3))
        {
            case (0):
                myTunnel.tintColor = new Color(1f, Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                break;
            case (1):
                myTunnel.tintColor = new Color(Random.Range(0.5f, 1f), 1f , Random.Range(0.5f, 1f));
                break;
            case (2):
                myTunnel.tintColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1f);
                break;
            case (3):
                return;
        }
    }

    void BackgroundColor()
    {
        switch (Random.Range(0, 3))
        {
            case (0):
                myTunnel.backgroundColor = new Color(1f, Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                break;
            case (1):
                myTunnel.backgroundColor = new Color(Random.Range(0.5f, 1f), 1f, Random.Range(0.5f, 1f));
                break;
            case (2):
                myTunnel.backgroundColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1f);
                break;
            case (3):
                return;
        }
    }
}
