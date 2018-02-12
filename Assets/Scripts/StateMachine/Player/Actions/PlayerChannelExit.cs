using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerChannelExit")]
public class PlayerChannelExit : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        if (mData.currentMelody == null || Input.GetButton("PlayMelody")) {

            mData.previousMelody = mData.currentMelody;
            mData.currentMelody = null;
            mData.MelodyRange.enabled = false;

            if (mData.previousMelody == Melody.MelodyID.JumpMelody) {
                data.jumpPower = data.defaultjumpPower;
            }
        }
    }
}