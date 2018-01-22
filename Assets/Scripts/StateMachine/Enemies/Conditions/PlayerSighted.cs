using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/PlayerSighted")]
public class PlayerSighted : Condition {
    public override bool? CheckCondition(StateController controller)
    {
        return null;
    }
}
