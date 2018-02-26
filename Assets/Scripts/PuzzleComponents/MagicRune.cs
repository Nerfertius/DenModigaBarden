using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRune : MonoBehaviour {

    private Collider2D collider;

    public float cooldown;
    private Timer cooldownTimer;

    void Start() {
        cooldownTimer = new Timer(cooldown);
        cooldownTimer.Start();
        collider = GetComponent<CircleCollider2D>();
    }

	public void OnTriggerStay2D(Collider2D coll) {
        if(coll.tag == "Player" && cooldownTimer.IsDone()) {
            //coll.GetComponent<StateController>().OnTriggerStay2D(this.collider);
            cooldownTimer.Start();
        }
    }
}
