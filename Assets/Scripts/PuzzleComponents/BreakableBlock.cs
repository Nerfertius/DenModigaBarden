using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour {


    public float velocityNeededToBreak;

    private bool alive = true;


    public void Update() {
        if (!alive) {
            Destroy(this.gameObject);
        }
    }

	public void OnCollisionEnter2D(Collision2D coll) {
        EnemyData data = coll.gameObject.GetComponent<EnemyData>();

        if (data != null && data.isHeavy && coll.relativeVelocity.sqrMagnitude >= Mathf.Pow(velocityNeededToBreak, 2)) {
            alive = false;
        }
    }
}
