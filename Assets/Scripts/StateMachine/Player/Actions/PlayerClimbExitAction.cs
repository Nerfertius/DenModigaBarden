using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerClimbExitAction")]
public class PlayerClimbExitAction : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        Physics2D.IgnoreLayerCollision(data.playerLayer, data.climbFixLayer, false);
    }
}
