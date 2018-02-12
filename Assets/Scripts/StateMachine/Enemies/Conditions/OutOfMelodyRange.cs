using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/OutOfMelodyRange")]
public class OutOfMelodyRange : Condition {

    public override bool? CheckTriggerExit(StateController controller, Collider2D other) {
        return other.tag == "MelodyRange";
    }
}
