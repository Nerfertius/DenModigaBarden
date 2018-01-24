using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerJump")]
public class PlayerJump : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (Input.GetAxis("Jump") == 1)
        {
            return true;
        }
        return false;
    }
}
