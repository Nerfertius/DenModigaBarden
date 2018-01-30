using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleData : EnemyData {

    [HideInInspector] public bool harmful;
    public PolygonCollider2D idleCollider;
    public PolygonCollider2D dashCollider;

    [HideInInspector] public bool transitFromFrozen = false;


    protected override void Start() {
        base.Start();

        currentDirection = new Vector3(currentDirection.x * -1, currentDirection.y);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);



    }
}
