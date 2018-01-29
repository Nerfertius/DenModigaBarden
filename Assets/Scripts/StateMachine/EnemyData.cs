using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MelodyInteractableData {
    [HideInInspector] public Transform player = null; // Set by SightCollider script

    public float speed;
    public float chaseSpeed;

    [HideInInspector] public Rigidbody2D rb;

    private Collider2D sightColl;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();

        if (transform.childCount != 0) {
            sightColl = GetComponentsInChildren<Collider2D>()[1];
        }

        facingRight = GetComponent<SpriteRenderer>().flipX;

        if (facingRight)
        {
            currentDirection.x = 1;
        }
        else
        {
            currentDirection.x = -1;
        }
    }
}
