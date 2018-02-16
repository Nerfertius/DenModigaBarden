using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/OutOfMelodyRange")]
public class OutOfMelodyRange : Condition {

    public override bool? CheckTriggerExit(StateController controller, Collider2D other) {
          if(other != null && other.tag == "MelodyRange") {
            return true;
        }
        return false;
    }

    public override bool? CheckCondition(StateController controller) {
        return !PlayerData.player.melodyData.MelodyRange.enabled;
    }
}
