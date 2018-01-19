using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerWalking")]
public class PlayerWalking : Condition {

    public override bool? CheckCondition(StateController controller)
    {
        return true;
    }
}
