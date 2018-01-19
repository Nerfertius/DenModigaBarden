using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/NPC/NPCTalking")]
public class NPCTalking : Condition {
    public override bool? CheckCondition(StateController controller)
    {
        return false;
    }
}
