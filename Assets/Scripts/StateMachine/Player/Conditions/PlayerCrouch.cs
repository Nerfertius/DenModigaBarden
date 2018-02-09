using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerCrouch")]
public class PlayerCrouch : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
