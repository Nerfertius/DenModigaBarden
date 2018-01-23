using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerClimbOff")]
public class PlayerClimbOff : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (data.transform.position.y < data.ladderBottom.transform.position.y - 0.1f)
        {
            data.transform.position = new Vector2(data.transform.position.x, data.ladderBottom.transform.position.y);
            data.body.isKinematic = false;
            return true;
        }
        else if (data.transform.position.y > data.ladderTop.transform.position.y)
        {
            data.transform.position = new Vector2(data.transform.position.x, data.ladderTop.transform.position.y + 0.7f);
            data.body.isKinematic = false;
            return true;
        }
        return false;
    }
}
