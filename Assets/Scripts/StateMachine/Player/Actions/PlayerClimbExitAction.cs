using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerClimbExitAction")]
public class PlayerClimbExitAction : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;

        int playerLayer = data.body.gameObject.layer;
        int blockablesLayer = LayerMask.NameToLayer("Blockable");
        Physics2D.IgnoreLayerCollision(playerLayer, blockablesLayer, false);
    }
}
