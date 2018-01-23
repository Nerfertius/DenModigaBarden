using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerClimbJumpOff")]
public class PlayerClimbJumpOff : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
}