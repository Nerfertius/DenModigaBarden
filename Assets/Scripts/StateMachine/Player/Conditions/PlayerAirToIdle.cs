using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerAirToIdle")]
public class PlayerAirToIdle : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (!data.jumping && Mathf.Abs(data.body.velocity.x) == 0 && data.body.velocity.y < 0)
        {
            data.grounded = Physics2D.OverlapCircle(data.groundCheck.position, 0.25f, data.groundLayer);
            return data.grounded;
        }
        return null;
    }
}