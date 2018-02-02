using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleData : EnemyData {

    [HideInInspector] public bool harmful;
    public PolygonCollider2D idleCollider;
    public PolygonCollider2D dashCollider;

    [HideInInspector] public Vector2 velocityBeforeFrozen;



    protected override void Start() {
        base.Start();
    }
}
