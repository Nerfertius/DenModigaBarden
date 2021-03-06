﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerAirToWalk")]
public class PlayerAirToWalk : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (!data.jumping && Mathf.Abs(data.body.velocity.x) > 0 && data.body.velocity.y <= 0 && Physics2D.IsTouchingLayers(controller.coll, data.groundLayer))
        {
            data.grounded = Physics2D.OverlapCircle(data.groundCheck.position, data.groundCheckRadius, data.groundLayer);
            return data.grounded;
        }
        return null;
    }
}