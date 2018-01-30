using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/NPC/NPCStartTalking")]
public class NPCStartTalking : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (data.playerInRange && Input.GetButtonDown("Interact"))
        {
            return true;
        }
        return false;
    }

    public override bool? CheckTriggerEnter(StateController controller, Collider2D other)
    {
        if (other.tag == "Player")
            ((NPCData)controller.data).playerInRange = true;
        return null;
    }
    public override bool? CheckTriggerExit(StateController controller, Collider2D other)
    {
        if (other.tag == "Player")
            ((NPCData)controller.data).playerInRange = false;
        return null;
    }
}
