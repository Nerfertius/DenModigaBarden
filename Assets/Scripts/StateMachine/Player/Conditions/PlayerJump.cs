using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerJump")]
public class PlayerJump : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (Input.GetButtonDown("Jump") && Input.GetAxisRaw("PlayMelody") == 0)    //Are you jumping?
        {
            if (data.climbing)
            {
                data.ClimbFixPause();
            }
            data.falling = false;
            return true;
        }
        return false;
    }
}
