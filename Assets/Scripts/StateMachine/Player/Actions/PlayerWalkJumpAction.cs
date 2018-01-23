using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerWalkJumpAction")]
public class PlayerWalkJumpAction : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.moveHorizontal = Input.GetAxis("Horizontal");
        data.body.AddForce(new Vector2(0, data.jumpPower));
        data.body.isKinematic = false;
    }
}