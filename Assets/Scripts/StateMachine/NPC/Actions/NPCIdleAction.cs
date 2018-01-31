using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/NPC/NPCIdleAction")]
public class NPCIdleAction : StateAction {

    public override void ActOnce(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (data.canWalk)
        {
            data.waitTime = Random.Range(5f, 20f);
        }
    }

    public override void Act(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (data.canWalk) {
            data.waitTime -= Time.deltaTime;
        }
    }
}
