using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerChannelExit")]
public class PlayerChannelExit : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyManagerData mData = data.melodyManagerData;

        if (mData.currentMelody == null || Input.GetButton("PlayMelody")) {
            mData.currentMelody = null;
            if (mData.previousMelody == Melody.MelodyID.JumpMelody) {
                data.jumpPower = data.defaultjumpPower;
            }
        }
    }
}