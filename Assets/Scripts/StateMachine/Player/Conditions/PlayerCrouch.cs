using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerCrouch")]
public class PlayerCrouch : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (Input.GetAxisRaw("Vertical") < -data.axisSensitivity || Input.GetAxisRaw("Crouch") == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
