﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerDoubleJump")]
public class PlayerDoubleJump : StateAction {

    public override void Act(StateController controller) {
        PlayerData data = (PlayerData)controller.data;

        // Double jump
        if (Input.GetButtonDown("Jump") && data.melodyData.hasDoubleJump && 
            data.melodyData.currentMelody == Melody.MelodyID.JumpMelody &&
            Time.realtimeSinceStartup >= data.melodyData.doubleJumpAfterJumpCooldownJumpTime + data.melodyData.doubleJumpAfterJumpCooldown) {
            data.body.velocity = new Vector2(data.body.velocity.x, 0);
            data.body.AddForce(new Vector2(0, data.doubleJumpPower));

            data.melodyData.hasDoubleJump = false;
        }
    }

}