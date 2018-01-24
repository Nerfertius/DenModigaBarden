using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodtProjectile : MonoBehaviour {

    public Melody.MelodyID melodyID;

    public float Speed;
    public bool FacingRight;
    //public float Direction;

    private bool Alive = true;


    public void Init(float Speed, bool FacingRight) {
        this.Speed = Speed;
        this.FacingRight = FacingRight;
    }

    public void FixedUpdate() {

        if (!Alive) {
            Destroy(gameObject);
        }

        if (FacingRight) {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
        else {
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }       
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Alive = false;
    }
}
