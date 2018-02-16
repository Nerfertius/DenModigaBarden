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
    protected override void Start() {
        base.Start();
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    public void Update() {
        previousVelocity = rb.velocity;
    }
}
