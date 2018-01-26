using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerAirEntry")]
public class PlayerAirEntry : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.body.gravityScale = 1;

        if (!data.climbing && !data.falling)
        {
            data.body.velocity = new Vector2(data.body.velocity.x, 0);
            data.body.AddForce(new Vector2(0, data.jumpPower));
        }
        else if (data.climbing)
        {
            data.moveHorizontal = Input.GetAxis("Horizontal");
            data.body.AddForce(new Vector2(data.moveHorizontal, data.jumpPower));
            data.climbing = false;
        }
        data.Pause();
    }
}
