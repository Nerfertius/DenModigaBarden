using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerWalkAction")]
public class PlayerWalkAction : StateAction {

    public override void FixedAct(StateController controller)
    {
		PlayerData data = (PlayerData)controller.data;
		data.moveHorizontal = Input.GetAxis("Horizontal");
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
	}

	public override void Act(StateController controller)
	{
		PlayerData data = (PlayerData)controller.data;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			data.body.AddForce(new Vector2(0, data.jumpPower));
			data.grounded = false;
		}
	}
}