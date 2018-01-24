using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerClimbJumpAction")]
public class PlayerClimbJumpAction : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.moveHorizontal = Input.GetAxis("Horizontal");
        data.body.AddForce(new Vector2(data.moveHorizontal, data.jumpPower));
        data.body.isKinematic = false;
    }
}
