using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerAirExit")]
public class PlayerAirExit : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.falling = false;
        data.jumping = false;
        data.body.velocity = new Vector2(data.body.velocity.x, 0);
        data.body.sharedMaterial = data.defaultMat;
    }
}
