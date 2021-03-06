﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerCheckFalling")]
public class PlayerCheckFalling : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.grounded = Physics2D.OverlapCircle(data.groundCheck.position, data.groundCheckRadius, data.groundLayer);
        if (data.grounded == false && data.climbing == false)                                          //If not, you're falling
        {
            data.falling = true;
            return true;
        }
        return false;
    }
}
