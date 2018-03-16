using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MelodyInteractableData
{
    [HideInInspector] public Transform player = null; // Set by SightCollider script

    public float speed;
    public float chaseSpeed;
    public float attackRange;
    public bool behaveAsHitbox;
    public bool childCollided;
    public Sprite battleTopBackground;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public StateController controller;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    private Collider2D sightColl;

    [HideInInspector] public SightColliderAV sight;
    [HideInInspector] public Collider2D[] colliders;

    private ContactFilter2D playerCollisionFilter;
    
    private bool isTouchingPlayer = false;
    public bool switchingCollider = false;

    [HideInInspector] public ParticleSystem sleepSFXPrefab;
    [HideInInspector] public ParticleSystem sleepSFXObject;

    public bool isHeavy;

    [HideInInspector] public PlayerDamageData playerDamageData;

    protected virtual void Awake()
    {
        startPos = transform.position;
        startScale = transform.localScale;

        sight = transform.GetComponentInChildren<SightColliderAV>();
        colliders = GetComponents<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start ()
	{
		controller = GetComponent<StateController> ();
		playerDamageData = GetComponent<PlayerDamageData> ();
		playerCollisionFilter.useLayerMask = true;
		playerCollisionFilter.layerMask = 1 << 13; // player layer = 13

		if (behaveAsHitbox)
			return;

		if (transform.childCount != 0) {
			sightColl = transform.GetChild (0).GetComponent<Collider2D> ();
		}

		if (spriteRenderer != null) {
			facingRight = spriteRenderer.flipX;
		}

        if (facingRight)
        {
            currentDirection.x = 1 * transform.localScale.x;
        }
        else
        {
            currentDirection.x = -1 * transform.localScale.x;
        }

        startDirection = currentDirection;

        sleepSFXPrefab = Resources.Load<ParticleSystem>("SFX/Sleep SFX");
    }

    public virtual void OnEnable()
    {
        transform.position = startPos;
        transform.localScale = startScale;
        currentDirection = startDirection;
        if(playerDamageData != null) {
            playerDamageData.harmful = true;
        }
        
        if(sleepSFXObject != null) {
            Destroy(sleepSFXObject.gameObject);
        }
    }

    public void FixedUpdate() {
        checkPlayerCollision();
    }

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
                                if (!behaveAsHitbox) controller.OnTriggerStay2D(collRes);
                                return;
                           }
                            else
                            {
                                PlayerData.player.controller.OnTriggerEnter2D(coll);
                                if (!behaveAsHitbox) controller.OnTriggerEnter2D(collRes);
                                isTouchingPlayer = true;
                                return;
                            }
                        }
                    }
                    if (isTouchingPlayer) {
                        PlayerData.player.controller.OnTriggerExit2D(coll);
                        if (!behaveAsHitbox) controller.OnTriggerExit2D(collRes);
                        isTouchingPlayer = false;
                    }
                }
            }
        }
    }

    public void ToggleColliderSwitchCoroutine()
    {
        StartCoroutine(ToggleColliderSwitch());
    }

    IEnumerator ToggleColliderSwitch()
    {
        switchingCollider = !switchingCollider;
        yield return new WaitForSeconds(0.25f);
    }
}
