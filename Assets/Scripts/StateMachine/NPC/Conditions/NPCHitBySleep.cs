using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/NPC/NPCHitBySleep")]
public class NPCHitBySleep : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        if (controller.anim != null && controller.anim.HasState(0, Animator.StringToHash("Sleep")) && PlayerData.player.hasReadNote)
        {
            if (Vector2.Distance(PlayerData.player.transform.position, controller.transform.position) <= 3f && 
                PlayerData.player.melodyData.currentMelody == Melody.MelodyID.SleepMelody)
            {
                return true;
            }
        }
        
        return null;
    }
}
