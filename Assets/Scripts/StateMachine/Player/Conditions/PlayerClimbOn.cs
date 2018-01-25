using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerClimbOn")]
public class PlayerClimbOn : Condition
{
	public override bool? CheckTriggerEnter(StateController controller, Collider2D other)
	{
		if (other.CompareTag("Ladder"))
		{
			PlayerData data = (PlayerData)controller.data;
			data.ladderBottom = other.transform.parent.GetChild(0).gameObject;
			data.ladderTop = other.transform.parent.GetChild(1).gameObject;
		}
        return null;
	}

	public override bool? CheckTriggerStay(StateController controller, Collider2D other)
	{
		if (other.CompareTag("Ladder"))
		{
			PlayerData data = (PlayerData)controller.data;
			//Bottom
			if (Input.GetKey(KeyCode.W) && data.transform.position.y < data.ladderBottom.transform.position.y)
			{
                return true;
			}

			//Top
			else if (Input.GetKey(KeyCode.S) && data.transform.position.y > data.ladderTop.transform.position.y)
			{
                return true;
			}

			//Between
			else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) && data.transform.position.y < data.ladderTop.transform.position.y && data.transform.position.y > data.ladderBottom.transform.position.y)
			{
                return true;
			}
		}
		return false;
	}
}
