using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MelodyInteractableData
{
    [HideInInspector] public Transform player = null; // Set by SightCollider script

    public float speed;
    public float chaseSpeed;

    [HideInInspector] public Rigidbody2D rb;

    private Collider2D sightColl;

    public SightColliderAV sight;

    protected virtual void Start()
    {

        sight = transform.GetComponentInChildren<SightColliderAV>();

        rb = GetComponent<Rigidbody2D>();

        if (transform.childCount != 0)
        {

            sightColl = transform.GetChild(0).GetComponent<Collider2D>();
        }

        facingRight = GetComponent<SpriteRenderer>().flipX;

        if (facingRight)
        {
            currentDirection.x = 1 * transform.localScale.x;
        }
        else
        {
            currentDirection.x = -1 * transform.localScale.x;
        }
    }
}
