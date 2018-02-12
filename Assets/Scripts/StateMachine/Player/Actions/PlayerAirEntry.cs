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

        if (Mathf.Abs(data.body.velocity.y) == 0 && data.jumping == false)
        {
            if (!data.climbing && !data.falling)
            {
                data.body.velocity = new Vector2(data.body.velocity.x, 0);

                if(data.melodyData.currentMelody == Melody.MelodyID.JumpMelody) {
                    data.body.AddForce(new Vector2(0, data.boostedjumpPower));
                }
                else {
                    data.body.AddForce(new Vector2(0, data.defaultjumpPower));
                }
               
                
            }
            else if (data.climbing)
            {
                data.moveHorizontal = Input.GetAxis("Horizontal");
                data.body.velocity = new Vector2(data.body.velocity.x, 0);
                data.body.AddForce(new Vector2(data.moveHorizontal, data.jumpPower));
                data.climbing = false;
            }

            data.melodyData.hasDoubleJump = true;
            data.melodyData.doubleJumpTimer.Start();
            data.Pause();
        }
    }
}
