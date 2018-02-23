using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerJump")]
public class PlayerJump : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (Input.GetButtonDown("Jump"))    //Are you jumping? // if jumo and play on same button add && Input.GetAxisRaw("PlayMelody") == 0
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
