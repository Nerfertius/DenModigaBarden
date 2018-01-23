using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerWalking")]
public class PlayerWalking : Condition
{

    public override bool? CheckCondition(StateController controller)
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayerData data = (PlayerData)controller.data;
			data.body.AddForce(new Vector2(0, data.jumpPower));
			data.grounded = false;
			return true;
		}
		return false;
	}
}