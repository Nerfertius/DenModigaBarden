using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/NPC/NPCStartTalking")]
public class NPCStartTalking : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        bool interactInRange = data.playerInRange && Input.GetButtonDown("Interact");
        bool autospeak = data.autoSpeak && Vector2.Distance(GameManager.instance.player.transform.position, data.transform.position) <= data.autoSpeakRange;
        if (data.autoSpeak && data.autoSpeakRange == 0)
        {
            Debug.LogWarning("autoSpeakRange is 0");
        }
        if (interactInRange || autospeak || (autospeak && data.startTalking))
        {
            data.startTalking = false;
            data.inAutoRange = autospeak;
            return true;
        }
        return false;
    }

    public override bool? CheckTriggerEnter(StateController controller, Collider2D other)
    {
        if (other.tag == "Player")
        {
            NPCData data = (NPCData)controller.data;
            data.playerInRange = true;
            data.player = other.GetComponent<PlayerData>();
        }
        return null;
    }
    public override bool? CheckTriggerExit(StateController controller, Collider2D other)
    {
        if (other.tag == "Player")
        {
            NPCData data = (NPCData)controller.data;
            data.playerInRange = false;
            data.player = null;
        }
        return null;
    }
}
