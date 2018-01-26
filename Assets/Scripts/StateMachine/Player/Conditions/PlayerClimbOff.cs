using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerClimbOff")]
public class PlayerClimbOff : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (data.transform.position.y < data.ladderBottom.y)
        {
            data.transform.position = new Vector2(data.transform.position.x, data.ladderBottom.y);
            return true;
        }
        else if (data.transform.position.y > data.ladderTop.y + 0.3f)
        {
            return true;
        }
        return false;
    }
}
