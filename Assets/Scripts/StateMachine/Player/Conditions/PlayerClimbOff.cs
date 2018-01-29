using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerClimbOff")]
public class PlayerClimbOff : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;

        //Bottom
        if (data.col.bounds.min.y < data.ladderBottom.GetComponent<Collider2D>().bounds.min.y)
        {
            data.transform.position = new Vector2(data.transform.position.x, data.ladderBottom.position.y);
            return true;
        }

        //Top
        else if (data.col.bounds.min.y > data.ladderTop.GetComponent<Collider2D>().bounds.max.y)
        {
            return true;
        }
        return false;
    }
}
