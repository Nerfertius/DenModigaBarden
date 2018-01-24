using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerWalkToAir")]
public class PlayerWalkToAir : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (!data.grounded)
        {
            return true;
        }
        return false;
    }
}
