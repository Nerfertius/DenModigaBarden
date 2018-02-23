using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerIdleToWalk")]
public class PlayerIdleToWalk : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || (Mathf.Abs(data.body.velocity.x) > 0))
        {
            return true;
        }
        return false;
    }
}
