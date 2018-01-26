using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerAirToWalk")]
public class PlayerAirToWalk : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (!data.jumping)
        {
            data.grounded = Physics2D.OverlapCircle(data.groundCheck.position, 0.15f, data.groundLayer);
            return data.grounded;
        }
        return null;
    }
}