using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerHitEntry")]
public class PlayerHitEntry : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        controller.GetComponent<OnHitEffect>().enabled = true;
    }
}
