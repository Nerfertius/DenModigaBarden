using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MelodyInteractableData
{
    [HideInInspector] public Transform player = null; // Set by SightCollider script

    public float speed;
    public float chaseSpeed;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public StateController controller;

    private Collider2D sightColl;

    [HideInInspector] public SightColliderAV sight;
    [HideInInspector] public Collider2D[] colliders;

    private ContactFilter2D playerCollisionFilter;

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

        colliders = GetComponents<Collider2D>();
        controller = GetComponent<StateController>();
        playerCollisionFilter.layerMask = 13; // 13 = player
    }

    public void Update() {
        checkPlayerCollision();
    }


    private void checkPlayerCollision() {
       
        foreach(Collider2D coll in colliders) {

            if (coll.enabled) {

                foreach(Collider2D thisColl in colliders) {
                    Collider2D[] results = new Collider2D[1];
                    Physics2D.OverlapCollider(thisColl, playerCollisionFilter, results);
                    
                    foreach (Collider2D collRes in results) {
                        if(collRes != null) {
                            if (collRes.tag == "Player") {
                                PlayerData.player.collidedWithEnemy(coll);
                                controller.OnTriggerStay2D(collRes);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
