using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour {

    private int currentCollider;

    private Collider2D[] parantColliders;

    private Collider2D[] thisColliders;

	void Start () {
        parantColliders = GetComponentsInParent<Collider2D>();

        thisColliders = new Collider2D[parantColliders.Length];
        for (int i = 0; i < parantColliders.Length; i++) {
            //thisColliders[i] = parantColliders[i]
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        for(int i = 0; i < parantColliders.Length; i++) {
            if (parantColliders[i].enabled) {
                currentCollider = i;
               // thisCollider = parantColliders[i];
                break;
            }
        }

	}


    public Collider2D getActiveCollider() {
        if(parantColliders.Length != 0) {
            return parantColliders[currentCollider];
        }
        return null;
    }
}
