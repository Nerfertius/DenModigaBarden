﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public static bool controllable;
    public float speed;
    public Vector2 direction;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controllable = false;
    }

    void Update()
    {
        if (!controllable) return;
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
