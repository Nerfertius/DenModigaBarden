﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerChannelEnter")]
public class PlayerChannelEnter : StateAction
{
    public override void ActOnce(StateController controller) {

        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        if (mData.currentMelody == Melody.MelodyID.JumpMelody) {
            data.jumpPower = data.boostedjumpPower;
        }
    }
}