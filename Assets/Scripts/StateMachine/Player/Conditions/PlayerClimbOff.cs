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

        //Top with a platform behind
        else if (data.ladderBottom.GetComponent<LadderBuilder>().hasPlatformBehind && data.col.bounds.min.y > data.ladderTop.GetComponent<Collider2D>().bounds.center.y)
        {
            return true;
        }

        //Top WITHOUT a platform behind
        else if (data.ladderBottom.GetComponent<LadderBuilder>().hasPlatformBehind == false && data.col.bounds.min.y > data.ladderTop.GetComponent<Collider2D>().bounds.max.y)
        {
            return true;
        }
        return false;
    }
}
