using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerClimbOff")]
public class PlayerClimbOff : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        Collider2D botCol = data.ladderBottom.GetComponent<Collider2D>();
        Collider2D topCol = data.ladderTop.GetComponent<Collider2D>();
        float feet = data.coll.bounds.min.y;

        //Bottom
        if (feet < botCol.bounds.min.y)
        {
            return true;
        }

        //Top with a platform behind
        else if (data.ladderBottom.GetComponent<LadderBuilder>().hasPlatformBehind && feet > topCol.bounds.center.y)
        {
            return true;
        }

        //Top WITHOUT a platform behind
        else if (data.ladderBottom.GetComponent<LadderBuilder>().hasPlatformBehind == false && feet > topCol.bounds.max.y)
        {
            return true;
        }
        return false;
    }
}
