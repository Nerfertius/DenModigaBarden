using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerCrouchEnter")]
public class PlayerCrouchEnter : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.body.sharedMaterial = data.fullFriction;
    }
}
