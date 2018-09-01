﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float flightSpeed;

    Portal portal;
    Rigidbody2D rb;

    bool working = true;

	void Start ()
    {
        portal = FindObjectOfType<Portal>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (working)
        {
            Vector3 targ = portal.transform.position;
            Vector3 myPos = transform.position;
            targ.z = 0f;
            targ.x = targ.x - myPos.x;
            targ.y = targ.y - myPos.y;
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            rb.AddForce(targ * flightSpeed, ForceMode2D.Force);
        }
    }
}