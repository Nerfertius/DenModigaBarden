using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour {


    public float velocityNeededToBreak;
    public UnexploredArea revealOnDestroy;
    public ParticleSystem onBreakEffect;

    private bool alive = true;


    public float cameraShakeDuration = 0.75f;
    public float shakeIntensity = 0.35f;

    public void Update() {
        if (!alive) {
            if(revealOnDestroy != null) {
                revealOnDestroy.Reveal();
            }
            Destroy(this.gameObject);
        }
    }

	public void OnCollisionEnter2D(Collision2D coll) {
        EnemyData data = coll.gameObject.GetComponent<EnemyData>();
        if (data != null && data.isHeavy && coll.relativeVelocity.sqrMagnitude >= Mathf.Pow(velocityNeededToBreak, 2)) {
            CameraFX.Screenshake(cameraShakeDuration, shakeIntensity, shakeIntensity);
            alive = false;
            Instantiate(onBreakEffect, transform.position, Quaternion.identity);
        }
    }
}
