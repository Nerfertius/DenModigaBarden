using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerChannelEnter")]
public class PlayerChannelEnter : StateAction
{
    public override void ActOnce(StateController controller) {

        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyManagerData mData = data.melodyManagerData;

        if (mData.justStartedPlaying) {
            mData.justStartedPlaying = false;

            foreach (Melody melody in mData.melodies) {
                if (melody.CheckMelody(mData.PlayedNotes)) {
                    mData.currentMelody = melody.melodyID;
                    mData.PlayedNotes.Clear();
                    break;
                }
            }
        }
        



        if (mData.currentMelody == Melody.MelodyID.JumpMelody) {
            data.jumpPower = data.boostedjumpPower;
        }
    }
}