using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerJumpAction")]
public class PlayerJumpAction : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (!data.climbing)
        {
            data.body.AddForce(new Vector2(0, data.jumpPower));
        }
        else
        {
            data.moveHorizontal = Input.GetAxis("Horizontal");
            data.body.AddForce(new Vector2(data.moveHorizontal, data.jumpPower));
            data.climbing = false;
        }
        data.body.isKinematic = false;
    }
}
