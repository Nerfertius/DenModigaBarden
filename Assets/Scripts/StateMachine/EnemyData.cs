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

    [HideInInspector] public bool harmful = true;

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

        playerCollisionFilter.useLayerMask = true;
        playerCollisionFilter.layerMask = 1 << 13; // player layer = 13
        harmful = true;
    }

    public void FixedUpdate() {
        checkPlayerCollision();
    }


    private bool isTouchingPlayer = false;

    private void checkPlayerCollision() {
        

        foreach(Collider2D coll in colliders) {
            
            if (coll.enabled) {
                Collider2D[] results = new Collider2D[1];

                Physics2D.OverlapCollider(coll, playerCollisionFilter, results);
                
                foreach (Collider2D collRes in results) {
                    if (collRes != null) {
                        
                        if (collRes.tag == "Player") {
                            if (isTouchingPlayer) {
                                PlayerData.player.controller.OnTriggerStay2D(coll);
                                controller.OnTriggerStay2D(collRes);
                                return;
                           }
                            else {
                                PlayerData.player.controller.OnTriggerEnter2D(coll);
                                controller.OnTriggerEnter2D(collRes);
                                isTouchingPlayer = true;
                                return;
                            }
                        }
                    }
                    if (isTouchingPlayer) {
                        PlayerData.player.controller.OnTriggerExit2D(coll);
                        controller.OnTriggerExit2D(collRes);
                        isTouchingPlayer = false;
                    }
                }
            }
        }
    }
}
