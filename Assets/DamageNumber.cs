using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float fadingSpeed = 0.01f;
    [SerializeField] float destructionTime = 5f;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        Destroy(gameObject, destructionTime);
    }

    void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, speed);
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - fadingSpeed);
	}
}
