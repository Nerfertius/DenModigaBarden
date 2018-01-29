using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerPlayMelodyEnter")]
public class PlayerPlayMelodyEnter : StateAction {
    public override void ActOnce(StateController controller) {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;


        Melody m = mData.getMelody(mData.currentMelody);
        //m.audio
        mData.PlayedNotes.Clear();
    }
}
