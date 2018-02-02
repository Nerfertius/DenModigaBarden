using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerJump")]
public class PlayerJump : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.grounded = Physics2D.OverlapCircle(data.groundCheck.position, 0.15f, data.groundLayer);
        if (Input.GetButtonDown("Jump"))                                                                     //Are you jumping?
        {
            data.falling = false;
            return true;
        }
        else if (data.grounded == false && data.climbing == false)                                          //If not, you're falling
        {
            data.falling = true;
            return true;
        }
        return false;
    }
}
