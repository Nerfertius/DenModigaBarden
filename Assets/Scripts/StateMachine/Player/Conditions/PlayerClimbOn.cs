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
            data.ladderBottom = other.transform.parent.GetChild(0).transform.position;
            data.ladderTop = other.transform.parent.GetChild(1).transform.position;
        }
        return null;
	}

    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (data.transform.position.x > data.ladderBottom.x - 0.5f && data.transform.position.x < data.ladderBottom.x + 0.5f)
        {
            //Bottom
            if (Input.GetKey(KeyCode.W) && data.transform.position.y < data.ladderBottom.y)
            {
                return true;
            }

            //Top
            else if (Input.GetKey(KeyCode.S) && data.transform.position.y > data.ladderTop.y)
            {
                return true;
            }

            //Between
            else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) && data.transform.position.y < data.ladderTop.y && data.transform.position.y > data.ladderBottom.y)
            {
                return true;
            }
        }
        return false;
    }
}
