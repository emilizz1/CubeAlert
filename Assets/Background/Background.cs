using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    [SerializeField] float tileSize;

    Vector3 startPosition;

	void Start ()
    {
        startPosition = transform.position;
	}
	

	void Update ()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPosition + Vector3.up * newPosition;
        
	}
}
