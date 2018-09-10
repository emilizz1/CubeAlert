using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] ParticleSystem stars;
    [SerializeField] Camera mainCamera;

	void Start ()
    {
        stars.startColor = GetRandomColor();
        mainCamera.backgroundColor = new Color(Random.Range(0.17f, 0.33f), Random.Range(0.17f, 0.33f), Random.Range(0.17f, 0.33f));
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
