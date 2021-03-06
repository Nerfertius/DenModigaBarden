﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerWalkAction")]
public class PlayerWalkAction : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.climbing = false;
        data.body.gravityScale = 1;
    }

    public override void Act(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.moveHorizontal = Input.GetAxis("Horizontal");
    }

    public override void FixedAct(StateController controller)
    {
		PlayerData data = (PlayerData)controller.data;
		data.movement = new Vector2(data.moveHorizontal, 0);
		data.body.AddForce(data.movement * data.speedMod);

		if (Mathf.Abs(data.body.velocity.x) > data.maxSpeed)
		{
			if (data.body.velocity.x > 0)
			{
				data.body.velocity = new Vector2(data.maxSpeed, data.body.velocity.y);
			}
			else if (data.body.velocity.x < 0)
			{
				data.body.velocity = new Vector2(data.maxSpeed * -1, data.body.velocity.y);
			}
		}

        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
        {
            data.anim.SetFloat("SpeedMultiplier", 1);
        } else
        {
            data.anim.SetFloat("SpeedMultiplier", Mathf.Abs(data.rb.velocity.x / data.maxSpeed));
        }
	}
}