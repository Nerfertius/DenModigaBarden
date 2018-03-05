using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleData : EnemyData {

    public PolygonCollider2D idleCollider;
    public PolygonCollider2D dashCollider;

    [HideInInspector] public Vector2 velocityBeforeFrozen;
    [HideInInspector] public Vector2 previousVelocity;

    public ColorBlinkData frozenColorBlinkData;
    [HideInInspector] public PlatformEffector2D platformEffector;

    public Vector2 DashForce = new Vector2(500, 350);

    public bool CanSeeThrughWalls = false;

    [HideInInspector] public AudioClipPlayLimiter gargoyleCollideSound;

    protected override void Awake() {
        base.Awake();
        platformEffector = GetComponent<PlatformEffector2D>();

        frozenColorBlinkData.defaultColor = spriteRenderer.color;
    }

    protected override void Start()
    {
        base.Start();
        gargoyleCollideSound = new AudioClipPlayLimiter(Resources.Load("SoundEffects/Gargoyle/Gargoyle_Collision4") as AudioClip);
    }

    public void Update() {
        previousVelocity = rb.velocity;
    }


    public override void OnEnable() {
        base.OnEnable();
        frozenColorBlinkData.End(spriteRenderer);
        SetPlatformEffector(false);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.layer = 11; // Enemy
        
    }

    public void SetPlatformEffector(bool enabled)
    {
        idleCollider.usedByEffector = enabled;
        dashCollider.usedByEffector = enabled;
        platformEffector.enabled = enabled;
    }
}
