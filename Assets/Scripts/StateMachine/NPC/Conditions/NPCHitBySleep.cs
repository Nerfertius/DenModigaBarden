using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/NPC/NPCHitBySleep")]
public class NPCHitBySleep : Condition
{
    public override bool? CheckTriggerEnter(StateController controller, Collider2D other)
    {
        if (controller.gameObject.GetComponent<Animator>().HasState(0, Animator.StringToHash("Sleep")))
        {
            if (other.CompareTag("PlayerProjectile") && other.GetComponent<MelodyProjectile>().melodyID == Melody.MelodyID.SleepMelody)
            {
                return true;
            }
        }
        
        return null;
    }
}
