using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/NPC/NPCStartWalking")]
public class NPCStartWalking : Condition
{

    public override bool? CheckCondition(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (data.canWalk && data.waitTime <= 0) {
            return true;
        }
        return null;
    }
}
