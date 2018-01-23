using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/NPC/NPCStopTalking")]
public class NPCStopTalking : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        NPCData data = (NPCData) controller.data;
        if (data.endOfConv) {
            data.endOfConv = false;
            return true;
        }
        return false;
    }

    public override bool? CheckTriggerExit(StateController controller, Collider2D other)
    {
        bool player = other.tag == "Player";
        if (player){
            ((NPCData)controller.data).playerInRange = false;
        }
        return player;
    }
}
