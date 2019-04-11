using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBarBackground : MonoBehaviour
{
    [SerializeField] GameObject firstImage;
    [SerializeField] GameObject secondImage;

    float movingSpeed;
    
    void Start()
    {
        movingSpeed = Random.Range(0.01f, 0.1f);
    }
    
    void Update()
    {
        
    }
}
