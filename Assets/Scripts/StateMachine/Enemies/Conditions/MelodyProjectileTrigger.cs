using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/MelodyProjectileTrigger")]
public class MelodyProjectileTrigger : Condition {

    public Melody.MelodyID melodyID;

    public override bool? CheckTriggerEnter(StateController controller, Collider2D coll) {
        //SongProjectile
        MelodyProjectile projectile = coll.GetComponent<MelodyProjectile>();


        if (projectile != null && projectile.melodyID == melodyID) {
            return true;
        }
        else {
            return false;
        }
    }
}
