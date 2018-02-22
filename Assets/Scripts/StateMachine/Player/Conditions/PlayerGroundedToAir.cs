using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerGroundedToAir")]
public class PlayerGroundedToAir : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.grounded = Physics2D.OverlapCircle(data.groundCheck.position, data.groundCheckRadius, data.groundLayer);
        if (!data.grounded)
        {
            data.falling = true;
            return true;
        }
        return false;
    }
}
