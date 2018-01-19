using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/NPC/NPCTalkAction")]
public class NPCTalkAction : StateAction {

    public override void ActOnce(StateController controller)
    {
        Debug.Log("tralllala");
    }
}
