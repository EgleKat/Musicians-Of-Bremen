﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoVelocity : MonoBehaviour {

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        rb.velocity = Vector2.zero;
	}
}
